// using System.Linq.Expressions;
// using AutoMapper;
// using CusCake.Application.Extensions;
// using CusCake.Application.GlobalExceptionHandling.Exceptions;
// using CusCake.Application.Services.IServices;
// using CusCake.Application.Utils;
// using CusCake.Application.ViewModels.CakeMessageModels;
// using CusCake.Domain.Entities;

// namespace CusCake.Application.Services;

// public interface ICakeMessageService
// {
//     Task<List<CakeMessage>> CreateAsync(List<CakeMessageCreateModel> model);
//     Task<CakeMessage> UpdateAsync(Guid id, CakeMessageUpdateModel model);
//     Task<CakeMessage> GetByIdAsync(Guid id);
//     Task<(Pagination<CakeMessage>, List<CakeMessage>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeMessage, bool>>? filter = null);
//     Task DeleteAsync(Guid id);
// }

// public class CakeMessageService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService) : ICakeMessageService
// {

//     private readonly IUnitOfWork _unitOfWork = unitOfWork;
//     private readonly IMapper _mapper = mapper;
//     private readonly IClaimsService _claimsService = claimsService;

//     public async Task<List<CakeMessage>> CreateAsync(List<CakeMessageCreateModel> models)
//     {
//         var cakeMessages = _mapper.Map<List<CakeMessage>>(models);

//         foreach (var item in cakeMessages)
//         {
//             item.BakeryId = _claimsService.GetCurrentUser;
//         }

//         await _unitOfWork.CakeMessageRepository.AddRangeAsync(cakeMessages);
//         await _unitOfWork.SaveChangesAsync();

//         return cakeMessages;
//     }

//     public async Task DeleteAsync(Guid id)
//     {
//         var cakeMessage = await GetByIdAsync(id) ?? throw new BadRequestException("Id is not found!");

//         _unitOfWork.CakeMessageRepository.SoftRemove(cakeMessage);

//         await _unitOfWork.SaveChangesAsync();
//     }

//     public async Task<(Pagination<CakeMessage>, List<CakeMessage>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeMessage, bool>>? filter = null)
//     {
//         Expression<Func<CakeMessage, bool>> combinedFilter = filter ?? (x => true);

//         Expression<Func<CakeMessage, bool>> idFilter = x => x.BakeryId == _claimsService.GetCurrentUser;
//         combinedFilter = FilterCustom.CombineFilters(combinedFilter, idFilter);

//         var includes = QueryHelper.Includes<CakeMessage>(x => x.MessageImage!, x => x.CakeMessageTypes!);

//         return await _unitOfWork.CakeMessageRepository.ToPagination(pageIndex, pageSize, includes: includes, filter: combinedFilter);
//     }

//     public async Task<CakeMessage> GetByIdAsync(Guid id)
//     {
//         var includes = QueryHelper.Includes<CakeMessage>(x => x.MessageImage!, x => x.CakeMessageTypes!);

//         return await _unitOfWork.CakeMessageRepository.GetByIdAsync(id, includes: includes) ?? throw new BadRequestException("Id is not exist!");

//     }

//     public async Task<CakeMessage> UpdateAsync(Guid id, CakeMessageUpdateModel model)
//     {
//         var cakeMessage = await GetByIdAsync(id);

//         if (cakeMessage.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");


//         _mapper.Map(model, cakeMessage);

//         _unitOfWork.CakeMessageRepository.Update(cakeMessage);

//         await _unitOfWork.SaveChangesAsync();

//         return cakeMessage;
//     }
// }
