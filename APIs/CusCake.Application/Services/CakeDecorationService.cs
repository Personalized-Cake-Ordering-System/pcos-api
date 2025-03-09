// using System.Linq.Expressions;
// using AutoMapper;
// using CusCake.Application.GlobalExceptionHandling.Exceptions;
// using CusCake.Application.Services.IServices;
// using CusCake.Application.Utils;
// using CusCake.Application.ViewModels.CakeDecorationModels;
// using CusCake.Domain.Entities;

// namespace CusCake.Application.Services;

// public interface ICakeDecorationService
// {
//     Task<List<CakeDecoration>> CreateAsync(List<CakeDecorationCreateModel> models);
//     Task<CakeDecoration> UpdateAsync(Guid id, CakeDecorationUpdateModel model);
//     Task<CakeDecoration> GetByIdAsync(Guid id);
//     Task<(Pagination<CakeDecoration>, List<CakeDecoration>)> GetAllAsync(
//         int pageIndex = 0,
//         int pageSize = 10,
//         Expression<Func<CakeDecoration, bool>>? filter = null
//     );
//     Task DeleteAsync(Guid id);
// }

// public class CakeDecorationService(IUnitOfWork unitOfWork,
//                             IMapper mapper,
//                             IClaimsService claimsService,
//                             IFileService fileService
//                         ) : ICakeDecorationService
// {
//     private readonly IUnitOfWork _unitOfWork = unitOfWork;
//     private readonly IMapper _mapper = mapper;
//     private readonly IFileService _fileService = fileService;
//     private readonly IClaimsService _claimsService = claimsService;


//     private async Task<List<CakeDecoration>?> GetListDefaultAsync(List<string> types)
//     {
//         var cake_decorations = await _unitOfWork.CakeDecorationRepository
//                     .WhereAsync(x =>
//                         x.IsDefault &&
//                         types.Contains(x.DecorationType) &&
//                         x.BakeryId == _claimsService.GetCurrentUser
//                     );

//         return cake_decorations.Count != 0 ? cake_decorations : null;
//     }



//     public async Task<List<CakeDecoration>> CreateAsync(List<CakeDecorationCreateModel> models)
//     {
//         var decorations = _mapper.Map<List<CakeDecoration>>(models);

//         var default_decorations = await GetListDefaultAsync([.. models.Select(x => x.DecorationType)]);

//         foreach (var decoration in decorations)
//         {
//             if (default_decorations != null && default_decorations.Any(x => x.DecorationType == decoration.DecorationType))
//                 throw new BadRequestException($"Type {decoration.DecorationType} already has default value!");

//             decoration.BakeryId = _claimsService.GetCurrentUser;
//         }

//         await _unitOfWork.CakeDecorationRepository.AddRangeAsync(decorations);

//         await _unitOfWork.SaveChangesAsync();

//         return decorations;
//     }

//     public async Task DeleteAsync(Guid id)
//     {
//         var decoration = await GetByIdAsync(id);

//         _unitOfWork.CakeDecorationRepository.SoftRemove(decoration);
//         await _unitOfWork.SaveChangesAsync();

//     }

//     public async Task<(Pagination<CakeDecoration>, List<CakeDecoration>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CakeDecoration, bool>>? filter = null)
//     {
//         return await _unitOfWork.CakeDecorationRepository.ToPagination(pageIndex, pageSize, includes: x => x.DecorationImage!, filter: filter);
//     }

//     public async Task<CakeDecoration> GetByIdAsync(Guid id)
//     {
//         return await _unitOfWork.CakeDecorationRepository.GetByIdAsync(id, includes: x => x.DecorationImage!) ?? throw new BadRequestException("Id not found!");
//     }

//     public async Task<CakeDecoration> UpdateAsync(Guid id, CakeDecorationUpdateModel model)
//     {
//         var decoration = await GetByIdAsync(id);

//         if (decoration.BakeryId != _claimsService.GetCurrentUser) throw new BadRequestException("No permission to edit!");

//         _mapper.Map(model, decoration);

//         var default_decorations = await GetListDefaultAsync([decoration.DecorationType]);

//         if (default_decorations != null && default_decorations[0].Id != decoration.Id)
//             throw new BadRequestException($"Type {decoration.DecorationType} already has default value!");

//         _unitOfWork.CakeDecorationRepository.Update(decoration);

//         await _unitOfWork.SaveChangesAsync();

//         return decoration;
//     }
// }
