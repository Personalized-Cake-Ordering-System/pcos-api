using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using CusCake.Domain.Enums;

namespace CusCake.Application.Services;

public interface ICakePartService
{
    Task<List<CakePartOption>> CreateAsync(List<CakePartCreateModel> models);
    Task<CakePartOption> UpdateAsync(Guid id, CakePartUpdateModel model);
    Task<CakePartOption> GetByIdAsync(Guid id);
    Task<(Pagination, List<CakePartOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakePartOption, bool>>? filter = null);
    Task DeleteAsync(Guid id);
}

public class CakePartService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService,
                            IFileService fileService
                        ) : ICakePartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IFileService _fileService = fileService;
    private readonly IClaimsService _claimsService = claimsService;


    private async Task<List<CakePartOption>?> GetListDefaultAsync(List<string> types)
    {
        var requiredTypes = types
            .Select(type => Enum.TryParse<CakePartTypeEnum>(type, out var enumType) ? enumType : (CakePartTypeEnum?)null)
            .Where(enumType => enumType.HasValue && CakePartTypeConstants.IsRequired(enumType.Value))
            .Select(enumType => enumType!.Value.ToString())
            .ToList();
        var cake_parts = await _unitOfWork.CakePartOptionRepository
                    .WhereAsync(x =>
                        x.IsDefault &&
                        requiredTypes.Contains(x.Type) &&
                        x.BakeryId == _claimsService.GetCurrentUser
                    );

        return cake_parts.Count != 0 ? cake_parts : null;
    }


    public async Task<List<CakePartOption>> CreateAsync(List<CakePartCreateModel> models)
    {
        var parts = _mapper.Map<List<CakePartOption>>(models);

        var default_parts = await GetListDefaultAsync([.. models.Select(x => x.Type)]);

        foreach (var part in parts)
        {
            if (default_parts != null && (default_parts.Any(x => x.Type == part.Type) && part.IsDefault))
                throw new BadRequestException($"Type {part.Type} already has default value!");

            part.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakePartOptionRepository.AddRangeAsync(parts);

        await _unitOfWork.SaveChangesAsync();

        return parts;
    }

    public async Task DeleteAsync(Guid id)
    {
        var part = await GetByIdAsync(id);

        _unitOfWork.CakePartOptionRepository.SoftRemove(part);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination, List<CakePartOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakePartOption, bool>>? filter = null)
    {

        return await _unitOfWork.CakePartOptionRepository.ToPagination(pageIndex, pageSize, includes: x => x.Image!, filter: filter);
    }

    public async Task<CakePartOption> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakePartOptionRepository.GetByIdAsync(id, includes: x => x.Image!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakePartOption> UpdateAsync(Guid id, CakePartUpdateModel model)
    {
        var part = await GetByIdAsync(id);
        _mapper.Map(model, part);

        if (part.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

        var default_parts = await GetListDefaultAsync([part.Type]);

        if (default_parts != null && (default_parts[0].Id != part.Id && part.IsDefault))
            throw new BadRequestException($"Type {part.Type} already has default value!");

        _unitOfWork.CakePartOptionRepository.Update(part);
        await _unitOfWork.SaveChangesAsync();

        return part;
    }
}
