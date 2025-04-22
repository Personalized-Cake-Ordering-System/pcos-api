using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAvailableCakeMetricService
{
    Task CalculateAvailableCakeMetricsAsync(Guid cakeId);
    Task CalculateAvailableCakeMetricsByOrderIdAsync(Guid orderId);
}

public class AvailableCakeMetricService(
    IUnitOfWork unitOfWork) : IAvailableCakeMetricService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task CalculateAvailableCakeMetricsByOrderIdAsync(Guid orderId)
    {
        var order_details = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.Id == orderId & x.AvailableCakeId != null);
        if (order_details == null || order_details.Count == 0) return;
        var cakeIds = order_details.Select(x => x.AvailableCakeId).Distinct().ToList();
        foreach (var id in cakeIds)
        {
            await CalculateAvailableCakeMetricsAsync(id!.Value);
        }
    }

    public async Task CalculateAvailableCakeMetricsAsync(Guid cakeId)
    {
        var metric = await _unitOfWork.AvailableCakeMetricRepository.FirstOrDefaultAsync(x => x.AvailableCakeId == cakeId);
        var reviews = await _unitOfWork.ReviewRepository.WhereAsync(r => r.AvailableCakeId == cakeId);
        var order_details = await _unitOfWork.OrderDetailRepository.WhereAsync(x => x.AvailableCakeId == cakeId);
        if (metric == null)
        {
            var newMetric = new AvailableCakeMetric
            {
                AvailableCakeId = cakeId,
                RatingAverage = reviews?.Count > 0 ? reviews.Average(x => x.Rating) : 0,
                ReviewsCount = reviews?.Count > 0 ? reviews.Count : 0,
                QuantitySold = order_details?.Sum(x => x.Quantity) ?? 0
            };
            await _unitOfWork.AvailableCakeMetricRepository.AddAsync(newMetric);
        }
        else
        {
            metric.RatingAverage = reviews?.Count > 0 ? reviews.Average(x => x.Rating) : metric.RatingAverage;
            metric.ReviewsCount = reviews?.Count > 0 ? reviews.Count : metric.ReviewsCount;
            metric.QuantitySold = order_details?.Sum(x => x.Quantity) ?? metric.QuantitySold;
            _unitOfWork.AvailableCakeMetricRepository.Update(metric);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
