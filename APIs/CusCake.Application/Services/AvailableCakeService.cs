using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAvailableCakeService
{
    public Task<AvailableCake> GetByIdAsync(Guid id);

    public Task<AvailableCake> CreateAsync(AvailableCakeCreateModel model);

    public Task<AvailableCake> UpdateAsync(Guid id, AvailableCakeUpdateModel model);

    public Task DeleteAsync(Guid id);

    public Task<(Pagination<AvailableCake>, List<AvailableCake>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<AvailableCake, bool>>? filter = null);
}

public class AvailableCakeService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService) : IAvailableCakeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IMapper _mapper = mapper;

    private readonly IFileService _fileService = fileService;

    public async Task<AvailableCake> CreateAsync(AvailableCakeCreateModel model)
    {
        var cake = _mapper.Map<AvailableCake>(model);


        cake.AvailableCakeMainImageId = await _fileService.UploadFileAsync(model.AvailableCakeFileImage, FolderConstants.AVAILABLE_CAKE_IMAGES);


        var result = await _unitOfWork.AvailableCakeRepository.AddAsync(cake);

        await _unitOfWork.SaveChangesAsync();

        return result;

    }

    public async Task DeleteAsync(Guid id)
    {
        var cake = await GetByIdAsync(id);

        _unitOfWork.AvailableCakeRepository.SoftRemove(cake);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination<AvailableCake>, List<AvailableCake>)> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        Expression<Func<AvailableCake, bool>>? filter = null)
    {
        return await _unitOfWork.AvailableCakeRepository.ToPagination(pageIndex, pageSize, filter: filter);

    }

    public async Task<AvailableCake> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.AvailableCakeRepository.GetByIdAsync(id) ?? throw new BadRequestException("Cake is not exist!");
    }

    public async Task<AvailableCake> UpdateAsync(Guid id, AvailableCakeUpdateModel model)
    {
        var cake = await GetByIdAsync(id);

        _mapper.Map(model, cake);


        if (model.AvailableCakeFileImage != null)
        {
            cake.AvailableCakeMainImageId = await _fileService.UploadFileAsync(model.AvailableCakeFileImage, FolderConstants.AVAILABLE_CAKE_IMAGES);
        }

        _unitOfWork.AvailableCakeRepository.Update(cake);

        await _unitOfWork.SaveChangesAsync();

        return cake;
    }
}
