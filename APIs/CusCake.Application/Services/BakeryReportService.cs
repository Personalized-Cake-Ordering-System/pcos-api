using CusCake.Application.Extensions;
using CusCake.Application.ViewModels.BakeryReportModels.BakeryReports;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IBakeryReportService
{
    Task<OverviewModel> GetBakeryOverviewAsync(Guid bakeryId);
    Task<List<object>> GetBakerySalesOverviewAsync(Guid bakeryId, string type, int year);
    Task<object> GetBakeryProductPerformanceAsync(Guid bakeryId);
    Task<object> GetBakeryCategoryDistributionAsync(Guid bakeryId);
}

public enum SalesOverviewType
{
    REVENUE,
    ORDERS,
    CUSTOMERS
}


public class BakerReportService(IUnitOfWork unitOfWork) : IBakeryReportService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private static readonly DateTime CurrentMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    private static readonly DateTime LastMonth = CurrentMonth.AddMonths(-1);

    public async Task<OverviewModel> GetBakeryOverviewAsync(Guid bakeryId)
    {
        var orders_last_month = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId
            && x.CreatedAt < CurrentMonth
            && x.CreatedAt >= LastMonth
        );

        var orders_current_month = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId
            && x.CreatedAt >= CurrentMonth
        );

        var (total_revenue_metric, orders_metric, customers_metric, average_order_metric) = GetTotalRevenueMetric(orders_last_month, orders_current_month);

        return new OverviewModel
        {
            TotalRevenue = total_revenue_metric,
            Orders = orders_metric,
            Customers = customers_metric,
            AverageOrder = average_order_metric
        };
    }

    private static (MetricModel totalRevenue, MetricModel orders, MetricModel customers, MetricModel averageOrder) GetTotalRevenueMetric(List<Order> orders_last_month, List<Order> orders_current_month)
    {
        var count_last_month = 0;
        var count_current_month = 0;
        var total_revenue_last_month = 0.0;
        var total_revenue_current_month = 0.0;

        var customers_id_last_month = new List<Guid>();
        var customers_id_current_month = new List<Guid>();

        foreach (var order in orders_last_month)
        {
            if (order.OrderStatus == OrderStatusConstants.COMPLETED)
            {
                total_revenue_last_month += order.ShopRevenue;
                count_last_month++;
                if (!customers_id_last_month.Contains(order.CustomerId))
                    customers_id_last_month.Add(order.CustomerId);
            }
        }

        foreach (var order in orders_current_month)
        {
            if (order.OrderStatus == OrderStatusConstants.COMPLETED)
            {
                total_revenue_current_month += order.ShopRevenue;
                count_current_month++;
                if (!customers_id_current_month.Contains(order.CustomerId))
                    customers_id_current_month.Add(order.CustomerId);
            }
        }
        var total_customers_last_month = customers_id_last_month.Count;
        var total_customers_current_month = customers_id_current_month.Count;

        var total_revenue_metric = new MetricModel
        {
            Amount = total_revenue_current_month,
            Change = total_revenue_last_month != 0
                ? ((total_revenue_current_month - total_revenue_last_month) / total_revenue_last_month) * 100
                : 0
        };

        var orders_metric = new MetricModel
        {
            Amount = count_current_month,
            Change = count_last_month != 0
                ? ((count_current_month - count_last_month) / (double)count_last_month) * 100
                : 0
        };

        var customers_metric = new MetricModel
        {
            Amount = total_customers_current_month,
            Change = total_customers_last_month != 0
                ? ((total_customers_current_month - total_customers_last_month) / (double)total_customers_last_month) * 100
                : 0
        };

        var average_order_last_month = count_last_month != 0 ? total_revenue_last_month / count_last_month : 0;
        var average_order_current_month = count_current_month != 0 ? total_revenue_current_month / count_current_month : 0;

        var average_order_metric = new MetricModel
        {
            Amount = average_order_current_month,
            Change = average_order_last_month != 0
                ? ((average_order_current_month - average_order_last_month) / average_order_last_month) * 100
                : 0
        };

        return (total_revenue_metric, orders_metric, customers_metric, average_order_metric);
    }

    public async Task<List<object>> GetBakerySalesOverviewAsync(Guid bakeryId, string type, int year)
    {
        var orders = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId
            && x.CreatedAt.Year == year
            && x.OrderStatus == OrderStatusConstants.COMPLETED
        );

        var result = new List<object>();

        for (int month = 1; month <= 12; month++)
        {
            var monthlyOrders = orders.Where(x => x.CreatedAt.Month == month);

            if (type == "REVENUE")
            {
                result.Add(monthlyOrders.Sum(x => x.ShopRevenue));
            }
            else if (type == "ORDERS")
            {
                result.Add(monthlyOrders.Count());
            }
            else if (type == "CUSTOMERS")
            {
                result.Add(monthlyOrders.Select(x => x.CustomerId).Distinct().Count());
            }
        }

        return result;
    }

    public async Task<object> GetBakeryProductPerformanceAsync(Guid bakeryId)
    {
        var includes = QueryHelper.Includes<OrderDetail>(x => x.Order, x => x.AvailableCake!);
        var order_details = await _unitOfWork.OrderDetailRepository.WhereAsync(
            x => x.Order.BakeryId == bakeryId
            && x.AvailableCakeId != null
            && x.Order.OrderStatus == OrderStatusConstants.COMPLETED,
            includes: includes
        );

        var grouped = order_details.GroupBy(x => x.AvailableCakeId)
                   .Select(x => new
                   {
                       Name = x.First().AvailableCake?.AvailableCakeName ?? "Unknown",
                       Quantity = x.Sum(y => y.Quantity)
                   })
                   .OrderByDescending(x => x.Quantity)
                   .Take(10);
        var names = grouped.Select(x => x.Name).Cast<object>().ToList();
        var quantities = grouped.Select(x => x.Quantity).Cast<object>().ToList();
        return new
        {
            cake_names = names,
            cake_quantities = quantities
        };
    }

    public async Task<object> GetBakeryCategoryDistributionAsync(Guid bakeryId)
    {
        var available_cakes = await _unitOfWork.AvailableCakeRepository.WhereAsync(x => x.BakeryId == bakeryId);
        var custom_cakes = await _unitOfWork.CustomCakeRepository.WhereAsync(x => x.BakeryId == bakeryId);
        var grouped = available_cakes.GroupBy(x => x.AvailableCakeType);
        var names = grouped.Select(x => x.First().AvailableCakeType).Cast<object>().ToList();
        var quantities = grouped.Select(x => x.Count()).Cast<object>().ToList();

        names.Add("CUSTOM");
        quantities.Add(custom_cakes.Count);

        return new
        {
            cake_names = names,
            cake_quantities = quantities
        };
    }
}
