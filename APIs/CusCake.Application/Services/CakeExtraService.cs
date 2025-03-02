using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeExtraModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakeExtraService
{
    Task<CakeExtra> CreateAsync(CakeExtraCreateModel model);
    Task<CakeExtra> UpdateAsync(Guid id, CakeExtraUpdateModel model);
    Task<CakeExtra> GetByIdAsync(Guid id);
    Task<(Pagination<CakeExtra>, List<CakeExtra>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeExtra, bool>>? filter = null);
    Task DeleteAsync(Guid id);
}

public class CakeExtraService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService,
                            IFileService fileService
                        ) : ICakeExtraService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IFileService _fileService = fileService;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<CakeExtra> CreateAsync(CakeExtraCreateModel model)
    {
        var extra = _mapper.Map<CakeExtra>(model);

        if (model.Image != null)
        {
            extra.ExtraImageId = await _fileService.UploadFileAsync(model.Image, FolderConstants.CAKE_PART_IMAGES);
        }

        extra.BakeryId = _claimsService.GetCurrentUser;

        var result = await _unitOfWork.CakeExtraRepository.AddAsync(extra);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var extra = await GetByIdAsync(id);

        _unitOfWork.CakeExtraRepository.SoftRemove(extra);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination<CakeExtra>, List<CakeExtra>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeExtra, bool>>? filter = null)
    {
        Expression<Func<CakeExtra, bool>> combinedFilter = filter ?? (x => true);

        Expression<Func<CakeExtra, bool>> idFilter = x => x.BakeryId == _claimsService.GetCurrentUser;
        combinedFilter = FilterCustom.CombineFilters(combinedFilter, idFilter);

        return await _unitOfWork.CakeExtraRepository.ToPagination(pageIndex, pageSize, includes: x => x.ExtraImage!, filter: combinedFilter);
    }

    public async Task<CakeExtra> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakeExtraRepository.GetByIdAsync(id, includes: x => x.ExtraImage!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakeExtra> UpdateAsync(Guid id, CakeExtraUpdateModel model)
    {
        var extra = await GetByIdAsync(id);

        if (extra.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

        _mapper.Map(model, extra);

        if (model.Image is not null)
        {
            extra.ExtraImageId = await _fileService.UploadFileAsync(model.Image, FolderConstants.CAKE_PART_IMAGES);
        }

        _unitOfWork.CakeExtraRepository.Update(extra);

        await _unitOfWork.SaveChangesAsync();

        return extra;
    }
}
