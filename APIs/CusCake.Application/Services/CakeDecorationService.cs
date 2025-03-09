using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakeDecorationService
{
    Task<List<CakeDecorationOption>> CreateAsync(List<CakeDecorationCreateModel> models);
    Task<CakeDecorationOption> UpdateAsync(Guid id, CakeDecorationUpdateModel model);
    Task<CakeDecorationOption> GetByIdAsync(Guid id);
    Task<(Pagination, List<CakeDecorationOption>)> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        Expression<Func<CakeDecorationOption, bool>>? filter = null
    );
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


    private async Task<List<CakeDecorationOption>?> GetListDefaultAsync(List<string> types)
    {
        var cake_decorations = await _unitOfWork.CakeDecorationOptionRepository
                    .WhereAsync(x =>
                        x.IsDefault &&
                        types.Contains(x.Type) &&
                        x.BakeryId == _claimsService.GetCurrentUser
                    );

        return cake_decorations.Count != 0 ? cake_decorations : null;
    }



    public async Task<List<CakeDecorationOption>> CreateAsync(List<CakeDecorationCreateModel> models)
    {
        var decorations = _mapper.Map<List<CakeDecorationOption>>(models);

        var default_decorations = await GetListDefaultAsync([.. models.Select(x => x.Type)]);

        foreach (var decoration in decorations)
        {
            if (default_decorations != null && (default_decorations.Any(x => x.Type == decoration.Type) && decoration.IsDefault))
                throw new BadRequestException($"Type {decoration.Type} already has default value!");

            decoration.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakeDecorationOptionRepository.AddRangeAsync(decorations);

        await _unitOfWork.SaveChangesAsync();

        return decorations;
    }

    public async Task DeleteAsync(Guid id)
    {
        var decoration = await GetByIdAsync(id);

        _unitOfWork.CakeDecorationOptionRepository.SoftRemove(decoration);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination, List<CakeDecorationOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeDecorationOption, bool>>? filter = null)
    {
        return await _unitOfWork.CakeDecorationOptionRepository.ToPagination(pageIndex, pageSize, includes: x => x.Image!, filter: filter);
    }

    public async Task<CakeDecorationOption> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakeDecorationOptionRepository.GetByIdAsync(id, includes: x => x.Image!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakeDecorationOption> UpdateAsync(Guid id, CakeDecorationUpdateModel model)
    {
        var decoration = await GetByIdAsync(id);

        if (decoration.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

        _mapper.Map(model, decoration);

        var default_decorations = await GetListDefaultAsync([decoration.Type]);

        if (default_decorations != null && (default_decorations[0].Id != decoration.Id && decoration.IsDefault))
            throw new BadRequestException($"Type {decoration.Type} already has default value!");

        _unitOfWork.CakeDecorationOptionRepository.Update(decoration);

        await _unitOfWork.SaveChangesAsync();

        return decoration;
    }
}
