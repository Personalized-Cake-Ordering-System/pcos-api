using CusCake.Application.Extensions;
using CusCake.Application.ViewModels.BakeryReportModels.BakeryReports;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IBakeryDashboardService
{
    Task<OverviewModel> GetBakeryOverviewAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<List<object>> GetBakerySalesOverviewAsync(Guid bakeryId, string type, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<object> GetBakeryProductPerformanceAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<object> GetBakeryCategoryDistributionAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null);
}

public enum SalesOverviewType
{
    REVENUE,
    ORDERS,
    CUSTOMERS
}


public class BakeryDashboardService(IUnitOfWork unitOfWork) : IBakeryDashboardService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private static readonly DateTime CurrentMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    private static readonly DateTime LastMonth = CurrentMonth.AddMonths(-1);

    public async Task<OverviewModel> GetBakeryOverviewAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var timePeriod = dateFrom.HasValue && dateTo.HasValue
            ? dateFrom.Value.AddDays((dateTo - dateFrom).Value.TotalDays)
            : LastMonth;

        var orders_last_month = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId
            && x.CreatedAt.Date < (dateFrom ?? CurrentMonth).Date
            && x.CreatedAt.Date >= timePeriod.Date
        );

        var orders_current_month = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId &&
                x.CreatedAt >= (dateFrom ?? CurrentMonth).Date &&
                x.CreatedAt < (dateTo ?? DateTime.MaxValue).Date
        );

        var (total_revenue_metric, orders_metric, customers_metric, average_order_metric) = GetTotalRevenueMetric(orders_last_month, orders_current_month);

        total_revenue_metric.ComparisonPeriod = dateFrom.HasValue && dateTo.HasValue ? "last period time" : "last month";
        orders_metric.ComparisonPeriod = dateFrom.HasValue && dateTo.HasValue ? "last period time" : "last month";
        customers_metric.ComparisonPeriod = dateFrom.HasValue && dateTo.HasValue ? "last period time" : "last month";
        average_order_metric.ComparisonPeriod = dateFrom.HasValue && dateTo.HasValue ? "last period time" : "last month";

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
                ? (total_revenue_current_month - total_revenue_last_month) / total_revenue_last_month * 100
                : 0
        };

        var orders_metric = new MetricModel
        {
            Amount = count_current_month,
            Change = count_last_month != 0
                ? (count_current_month - count_last_month) / (double)count_last_month * 100
                : 0
        };

        var customers_metric = new MetricModel
        {
            Amount = total_customers_current_month,
            Change = total_customers_last_month != 0
                ? (total_customers_current_month - total_customers_last_month) / (double)total_customers_last_month * 100
                : 0
        };

        var average_order_last_month = count_last_month != 0 ? total_revenue_last_month / count_last_month : 0;
        var average_order_current_month = count_current_month != 0 ? total_revenue_current_month / count_current_month : 0;

        var average_order_metric = new MetricModel
        {
            Amount = average_order_current_month,
            Change = average_order_last_month != 0
                ? (average_order_current_month - average_order_last_month) / average_order_last_month * 100
                : 0
        };

        return (total_revenue_metric, orders_metric, customers_metric, average_order_metric);
    }

    public async Task<List<object>> GetBakerySalesOverviewAsync(Guid bakeryId, string type, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var result = new List<object>();

        var orders = await _unitOfWork.OrderRepository.WhereAsync(
            x => x.BakeryId == bakeryId
            && x.OrderStatus == OrderStatusConstants.COMPLETED
        );

        // Kiểm tra nếu có khoảng thời gian dateFrom và dateTo
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            var monthsDifference = ((dateTo.Value.Year - dateFrom.Value.Year) * 12) + dateTo.Value.Month - dateFrom.Value.Month;

            if (monthsDifference > 12)
            {
                // Nếu khoảng cách > 12 tháng thì trả về theo năm
                return GetBakerySalesByYear(orders, type, dateFrom.Value, dateTo.Value);
            }
            else
            {
                // Nếu khoảng cách <= 12 tháng thì trả về theo tháng
                return GetBakerySalesByMonth(orders, type, dateFrom.Value, dateTo.Value);
            }
        }
        return result;
    }

    // Lấy dữ liệu theo năm
    private static List<object> GetBakerySalesByYear(IEnumerable<Order> orders, string type, DateTime dateFrom, DateTime dateTo)
    {
        var result = new List<object>();

        // Lọc theo năm
        var filteredOrders = orders.Where(x => x.CreatedAt.Year >= dateFrom.Year && x.CreatedAt.Year <= dateTo.Year);

        for (int year = dateFrom.Year; year <= dateTo.Year; year++)
        {
            var yearlyOrders = filteredOrders.Where(x => x.CreatedAt.Year == year);

            if (type == "REVENUE")
            {
                result.Add(new { year = year, value = yearlyOrders.Sum(x => x.ShopRevenue) });
            }
            else if (type == "ORDERS")
            {
                result.Add(new { year = year, value = yearlyOrders.Count() });
            }
            else if (type == "CUSTOMERS")
            {
                result.Add(new { year = year, value = yearlyOrders.Select(x => x.CustomerId).Distinct().Count() });
            }
        }

        return result;
    }

    // Lấy dữ liệu theo tháng
    private static List<object> GetBakerySalesByMonth(List<Order> data, string type, DateTime dateFrom, DateTime dateTo)
    {
        var result = new List<object>();

        if (type == "REVENUE")
        {
            var orders = data.Where(
                x =>
                (x.OrderStatus == OrderStatusConstants.COMPLETED || x.OrderStatus == OrderStatusConstants.FAULTY)
                && x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date
            );

            if ((dateTo - dateFrom).TotalDays <= 31)
            {
                var days = Enumerable.Range(0, (dateTo - dateFrom).Days + 1)
                                     .Select(d => dateFrom.AddDays(d).Date);

                foreach (var day in days)
                {
                    var dailyOrders = orders.Where(x => x.CreatedAt.Date == day.Date);
                    result.Add(new { date = day.ToString("yyyy-MM-dd"), value = dailyOrders.Sum(x => x.AppCommissionFee) });
                }
            }
            else
            {

                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyOrders = orders.Where(x => x.CreatedAt.Year == date.Year && x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyOrders.Sum(x => x.AppCommissionFee) });
                }
            }
        }
        else if (type == "ORDERS")
        {
            var orders = data.Where(
                x => x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date
            );

            if ((dateTo - dateFrom).TotalDays <= 31)
            {
                var days = Enumerable.Range(0, (dateTo - dateFrom).Days + 1)
                                     .Select(d => dateFrom.AddDays(d).Date);

                foreach (var day in days)
                {
                    var dailyOrders = orders.Where(x => x.CreatedAt.Date == day.Date);
                    result.Add(new { date = day.ToString("yyyy-MM-dd"), value = dailyOrders.Count() });
                }
            }
            else
            {

                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyOrders = orders.Where(x => x.CreatedAt.Year == date.Year && x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyOrders.Count() });
                }
            }
        }
        else if (type == "CUSTOMERS")
        {
            var orders = data.Where(
                x => x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date
            );

            if ((dateTo - dateFrom).TotalDays <= 31)
            {
                var days = Enumerable.Range(0, (dateTo - dateFrom).Days + 1)
                                     .Select(d => dateFrom.AddDays(d).Date);

                foreach (var day in days)
                {
                    var dailyOrders = orders.Where(x => x.CreatedAt.Date == day.Date);
                    result.Add(new { date = day.ToString("yyyy-MM-dd"), value = dailyOrders.Select(x => x.CustomerId).Distinct().Count() });
                }
            }
            else
            {

                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyOrders = orders.Where(x => x.CreatedAt.Year == date.Year && x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyOrders.Select(x => x.CustomerId).Distinct().Count() });
                }
            }
        }

        return result;
    }


    public async Task<object> GetBakeryProductPerformanceAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var includes = QueryHelper.Includes<OrderDetail>(x => x.Order, x => x.AvailableCake!);
        var order_details = await _unitOfWork.OrderDetailRepository.WhereAsync(
            x => x.Order.BakeryId == bakeryId
            && x.AvailableCakeId != null
            && (x.Order.OrderStatus == OrderStatusConstants.COMPLETED || x.Order.OrderStatus == OrderStatusConstants.FAULTY)
            && x.Order.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date
            && x.Order.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date,
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

    public async Task<object> GetBakeryCategoryDistributionAsync(Guid bakeryId, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var available_cakes = await _unitOfWork.AvailableCakeRepository.WhereAsync(x =>
            x.BakeryId == bakeryId &&
            x.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            x.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date
        );

        var custom_cakes = await _unitOfWork.CustomCakeRepository.WhereAsync(x =>
            x.BakeryId == bakeryId &&
            x.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            x.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date
        );


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
