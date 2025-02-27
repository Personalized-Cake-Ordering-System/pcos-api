using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.BakeryModel;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using UnauthorizedAccessException = CusCake.Application.GlobalExceptionHandling.Exceptions.UnauthorizedAccessException;

namespace CusCake.Application.Services;


public interface IBakeryService
{
    Task<Bakery> CreateAsync(BakeryCreateModel model);
    Task<Bakery> UpdateAsync(Guid id, BakeryCreateModel model);
    Task<Bakery> GetByIdAsync(Guid id);
    Task<(Pagination<Bakery>, List<Bakery>)> GetAllAsync(int pageIndex = 0, int pageSize = 10);
    Task DeleteAsync(Guid id);

    Task<bool> ApproveBakeryAsync(Guid id, bool isApprove = true);
}

public class BakeryService : IBakeryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    private readonly ICurrentTime _currentTime;
    public BakeryService(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper, ICurrentTime currentTime)
    {
        _fileService = fileService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
    }

    public async Task<bool> ApproveBakeryAsync(Guid id, bool isApprove = false)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status == BakeryStatusConstants.PENDING & x.Id == id) ?? throw new BadRequestException("Is is not found!");
        bakery.Status = isApprove ? BakeryStatusConstants.CONFIRMED : BakeryStatusConstants.REJECT;
        bakery.ConfirmedAt = _currentTime.GetCurrentTime();

        _unitOfWork.BakeryRepository.Update(bakery);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Bakery> CreateAsync(BakeryCreateModel model)
    {
        await ValidateBakery(model.BakeryName, model.Email, model.Phone, model.TaxCode, model.IdentityCardNumber);

        var bakery = _mapper.Map<Bakery>(model);

        bakery.AvatarFileId = await _fileService.UploadFileAsync(model.Avatar, FolderConstants.AVATAR);
        bakery.FrontCardFileId = await _fileService.UploadFileAsync(model.FrontCardImage, FolderConstants.IDENTITY_CARD);
        bakery.BackCardFileId = await _fileService.UploadFileAsync(model.BackCardImage, FolderConstants.IDENTITY_CARD);

        if (model.ShopImages.Count != 0)
        {
            foreach (var image in model.ShopImages)
            {
                bakery.ShopImageFiles.Add(await _fileService.UploadFileAsync(image, FolderConstants.BAKER_IMAGES));
            }
        }

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
    }

    public async Task<(Pagination<Bakery>, List<Bakery>)> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        return await _unitOfWork.BakeryRepository.ToPagination(pageIndex, pageSize);
    }

    public async Task<Bakery> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.BakeryRepository.GetByIdAsync(id) ?? throw new BadRequestException("Id is not exist!");

    }

    private async Task ValidateBakery(string name, string email, string phone, string taxCode, string cardNumber)
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
            if (existBakeries.Any(x => x.BakeryName == name)) throw new BadRequestException($"Name '{name}' already exists.");
            if (existBakeries.Any(x => x.Email == email)) throw new BadRequestException($"Email '{email}' already exists.");
            if (existBakeries.Any(x => x.TaxCode == taxCode)) throw new BadRequestException($"TaxCode '{taxCode}' already exists.");
            if (existBakeries.Any(x => x.Phone == phone)) throw new BadRequestException($"Phone '{phone}' already exists.");
            if (existBakeries.Any(x => x.IdentityCardNumber == cardNumber)) throw new BadRequestException($"Phone '{cardNumber}' already exists.");
        }
    }

    public async Task<Bakery> UpdateAsync(Guid id, BakeryCreateModel model)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Status == BakeryStatusConstants.CONFIRMED & x.Id == id) ?? throw new BadRequestException("Id is not found!");

        await ValidateBakery(model.BakeryName, model.Email, model.Phone, model.TaxCode, model.IdentityCardNumber);

        _mapper.Map(model, bakery);

        if (model.Avatar != null)
            bakery.AvatarFileId = await _fileService.UploadFileAsync(model.Avatar, FolderConstants.AVATAR);

        if (model.FrontCardImage != null)
            bakery.FrontCardFileId = await _fileService.UploadFileAsync(model.FrontCardImage, FolderConstants.IDENTITY_CARD);

        if (model.BackCardImage != null)
            bakery.BackCardFileId = await _fileService.UploadFileAsync(model.BackCardImage, FolderConstants.IDENTITY_CARD);

        if (model.ShopImages.Count != 0)
        {
            foreach (var image in model.ShopImages)
            {
                bakery.ShopImageFiles.Add(await _fileService.UploadFileAsync(image, FolderConstants.BAKER_IMAGES));
            }
        }


        _unitOfWork.BakeryRepository.Update(bakery);
        await _unitOfWork.SaveChangesAsync();

        return bakery;
    }
}
