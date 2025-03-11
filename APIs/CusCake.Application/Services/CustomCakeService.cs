using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Services.IServices;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Application.ViewModels.CustomCakeModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using CusCake.Domain.Enums;

namespace CusCake.Application.Services;

public interface ICustomCakeService
{
    Task<CustomCake> CreateAsync(CustomCakeCreateModel model);

    Task<(Pagination, List<CustomCake>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CustomCake, bool>>? filter = null);

}

public class CustomCakeService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService) : ICustomCakeService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IClaimsService _claimsService = claimsService;



    public async Task<CustomCake> CreateAsync(CustomCakeCreateModel model)
    {

        var custom_cake = _mapper.Map<CustomCake>(model);

        var messageSelection = await HandleMessageSelection(custom_cake.Id, model.MessageSelectionModel!);
        custom_cake.MessageSelectionId = messageSelection.Item2.Id;
        custom_cake.Price += messageSelection.Item1;
        custom_cake.Price += await HandlePartSelection(custom_cake.Id, custom_cake.BakeryId, model.PartSelectionModels!);
        custom_cake.Price += await HandleDecorationSelection(custom_cake.Id, custom_cake.BakeryId, model.DecorationSelectionModels!);
        custom_cake.Price += await HandleExtraSelection(custom_cake.Id, custom_cake.BakeryId, model.ExtraSelectionModels!);

        custom_cake.CustomerId = _claimsService.GetCurrentUser;

        var result = await _unitOfWork.CustomCakeRepository.AddAsync(custom_cake);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    private async Task<(double, CakeMessageSelection)> HandleMessageSelection(Guid cusCakeId, MessageSelection selection)
    {
        if (Enum.TryParse(selection.MessageType, out CakeMessageTypeEnum messageType))
        {
            var price = CakeMessageTypeConstants.GetPrice(messageType);
            var message = _mapper.Map<CakeMessageSelection>(selection);
            if (selection.CakeMessageOptionIds != null && selection.CakeMessageOptionIds.Count != 0)
                message.MessageOptions = await _unitOfWork.CakeMessageOptionRepository.WhereAsync(x => selection.CakeMessageOptionIds.Contains(x.Id));
            await _unitOfWork.CakeMessageSelectionRepository.AddAsync(message);
            return (price, message);
        }

        throw new Exception("Error at handle message");
    }

    private async Task<double> HandlePartSelection(Guid cusCakeId, Guid bakeryId, List<PartSelection> selections)
    {
        double total_price = 0;

        // Lấy danh sách các loại phần bắt buộc từ constants
        var requiredTypes = CakePartTypeConstants.CakePartTypes
            .Where(kv => kv.Value.IsRequired)
            .Select(kv => kv.Key.ToString())
            .ToList();

        if (selections == null || selections.Count == 0)
        {
            return await HandleDefaultPartSelection(cusCakeId, bakeryId, requiredTypes);
        }

        // Lấy danh sách các loại đã được chọn trong PartSelections
        var selectedTypes = selections
            .Select(ps => Enum.TryParse(ps.Type, out CakePartTypeEnum type) ? type : (CakePartTypeEnum?)null)
            .Where(type => type.HasValue)
            .Select(type => type!.Value.ToString())
            .ToHashSet();

        // Lọc ra các loại required chưa có trong PartSelections
        var missingRequiredTypes = requiredTypes
            .Where(type => !selectedTypes.Contains(type))
            .ToList();

        total_price += missingRequiredTypes.Count > 0 ?
                        await HandleDefaultPartSelection(cusCakeId, bakeryId, missingRequiredTypes) : 0;

        total_price += await HandleSelectedPartSelection(cusCakeId, bakeryId, selections);

        return total_price;
    }

    private async Task<double> HandleDefaultPartSelection(Guid cusCakeId, Guid bakeryId, List<string> types)
    {
        double total_price = 0;
        var list_options = new List<CakePartSelection>();

        var options = await _unitOfWork
                    .CakePartOptionRepository.WhereAsync(x =>
                        types.Contains(x.Type) &&
                        x.BakeryId == bakeryId &&
                        x.IsDefault
                    );

        foreach (var option in options)
        {
            list_options.Add(new CakePartSelection
            {
                CustomCakeId = cusCakeId,
                PartOptionId = option.Id,
                PartType = option.Type,
            });

            total_price += (double)option.Price;
        }
        await _unitOfWork.CakePartSelectionRepository.AddRangeAsync(list_options);

        return total_price;
    }

    private async Task<double> HandleSelectedPartSelection(Guid cusCakeId, Guid bakeryId, List<PartSelection> selections)
    {
        double total_price = 0;
        var list_options = new List<CakePartSelection>();
        var optionIds = selections.Select(x => x.OptionId).ToList();

        var options = await _unitOfWork
                    .CakePartOptionRepository.WhereAsync(x =>
                        optionIds.Contains(x.Id) &&
                        x.BakeryId == bakeryId
                    );

        foreach (var option in options)
        {
            list_options.Add(new CakePartSelection
            {
                CustomCakeId = cusCakeId,
                PartOptionId = option.Id,
                PartType = option.Type,
            });

            total_price += (double)option.Price;
        }
        await _unitOfWork.CakePartSelectionRepository.AddRangeAsync(list_options);

        return total_price;
    }

    private async Task<double> HandleExtraSelection(Guid cusCakeId, Guid bakeryId, List<ExtraSelection> selections)
    {
        double total_price = 0;
        var list_options = new List<CakeExtraSelection>();
        var optionIds = selections.Select(x => x.OptionId).ToList();

        var options = await _unitOfWork
                    .CakeExtraOptionRepository.WhereAsync(x =>
                        optionIds.Contains(x.Id) &&
                        x.BakeryId == bakeryId
                    );

        foreach (var option in options)
        {
            list_options.Add(new CakeExtraSelection
            {
                CustomCakeId = cusCakeId,
                ExtraOptionId = option.Id,
                ExtraType = option.Type,
            });

            total_price += (double)option.Price;
        }
        await _unitOfWork.CakeExtraSelectionRepository.AddRangeAsync(list_options);

        return total_price;
    }

    private async Task<double> HandleDecorationSelection(Guid cusCakeId, Guid bakeryId, List<DecorationSelection> selections)
    {
        double total_price = 0;
        var list_options = new List<CakeDecorationSelection>();
        var optionIds = selections.Select(x => x.OptionId).ToList();

        var options = await _unitOfWork
                    .CakeDecorationOptionRepository.WhereAsync(x =>
                        optionIds.Contains(x.Id) &&
                        x.BakeryId == bakeryId
                    );

        foreach (var option in options)
        {
            list_options.Add(new CakeDecorationSelection
            {
                CustomCakeId = cusCakeId,
                DecorationOptionId = option.Id,
                DecorationType = option.Type,
            });

            total_price += (double)option.Price;
        }
        await _unitOfWork.CakeDecorationSelectionRepository.AddRangeAsync(list_options);

        return total_price;
    }

    public async Task<(Pagination, List<CustomCake>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<CustomCake, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<CustomCake>(x =>
                            x.MessageSelection!,
                            x => x.PartSelections!,
                            x => x.DecorationSelections!,
                            x => x.ExtraSelections!,
                            x => x.Customer,
                            x => x.Bakery);

        return await _unitOfWork.CustomCakeRepository.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
    }
}
