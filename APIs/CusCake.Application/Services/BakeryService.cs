using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Newtonsoft.Json;

namespace CusCake.Application.Services;


public interface IBakeryService
{
    Task<Bakery> CreateAsync(BakeryCreateModel model);
    Task<Bakery> UpdateAsync(Guid id, BakeryUpdateModel model);
    Task<Bakery> GetByIdAsync(Guid id);
    Task<(Pagination, List<Bakery>)> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        double customerLat = 0,
        double customerLng = 0,
        Expression<Func<Bakery, bool>>? filter = null);
    Task DeleteAsync(Guid id);
    Task<bool> ApproveBakeryAsync(Guid id, bool isApprove = true);
    Task<bool> BanedBakeryAsync(Guid id, string action);
}

public class BakeryService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentTime currentTime,
    IAuthService authService,
    IClaimsService claimsService,
    INotificationService notificationService) : IBakeryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;
    private readonly IClaimsService _claimsService = claimsService;
    private readonly ICurrentTime _currentTime = currentTime;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<bool> ApproveBakeryAsync(Guid id, bool isApprove = false)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status == BakeryStatusConstants.PENDING & x.Id == id) ?? throw new BadRequestException("Is is not found!");
        bakery.Status = isApprove ? BakeryStatusConstants.CONFIRMED : BakeryStatusConstants.REJECT;
        bakery.ConfirmedAt = _currentTime.GetCurrentTime();

        _unitOfWork.BakeryRepository.Update(bakery);
        var result = await _unitOfWork.SaveChangesAsync();

        if (isApprove)
        {
            await _authService.CreateAsync(new AuthCreateModel
            {
                Email = bakery.Email,
                Password = bakery.Password,
                Role = RoleConstants.BAKERY,
                EntityId = bakery.Id,
                BakeryId = bakery.Id
            });

        }
        return result;
    }

    public async Task<Bakery> CreateAsync(BakeryCreateModel model)
    {
        await ValidateBakery(model.BakeryName, model.Email, model.Phone, model.TaxCode, model.IdentityCardNumber);
        var admin = await _authService.GetAdminAsync();
        var bakery = _mapper.Map<Bakery>(model);

        bakery.Status = BakeryStatusConstants.PENDING;

        bakery.ShopImageFiles = await _unitOfWork.StorageRepository.WhereAsync(s => model.ShopImageFileIds.Contains(s.Id));

        var result = await _unitOfWork.BakeryRepository.AddAsync(bakery);
        await _unitOfWork.SaveChangesAsync();

        var bakeryJson = JsonConvert.SerializeObject(bakery, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await _notificationService.CreateAdminNotificationAsync(bakery.Id, NotificationType.NEW_BAKERY_REGISTRATION, admin.EntityId);
        await _notificationService.SendNotificationAsync(admin.EntityId, bakeryJson, NotificationType.NEW_BAKERY_REGISTRATION);

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status != BakeryStatusConstants.REJECT & x.Id == id) ?? throw new BadRequestException("Id is not found!");

        _unitOfWork.BakeryRepository.SoftRemove(bakery);

        await _unitOfWork.SaveChangesAsync();

        await _authService.DeleteAsync(bakery.Id);
    }

    public async Task<(Pagination, List<Bakery>)> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        double customerLat = 0,
        double customerLng = 0,
        Expression<Func<Bakery, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<Bakery>(
            x => x.AvatarFile!,
            x => x.FrontCardFile!,
            x => x.FoodSafetyCertificateFile!,
            x => x.BusinessLicenseFile!,
            x => x.BackCardFile!,
            x => x.Metric!);
        var result = await _unitOfWork.BakeryRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
        if (customerLat != 0 && customerLng != 0)
        {
            foreach (var bakery in result.Item2)
            {
                bakery.DistanceToUser = Haversine.CalculateDistance(customerLat, customerLng, Double.Parse(bakery.Latitude), Double.Parse(bakery.Longitude));
            }
            result.Item2 = [.. result.Item2.OrderBy(x => x.DistanceToUser)];
        }
        return result;
    }

    public async Task<Bakery> GetByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Bakery>(
            x => x.AvatarFile!,
            x => x.FrontCardFile!,
            x => x.FoodSafetyCertificateFile!,
            x => x.BusinessLicenseFile!,
            x => x.BackCardFile!,
            x => x.Metric!
        );

        var cake = await _unitOfWork.BakeryRepository.GetByIdAsync(id, includes: includes) ?? throw new BadRequestException("Id is not exist!");
        cake.Reviews = await _unitOfWork.ReviewRepository.WhereAsync(x => x.BakeryId == id & x.ReviewType == ReviewTypeConstants.BAKERY_REVIEW);

        return cake;
    }

    private async Task ValidateBakery(string name = "", string email = "", string phone = "", string taxCode = "", string cardNumber = "")
    {
        var existBakeries = await _unitOfWork
                                    .BakeryRepository
                                    .WhereAsync(b =>
                                        b.Status != BakeryStatusConstants.REJECT & (
                                        b.Email == email ||
                                        b.Phone == phone ||
                                        b.BakeryName == name ||
                                        b.TaxCode == taxCode ||
                                        b.IdentityCardNumber == cardNumber
                                    ));

        if (existBakeries.Count > 0)
        {
            // if (!string.IsNullOrEmpty(name) && existBakeries.Any(x => x.BakeryName == name))
            //     throw new BadRequestException($"Name '{name}' already exists.");
            if (!string.IsNullOrEmpty(email) && existBakeries.Any(x => x.Email == email))
                throw new BadRequestException($"Email '{email}' already exists.");
            if (!string.IsNullOrEmpty(taxCode) && existBakeries.Any(x => x.TaxCode == taxCode))
                throw new BadRequestException($"TaxCode '{taxCode}' already exists.");
            if (!string.IsNullOrEmpty(phone) && existBakeries.Any(x => x.Phone == phone))
                throw new BadRequestException($"Phone '{phone}' already exists.");
            if (!string.IsNullOrEmpty(cardNumber) && existBakeries.Any(x => x.IdentityCardNumber == cardNumber))
                throw new BadRequestException($"IdentityCardNumber '{cardNumber}' already exists.");
        }
    }

    public async Task<Bakery> UpdateAsync(Guid id, BakeryUpdateModel model)
    {
        var bakery = await _unitOfWork.BakeryRepository
            .FirstOrDefaultAsync(x =>
                x.Status == BakeryStatusConstants.CONFIRMED &&
                x.Id == id
                ) ?? throw new BadRequestException("Id is not found!");

        if (bakery.BakeryName != model.BakeryName)
            await ValidateBakery(
                model.BakeryName == bakery.BakeryName ? model.BakeryName : "",
                model.Phone == bakery.Phone ? model.Phone : "",
                model.TaxCode == bakery.TaxCode ? model.TaxCode : "",
                model.IdentityCardNumber == bakery.IdentityCardNumber ? model.IdentityCardNumber : ""
            );

        _mapper.Map(model, bakery);

        bakery.ShopImageFiles = await _unitOfWork.StorageRepository.WhereAsync(s => model.ShopImageFileIds.Contains(s.Id));

        _unitOfWork.BakeryRepository.Update(bakery);
        await _unitOfWork.SaveChangesAsync();

        await _authService.UpdateAsync(new AuthUpdateModel
        {
            EntityId = id,
            Password = bakery.Password
        });

        return bakery;
    }

    public async Task<bool> BanedBakeryAsync(Guid id, string action)
    {
        if (!action.Equals("BAN") && !action.Equals("UN_BAN"))
            throw new BadRequestException("Invalid action!");

        var bakery = await GetByIdAsync(id);
        if (bakery.Status != BakeryStatusConstants.CONFIRMED)
            throw new BadRequestException("Status of bakery must be CONFIRMED");

        bakery.Status = action.Equals("BAN") ? BakeryStatusConstants.BANNED : BakeryStatusConstants.CONFIRMED;
        bakery.BannedAt = action.Equals("BAN") ? DateTime.Now : null;

        _unitOfWork.BakeryRepository.Update(bakery);

        return await _unitOfWork.SaveChangesAsync();
    }
}
