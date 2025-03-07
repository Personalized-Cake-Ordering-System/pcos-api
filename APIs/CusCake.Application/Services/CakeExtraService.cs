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
    Task<List<CakeExtra>> CreateAsync(List<CakeExtraCreateModel> models);
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


    private async Task<List<CakeExtra>?> GetListDefaultAsync(List<string> types)
    {
        var cake_extras = await _unitOfWork.CakeExtraRepository
                    .WhereAsync(x =>
                        x.IsDefault &&
                        types.Contains(x.ExtraType) &&
                        x.BakeryId == _claimsService.GetCurrentUser
                    );

        return cake_extras.Count != 0 ? cake_extras : null;
    }



    public async Task<List<CakeExtra>> CreateAsync(List<CakeExtraCreateModel> models)
    {
        var extras = _mapper.Map<List<CakeExtra>>(models);

        var default_extras = await GetListDefaultAsync([.. models.Select(x => x.ExtraType)]);

        foreach (var extra in extras)
        {
            if (default_extras != null && default_extras.Any(x => x.ExtraType == extra.ExtraType))
                throw new BadRequestException($"Type {extra.ExtraType} already has default value!");

            extra.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakeExtraRepository.AddRangeAsync(extras);

        await _unitOfWork.SaveChangesAsync();

        return extras;
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

        var default_extras = await GetListDefaultAsync([extra.ExtraType]);

        if (default_extras != null && default_extras[0].Id != extra.Id)
            throw new BadRequestException($"Type {extra.ExtraType} already has default value!");

        _unitOfWork.CakeExtraRepository.Update(extra);

        await _unitOfWork.SaveChangesAsync();

        return extra;
    }
}
