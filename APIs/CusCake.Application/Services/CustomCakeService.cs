// using System.Threading.Tasks;
// using AutoMapper;
// using CusCake.Application.Services.IServices;
// using CusCake.Application.ViewModels.CakeMessageModels;
// using CusCake.Application.ViewModels.CustomCakeModels;
// using CusCake.Domain.Entities;

// namespace CusCake.Application.Services;

// public interface ICustomCakeService
// {
//     Task<CustomCake> CreateAsync(CustomCakeCreateModel model);
// }

// public class CustomCakeService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService) : ICustomCakeService
// {

//     private readonly IUnitOfWork _unitOfWork = unitOfWork;
//     private readonly IMapper _mapper = mapper;
//     private readonly IClaimsService _claimsService = claimsService;



//     public Task<CustomCake> CreateAsync(CustomCakeCreateModel model)
//     {

//         var custom_cake = _mapper.Map<CustomCake>(model);


//         throw new NotImplementedException();
//     }


//     private async Task<List<CakeMessageDetail>?> GenerateCakeMessage(CakeMessageCreateDetail model)
//     {
//         var cake_message = await _unitOfWork.CakeMessageDetailRepository.GetByIdAsync(model.CakeMessageId);


//         return null;
//     }
// }
