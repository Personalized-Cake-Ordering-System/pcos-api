using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICakeMessageService
{
    Task<List<CakeMessageOption>> CreateAsync(List<CakeMessageOptionCreateModel> model);
    Task<CakeMessageOption> UpdateAsync(Guid id, CakeMessageOptionUpdateModel model);
    Task<CakeMessageOption> GetByIdAsync(Guid id);
    Task<(Pagination, List<CakeMessageOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeMessageOption, bool>>? filter = null);
    Task DeleteAsync(Guid id);
}

public class CakeMessageService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService) : ICakeMessageService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<List<CakeMessageOption>> CreateAsync(List<CakeMessageOptionCreateModel> models)
    {
        var cakeMessages = _mapper.Map<List<CakeMessageOption>>(models);

        foreach (var item in cakeMessages)
        {
            item.BakeryId = _claimsService.GetCurrentUser;
        }

        await _unitOfWork.CakeMessageOptionRepository.AddRangeAsync(cakeMessages);
        await _unitOfWork.SaveChangesAsync();

        return cakeMessages;
    }

    public async Task DeleteAsync(Guid id)
    {
        var cakeMessage = await GetByIdAsync(id) ?? throw new BadRequestException("Id is not found!");

        _unitOfWork.CakeMessageOptionRepository.SoftRemove(cakeMessage);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Pagination, List<CakeMessageOption>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeMessageOption, bool>>? filter = null)
    {

        return await _unitOfWork.CakeMessageOptionRepository.ToPagination(pageIndex, pageSize, filter: filter);
    }

    public async Task<CakeMessageOption> GetByIdAsync(Guid id)
    {

        return await _unitOfWork.CakeMessageOptionRepository.GetByIdAsync(id) ?? throw new BadRequestException("Id is not exist!");

    }

    public async Task<CakeMessageOption> UpdateAsync(Guid id, CakeMessageOptionUpdateModel model)
    {
        var cakeMessage = await GetByIdAsync(id);

        if (cakeMessage.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");


        _mapper.Map(model, cakeMessage);

        _unitOfWork.CakeMessageOptionRepository.Update(cakeMessage);

        await _unitOfWork.SaveChangesAsync();

        return cakeMessage;
    }
}
