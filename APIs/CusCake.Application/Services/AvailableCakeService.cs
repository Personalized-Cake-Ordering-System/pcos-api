using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAvailableCakeService
{
    public Task<AvailableCake> GetByIdAsync(Guid id);

    public Task<AvailableCake> CreateAsync(AvailableCakeCreateModel model);

    public Task<AvailableCake> UpdateAsync(Guid id, AvailableCakeUpdateModel model);

    public Task DeleteAsync(Guid id);

    Task<(Pagination, List<AvailableCake>)> GetAllAsync(
               int pageIndex = 0,
               int pageSize = 10,
               Expression<Func<AvailableCake, bool>>? filter = null,
               List<(Expression<Func<AvailableCake, object>> OrderBy, bool IsDescending)>? orderByList = null);
}

public class AvailableCakeService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IClaimsService claimsService) : IAvailableCakeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IMapper _mapper = mapper;

    private readonly IFileService _fileService = fileService;

    private readonly IClaimsService _claimsService = claimsService;

    public async Task<AvailableCake> CreateAsync(AvailableCakeCreateModel model)
    {
        var cake = _mapper.Map<AvailableCake>(model);

        cake.BakeryId = _claimsService.GetCurrentUser;

        cake.AvailableCakeImageFiles = await _unitOfWork.StorageRepository.WhereAsync(x => model.AvailableCakeImageFileIds.Contains(x.Id));

        var result = await _unitOfWork.AvailableCakeRepository.AddAsync(cake);

        await _unitOfWork.SaveChangesAsync();

        return result;

    }

    public async Task DeleteAsync(Guid id)
    {
        var cake = await GetByIdAsync(id);

        if (cake.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to delete");

        _unitOfWork.AvailableCakeRepository.SoftRemove(cake);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination, List<AvailableCake>)> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        Expression<Func<AvailableCake, bool>>? filter = null,
        List<(Expression<Func<AvailableCake, object>> OrderBy, bool IsDescending)>? orderByList = null)
    {
        return await _unitOfWork.AvailableCakeRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: x => x.Bakery, orderByList: orderByList);

    }

    public async Task<AvailableCake> GetByIdAsync(Guid id)
    {
        var available_cake = await _unitOfWork.AvailableCakeRepository.GetByIdAsync(id) ?? throw new BadRequestException("Cake is not exist!");
        available_cake.CakeReviews = await _unitOfWork.CakeReviewRepository.WhereAsync(x => x.AvailableCakeId == id);
        return available_cake;
    }

    public async Task<AvailableCake> UpdateAsync(Guid id, AvailableCakeUpdateModel model)
    {
        var cake = await GetByIdAsync(id);

        if (cake.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to update");
        _mapper.Map(model, cake);

        cake.AvailableCakeImageFiles = await _unitOfWork.StorageRepository.WhereAsync(x => model.AvailableCakeImageFileIds.Contains(x.Id));

        _unitOfWork.AvailableCakeRepository.Update(cake);

        await _unitOfWork.SaveChangesAsync();

        return cake;
    }
}
