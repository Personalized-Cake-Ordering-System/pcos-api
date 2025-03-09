using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeExtraModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakeExtraService
{
    Task<List<CakeExtraOption>> CreateAsync(List<CakeExtraCreateModel> models);
    Task<CakeExtraOption> UpdateAsync(Guid id, CakeExtraUpdateModel model);
    Task<CakeExtraOption> GetByIdAsync(Guid id);
    Task<(Pagination, List<CakeExtraOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeExtraOption, bool>>? filter = null);
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


    private async Task<List<CakeExtraOption>?> GetListDefaultAsync(List<string> types)
    {
        var cake_extras = await _unitOfWork.CakeExtraOptionRepository
                    .WhereAsync(x =>
                        x.IsDefault &&
                        types.Contains(x.Type) &&
                        x.BakeryId == _claimsService.GetCurrentUser
                    );

        return cake_extras.Count != 0 ? cake_extras : null;
    }

    public async Task<List<CakeExtraOption>> CreateAsync(List<CakeExtraCreateModel> models)
    {
        var extras = _mapper.Map<List<CakeExtraOption>>(models);

        var default_extras = await GetListDefaultAsync([.. models.Select(x => x.Type)]);

        foreach (var extra in extras)
        {
            if (default_extras != null && (default_extras.Any(x => x.Type == extra.Type) && extra.IsDefault))
                throw new BadRequestException($"Type {extra.Type} already has default value!");

            extra.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakeExtraOptionRepository.AddRangeAsync(extras);

        await _unitOfWork.SaveChangesAsync();

        return extras;
    }

    public async Task DeleteAsync(Guid id)
    {
        var extra = await GetByIdAsync(id);
        if (extra.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to delete");

        _unitOfWork.CakeExtraOptionRepository.SoftRemove(extra);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination, List<CakeExtraOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeExtraOption, bool>>? filter = null)
    {

        return await _unitOfWork.CakeExtraOptionRepository.ToPagination(pageIndex, pageSize, includes: x => x.Image!, filter: filter);
    }

    public async Task<CakeExtraOption> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakeExtraOptionRepository.GetByIdAsync(id, includes: x => x.Image!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakeExtraOption> UpdateAsync(Guid id, CakeExtraUpdateModel model)
    {

        var extra = await GetByIdAsync(id);
        if (extra.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to update!");

        _mapper.Map(model, extra);

        var default_extras = await GetListDefaultAsync([extra.Type]);

        if (default_extras != null && (default_extras[0].Id != extra.Id && extra.IsDefault))
            throw new BadRequestException($"Type {extra.Type} already has default value!");

        _unitOfWork.CakeExtraOptionRepository.Update(extra);

        await _unitOfWork.SaveChangesAsync();

        return extra;
    }
}
