using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakeDecorationService
{
    Task<CakeDecoration> CreateAsync(CakeDecorationCreateModel model);
    Task<CakeDecoration> UpdateAsync(Guid id, CakeDecorationUpdateModel model);
    Task<CakeDecoration> GetByIdAsync(Guid id);
    Task<(Pagination<CakeDecoration>, List<CakeDecoration>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeDecoration, bool>>? filter = null);
    Task DeleteAsync(Guid id);
}

public class CakeDecorationService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService,
                            IFileService fileService
                        ) : ICakeDecorationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IFileService _fileService = fileService;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<CakeDecoration> CreateAsync(CakeDecorationCreateModel model)
    {
        var decoration = _mapper.Map<CakeDecoration>(model);

        if (model.Image != null)
        {
            decoration.DecorationImageId = await _fileService.UploadFileAsync(model.Image, FolderConstants.CAKE_PART_IMAGES);
        }

        decoration.BakeryId = _claimsService.GetCurrentUser;

        var result = await _unitOfWork.CakeDecorationRepository.AddAsync(decoration);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var decoration = await GetByIdAsync(id);

        _unitOfWork.CakeDecorationRepository.SoftRemove(decoration);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination<CakeDecoration>, List<CakeDecoration>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeDecoration, bool>>? filter = null)
    {
        Expression<Func<CakeDecoration, bool>> combinedFilter = filter ?? (x => true);

        Expression<Func<CakeDecoration, bool>> idFilter = x => x.BakeryId == _claimsService.GetCurrentUser;
        combinedFilter = FilterCustom.CombineFilters(combinedFilter, idFilter);

        return await _unitOfWork.CakeDecorationRepository.ToPagination(pageIndex, pageSize, includes: x => x.DecorationImage!, filter: combinedFilter);
    }

    public async Task<CakeDecoration> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakeDecorationRepository.GetByIdAsync(id, includes: x => x.DecorationImage!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakeDecoration> UpdateAsync(Guid id, CakeDecorationUpdateModel model)
    {
        var decoration = await GetByIdAsync(id);

        if (decoration.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

        _mapper.Map(model, decoration);

        if (model.Image is not null)
        {
            decoration.DecorationImageId = await _fileService.UploadFileAsync(model.Image, FolderConstants.CAKE_PART_IMAGES);
        }

        _unitOfWork.CakeDecorationRepository.Update(decoration);

        await _unitOfWork.SaveChangesAsync();

        return decoration;
    }
}
