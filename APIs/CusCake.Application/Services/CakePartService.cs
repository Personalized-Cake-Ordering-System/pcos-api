using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakePartService
{
    Task<List<CakePart>> CreateAsync(List<CakePartCreateModel> models);
    Task<CakePart> UpdateAsync(Guid id, CakePartUpdateModel model);
    Task<CakePart> GetByIdAsync(Guid id);
    Task<(Pagination<CakePart>, List<CakePart>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakePart, bool>>? filter = null);
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

    public async Task<List<CakePart>> CreateAsync(List<CakePartCreateModel> models)
    {
        var parts = _mapper.Map<List<CakePart>>(models);

        foreach (var part in parts)
        {
            part.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakePartRepository.AddRangeAsync(parts);

        await _unitOfWork.SaveChangesAsync();

        return parts;
    }

    public async Task DeleteAsync(Guid id)
    {
        var part = await GetByIdAsync(id);

        _unitOfWork.CakePartRepository.SoftRemove(part);
        await _unitOfWork.SaveChangesAsync();

    }

    public async Task<(Pagination<CakePart>, List<CakePart>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakePart, bool>>? filter = null)
    {
        Expression<Func<CakePart, bool>> combinedFilter = filter ?? (x => true);

        Expression<Func<CakePart, bool>> idFilter = x => x.BakeryId == _claimsService.GetCurrentUser;
        combinedFilter = FilterCustom.CombineFilters(combinedFilter, idFilter);

        return await _unitOfWork.CakePartRepository.ToPagination(pageIndex, pageSize, includes: x => x.PartImage!, filter: combinedFilter);
    }

    public async Task<CakePart> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CakePartRepository.GetByIdAsync(id, includes: x => x.PartImage!) ?? throw new BadRequestException("Id not found!");
    }

    public async Task<CakePart> UpdateAsync(Guid id, CakePartUpdateModel model)
    {
        var part = await GetByIdAsync(id);

        if (part.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

        _mapper.Map(model, part);

        _unitOfWork.CakePartRepository.Update(part);
        await _unitOfWork.SaveChangesAsync();

        return part;
    }
}
