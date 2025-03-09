using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;


public interface IBakeryService
{
    Task<Bakery> CreateAsync(BakeryCreateModel model);
    Task<Bakery> UpdateAsync(Guid id, BakeryUpdateModel model);
    Task<Bakery> GetByIdAsync(Guid id);
    Task<(Pagination, List<Bakery>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Bakery, bool>>? filter = null);
    Task DeleteAsync(Guid id);

    Task<bool> ApproveBakeryAsync(Guid id, bool isApprove = true);
}

public class BakeryService(
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IMapper mapper,
    ICurrentTime currentTime,
    IAuthService authService) : IBakeryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;

    private readonly ICurrentTime _currentTime = currentTime;

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
                EntityId = bakery.Id
            });

        }
        return result;
    }

    public async Task<Bakery> CreateAsync(BakeryCreateModel model)
    {
        await ValidateBakery(model.BakeryName, model.Email, model.Phone, model.TaxCode, model.IdentityCardNumber);

        var bakery = _mapper.Map<Bakery>(model);

        bakery.Status = BakeryStatusConstants.PENDING;

        var result = await _unitOfWork.BakeryRepository.AddAsync(bakery);
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status != BakeryStatusConstants.REJECT & x.Id == id) ?? throw new BadRequestException("Id is not found!");

        _unitOfWork.BakeryRepository.SoftRemove(bakery);

        await _unitOfWork.SaveChangesAsync();

        await _authService.DeleteAsync(bakery.Id);
    }

    public async Task<(Pagination, List<Bakery>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<Bakery, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<Bakery>(x => x.AvatarFile!, x => x.FrontCardFile!, x => x.BackCardFile);
        return await _unitOfWork.BakeryRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
    }

    public async Task<Bakery> GetByIdAsync(Guid id)
    {
        var includes = QueryHelper.Includes<Bakery>(x => x.AvatarFile!, x => x.FrontCardFile!, x => x.BackCardFile);

        return await _unitOfWork.BakeryRepository.GetByIdAsync(id, includes: includes) ?? throw new BadRequestException("Id is not exist!");

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
            if (!string.IsNullOrEmpty(name) && existBakeries.Any(x => x.BakeryName == name))
                throw new BadRequestException($"Name '{name}' already exists.");
            if (!string.IsNullOrEmpty(email) && existBakeries.Any(x => x.Email == email))
                throw new BadRequestException($"Email '{email}' already exists.");
            if (!string.IsNullOrEmpty(taxCode) && existBakeries.Any(x => x.TaxCode == taxCode))
                throw new BadRequestException($"TaxCode '{taxCode}' already exists.");
            if (!string.IsNullOrEmpty(phone) && existBakeries.Any(x => x.Phone == phone))
                throw new BadRequestException($"Phone '{phone}' already exists.");
            if (!string.IsNullOrEmpty(cardNumber) && existBakeries.Any(x => x.IdentityCardNumber == cardNumber))
                throw new BadRequestException($"Phone '{cardNumber}' already exists.");
        }
    }

    public async Task<Bakery> UpdateAsync(Guid id, BakeryUpdateModel model)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status == BakeryStatusConstants.CONFIRMED & x.Id == id) ?? throw new BadRequestException("Id is not found!");

        if (bakery.BakeryName != model.BakeryName)
            await ValidateBakery(
                model.BakeryName == bakery.BakeryName ? model.BakeryName : "",
                model.Phone == bakery.Phone ? model.Phone : "",
                model.TaxCode == bakery.TaxCode ? model.TaxCode : "",
                model.IdentityCardNumber == bakery.IdentityCardNumber ? model.IdentityCardNumber : ""
            );

        _mapper.Map(model, bakery);

        _unitOfWork.BakeryRepository.Update(bakery);
        await _unitOfWork.SaveChangesAsync();

        await _authService.UpdateAsync(new AuthUpdateModel
        {
            EntityId = id,
            Password = bakery.Password
        });

        return bakery;
    }
}
