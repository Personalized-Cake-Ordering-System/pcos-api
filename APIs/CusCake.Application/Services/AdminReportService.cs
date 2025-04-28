

using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AdminReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAdminReportService
{
    Task<AdminOverviewModel> GetAdminOverviewModel(DateTime? dateFrom = null, DateTime? dateTo = null);
    Task<(Pagination, List<BakeryMetric>)> GetTopBakeryMetrics(
        int pageIndex = 0,
        int pageSize = 10,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        List<(Expression<Func<BakeryMetric, object>> OrderBy, bool IsDescending)>? orderByList = null
    );

    Task<List<object>> GetAdminChartAsync(string type, DateTime? dateFrom = null, DateTime? dateTo = null);
}

public class AdminReportService(IUnitOfWork unitOfWork) : IAdminReportService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<AdminOverviewModel> GetAdminOverviewModel(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var order_completed = await _unitOfWork.OrderRepository.WhereAsync(o =>
            o.OrderStatus == OrderStatusConstants.COMPLETED &&
            o.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            o.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date
        );

        var bakeries = await _unitOfWork.BakeryRepository.WhereAsync(x =>
            x.Status == BakeryStatusConstants.CONFIRMED &&
            x.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            x.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date
        );

        var available_cakes = await _unitOfWork.AvailableCakeRepository.WhereAsync(x =>
            x.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            x.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date
        );

        var reports = await _unitOfWork.ReportRepository.WhereAsync(x =>
            x.CreatedAt.Date >= (dateFrom ?? DateTime.MinValue).Date &&
            x.CreatedAt.Date <= (dateTo ?? DateTime.MaxValue).Date);

        var model = new AdminOverviewModel
        {
            TotalBakeries = bakeries.Count,
            TotalProducts = available_cakes.Count,
            TotalReports = reports.Count,
            TotalCustomers = order_completed.Select(x => x.CustomerId).Distinct().Count(),
            TotalRevenues = order_completed.Sum(x => x.AppCommissionFee),
        };

        return model;
    }

    public async Task<(Pagination, List<BakeryMetric>)> GetTopBakeryMetrics(
        int pageIndex = 0,
        int pageSize = 10,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        List<(Expression<Func<BakeryMetric, object>> OrderBy, bool IsDescending)>? orderByList = null
    )
    {
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            var orders = await _unitOfWork.OrderRepository.WhereAsync(
                    x =>
                    (x.OrderStatus == OrderStatusConstants.COMPLETED || x.OrderStatus == OrderStatusConstants.FAULTY) &&
                    x.CreatedAt.Date >= dateFrom.Value.Date && x.CreatedAt.Date <= dateTo.Value.Date,
                    includes: x => x.Bakery
                );
            var filterByDate = orders.GroupBy(x => x.BakeryId).Select(async g => new BakeryMetric
            {
                BakeryId = g.Key,
                Bakery = g.FirstOrDefault()!.Bakery,
                TotalRevenue = g.Sum(x => x.ShopRevenue),
                AppRevenue = g.Sum(x => x.AppCommissionFee),
                RatingAverage = await _unitOfWork.ReviewRepository
                                .WhereAsync(x => x.BakeryId == g.Key && x.ReviewType == ReviewTypeConstants.BAKERY_REVIEW)
                                .ContinueWith(task =>
                                {
                                    var reviews = task.Result;
                                    return reviews.Count != 0 ? reviews.Average(r => r.Rating) : 0;
                                }),
                AverageOrderValue = g.Average(x => x.ShopRevenue),
                OrdersCount = g.Count(),
                CustomersCount = g.Select(x => x.CustomerId).Distinct().Count()
            });

            var result = (await Task.WhenAll(filterByDate))
                        .OrderByDescending(x => x.TotalRevenue)
                        .ToList();
            return (new Pagination
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = result.Count,
            }, result.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
        return await _unitOfWork.BakeryMetricRepository.ToPagination(pageIndex, pageSize, orderByList: orderByList, includes: x => x.Bakery);

    }

    public async Task<List<object>> GetAdminChartAsync(string type, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var result = new List<object>();

        // Kiểm tra khoảng cách giữa dateFrom và dateTo
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            var monthsDifference = ((dateTo.Value.Year - dateFrom.Value.Year) * 12) + dateTo.Value.Month - dateFrom.Value.Month;

            if (monthsDifference > 12)
            {
                // Nếu khoảng cách lớn hơn 12 tháng -> Lấy theo năm
                return await GetDataByYear(type, dateFrom.Value, dateTo.Value);
            }
            else
            {
                // Nếu khoảng cách <= 12 tháng -> Lấy theo ngày (hoặc tháng)
                return await GetDataByDayOrMonth(type, dateFrom.Value, dateTo.Value);
            }
        }

        return result;
    }

    // Lấy dữ liệu theo năm nếu khoảng cách > 12 tháng
    private async Task<List<object>> GetDataByYear(string type, DateTime dateFrom, DateTime dateTo)
    {
        var result = new List<object>();

        if (type == "REVENUE")
        {
            var orders = await _unitOfWork.OrderRepository.WhereAsync(
                x => x.OrderStatus == OrderStatusConstants.COMPLETED
                && x.CreatedAt.Year >= dateFrom.Year && x.CreatedAt.Year <= dateTo.Year
            );

            for (int year = dateFrom.Year; year <= dateTo.Year; year++)
            {
                var yearlyOrders = orders.Where(x => x.CreatedAt.Year == year);
                result.Add(new { year = year, value = yearlyOrders.Sum(x => x.AppCommissionFee) });
            }
        }
        else if (type == "BAKERIES")
        {
            var bakeries = await _unitOfWork.BakeryRepository.WhereAsync(x => x.Status != BakeryStatusConstants.BANNED);
            var yearlyBakeries = bakeries.Where(x => x.CreatedAt.Year >= dateFrom.Year && x.CreatedAt.Year <= dateTo.Year);

            for (int year = dateFrom.Year; year <= dateTo.Year; year++)
            {
                var yearlyBakeryCount = yearlyBakeries.Count(x => x.CreatedAt.Year == year);
                result.Add(new { year = year, value = yearlyBakeryCount });
            }
        }
        else if (type == "CUSTOMERS")
        {
            var orders = await _unitOfWork.OrderRepository.WhereAsync(
                x => x.OrderStatus == OrderStatusConstants.COMPLETED
                && x.CreatedAt.Year >= dateFrom.Year && x.CreatedAt.Year <= dateTo.Year
            );

            for (int year = dateFrom.Year; year <= dateTo.Year; year++)
            {
                var yearlyOrders = orders.Where(x => x.CreatedAt.Year == year);
                result.Add(new { year = year, value = yearlyOrders.Select(x => x.CustomerId).Distinct().Count() });
            }
        }
        return result;
    }

    // Lấy dữ liệu theo ngày hoặc tháng (khi khoảng cách <= 12 tháng)
    private async Task<List<object>> GetDataByDayOrMonth(string type, DateTime dateFrom, DateTime dateTo)
    {
        var result = new List<object>();

        if (type == "REVENUE")
        {
            var orders = await _unitOfWork.OrderRepository.WhereAsync(
                x =>
                (x.OrderStatus == OrderStatusConstants.COMPLETED || x.OrderStatus == OrderStatusConstants.FAULTY)
                && x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date
            );

            // Lấy dữ liệu theo ngày (nếu khoảng cách <= 12 tháng)
            if ((dateTo - dateFrom).TotalDays <= 31) // Nếu khoảng cách giữa 2 ngày nhỏ hơn hoặc bằng 31 ngày (tương ứng với một tháng)
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

                // Nếu khoảng cách lớn hơn 31 ngày, lấy theo tháng
                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyOrders = orders.Where(x => x.CreatedAt.Year == date.Year && x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyOrders.Sum(x => x.AppCommissionFee) });
                }
            }
        }
        else if (type == "BAKERIES")
        {
            var bakeries = await _unitOfWork.BakeryRepository.WhereAsync(x => x.Status != BakeryStatusConstants.BANNED);
            var monthlyBakeries = bakeries.Where(x => x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date);

            // Lấy dữ liệu theo ngày (nếu khoảng cách <= 12 tháng)
            if ((dateTo - dateFrom).TotalDays <= 31) // Nếu khoảng cách giữa 2 ngày nhỏ hơn hoặc bằng 31 ngày (tương ứng với một tháng)
            {
                var days = Enumerable.Range(0, (dateTo - dateFrom).Days + 1)
                                     .Select(d => dateFrom.AddDays(d).Date);

                foreach (var day in days)
                {
                    var dailyBakeryCount = monthlyBakeries.Count(x => x.CreatedAt.Date == day);
                    result.Add(new { date = day.ToString("yyyy-MM-dd"), value = dailyBakeryCount });
                }
            }
            else
            {
                // Nếu khoảng cách lớn hơn 31 ngày, lấy theo tháng
                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyBakeryCount = monthlyBakeries.Count(x => x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyBakeryCount });
                }
            }
        }
        else if (type == "CUSTOMERS")
        {
            var orders = await _unitOfWork.OrderRepository.WhereAsync(
                x => x.OrderStatus == OrderStatusConstants.COMPLETED
                && x.CreatedAt.Date >= dateFrom.Date && x.CreatedAt.Date <= dateTo.Date
            );

            // Lấy dữ liệu theo ngày (nếu khoảng cách <= 12 tháng)
            if ((dateTo - dateFrom).TotalDays <= 31) // Nếu khoảng cách giữa 2 ngày nhỏ hơn hoặc bằng 31 ngày (tương ứng với một tháng)
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
                // Nếu khoảng cách lớn hơn 31 ngày, lấy theo tháng
                for (var date = new DateTime(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
                {
                    var monthlyOrders = orders.Where(x => x.CreatedAt.Month == date.Month);
                    result.Add(new { month = date.ToString("yyyy-MM"), value = monthlyOrders.Select(x => x.CustomerId).Distinct().Count() });
                }
            }
        }
        return result;
    }
}


