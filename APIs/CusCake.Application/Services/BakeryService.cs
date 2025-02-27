using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.BakeryModel;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

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
        var bakery = await GetByIdAsync(id);
        bakery.Status = isApprove ? BakeryStatusConstants.CONFIRMED : BakeryStatusConstants.REJECT;
        bakery.ConfirmedAt = _currentTime.GetCurrentTime();

        _unitOfWork.BakeryRepository.Update(bakery);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Bakery> CreateAsync(BakeryCreateModel model)
    {
        var existBakeries = await _unitOfWork
                            .BakeryRepository
                            .WhereAsync(b =>
                                b.Email == model.Email ||
                                b.Phone == model.Phone ||
                                b.BakeryName == model.BakeryName ||
                                b.TaxCode == model.TaxCode ||
                                b.IdentityCardNumber == model.IdentityCardNumber);

        if (existBakeries.Count > 0)
        {
            if (existBakeries.Any(x => x.BakeryName == model.BakeryName)) throw new BadRequestException($"Name '{model.BakeryName}' already exists.");
            if (existBakeries.Any(x => x.Email == model.Email)) throw new BadRequestException($"Email '{model.Email}' already exists.");
            if (existBakeries.Any(x => x.TaxCode == model.TaxCode)) throw new BadRequestException($"TaxCode '{model.TaxCode}' already exists.");
            if (existBakeries.Any(x => x.Phone == model.Phone)) throw new BadRequestException($"Phone '{model.Phone}' already exists.");
            if (existBakeries.Any(x => x.IdentityCardNumber == model.IdentityCardNumber)) throw new BadRequestException($"Phone '{model.IdentityCardNumber}' already exists.");
        }

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
        var bakery = await GetByIdAsync(id);

        _unitOfWork.BakeryRepository.SoftRemove(bakery);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination<Bakery>, List<Bakery>)> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        return await _unitOfWork.BakeryRepository.ToPagination(pageIndex, pageSize);
    }

    public async Task<Bakery> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.BakeryRepository.GetByIdAsync(id) ?? throw new BadRequestException("Is is not exist!");

    }

    public async Task<Bakery> UpdateAsync(Guid id, BakeryCreateModel model)
    {
        var bakery = await GetByIdAsync(id);

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
