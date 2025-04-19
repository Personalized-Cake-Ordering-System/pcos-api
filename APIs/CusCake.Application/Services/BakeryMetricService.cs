using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IBakeryMetricService
{
    Task ReCalculateBakeryMetricsAsync(Guid bakeryId);
}

public class BakeryMetricService(IUnitOfWork unitOfWork) : IBakeryMetricService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ReCalculateBakeryMetricsAsync(Guid bakeryId)
    {
        var order = await _unitOfWork.OrderRepository.WhereAsync(o => o.BakeryId == bakeryId && o.OrderStatus == OrderStatusConstants.COMPLETED);
        var review = await _unitOfWork.ReviewRepository.WhereAsync(r => r.BakeryId == bakeryId && r.ReviewType == ReviewTypeConstants.BAKERY_REVIEW);

        var metric = await _unitOfWork.BakeryMetricRepository.FirstOrDefaultAsync(b => b.BakeryId == bakeryId);
        if (metric == null)
        {
            metric = new BakeryMetric
            {
                BakeryId = bakeryId,
                TotalRevenue = order.Sum(o => o.ShopRevenue),
                OrdersCount = order.Count,
                RatingAverage = review.Count > 0 ? review.Average(r => r.Rating) : 0,
                CustomersCount = order.Select(o => o.CustomerId).Distinct().Count(),
                AppRevenue = order.Sum(o => o.AppCommissionFee),
                AverageOrderValue = order.Count > 0 ? order.Sum(o => o.ShopRevenue) / order.Count : 0
            };
            await _unitOfWork.BakeryMetricRepository.AddAsync(metric);
        }
        else
        {
            metric.TotalRevenue = order.Sum(o => o.ShopRevenue);
            metric.OrdersCount = order.Count;
            metric.RatingAverage = review.Count > 0 ? review.Average(r => r.Rating) : 0;
            metric.CustomersCount = order.Select(o => o.CustomerId).Distinct().Count();
            metric.AverageOrderValue = order.Count > 0 ? order.Sum(o => o.ShopRevenue) / order.Count : 0;
            metric.AppRevenue = order.Sum(o => o.AppCommissionFee);

            _unitOfWork.BakeryMetricRepository.Update(metric);
        }
        await _unitOfWork.SaveChangesAsync();
    }
}