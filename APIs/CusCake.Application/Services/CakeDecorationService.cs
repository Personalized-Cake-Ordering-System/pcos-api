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
    Task<List<CakeDecoration>> CreateAsync(List<CakeDecorationCreateModel> models);
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

    public async Task<List<CakeDecoration>> CreateAsync(List<CakeDecorationCreateModel> models)
    {
        var decorations = _mapper.Map<List<CakeDecoration>>(models);

        foreach (var decoration in decorations)
        {
            decoration.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakeDecorationRepository.AddRangeAsync(decorations);

        await _unitOfWork.SaveChangesAsync();

        return decorations;
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

        _unitOfWork.CakeDecorationRepository.Update(decoration);

        await _unitOfWork.SaveChangesAsync();

        return decoration;
    }
}
