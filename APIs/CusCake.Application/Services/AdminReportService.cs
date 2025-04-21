

using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AdminReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAdminReportService
{
    Task<AdminOverviewModel> GetAdminOverviewModel();
    Task<(Pagination, List<BakeryMetric>)> GetTopBakeryMetrics(
        int pageIndex = 0,
        int pageSize = 10,
        List<(Expression<Func<BakeryMetric, object>> OrderBy, bool IsDescending)>? orderByList = null
    );
    Task<List<object>> GetAdminChartAsync(string type, int year);
}

public class AdminReportService(IUnitOfWork unitOfWork, IMapper mapper) : IAdminReportService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<AdminOverviewModel> GetAdminOverviewModel()
    {
        var order_completed = await _unitOfWork.OrderRepository.WhereAsync(o => o.OrderStatus == OrderStatusConstants.COMPLETED);

        var bakeries = await _unitOfWork.BakeryRepository.WhereAsync(x => x.Status == BakeryStatusConstants.CONFIRMED);

        var available_cakes = await _unitOfWork.AvailableCakeRepository.GetAllAsync();

        var reports = await _unitOfWork.ReportRepository.GetAllAsync();
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
        List<(Expression<Func<BakeryMetric, object>> OrderBy, bool IsDescending)>? orderByList = null
    )
    {

        return await _unitOfWork.BakeryMetricRepository.ToPagination(pageIndex, pageSize, orderByList: orderByList, includes: x => x.Bakery);

    }


    public async Task<List<object>> GetAdminChartAsync(string type, int year)
    {

        var result = new List<object>();

        for (int month = 1; month <= 12; month++)
        {

            if (type == "REVENUE")
            {
                var orders = await _unitOfWork.OrderRepository.WhereAsync(
                    x => x.CreatedAt.Year == year
                    && x.OrderStatus == OrderStatusConstants.COMPLETED
                );

                var monthlyOrders = orders.Where(x => x.CreatedAt.Month == month);

                result.Add(monthlyOrders.Sum(x => x.AppCommissionFee));
            }
            else if (type == "BAKERIES")
            {
                var bakeries = await _unitOfWork.BakeryRepository.WhereAsync(x => x.Status != BakeryStatusConstants.BANNED);
                var monthlyBakeries = bakeries.Where(x => x.CreatedAt.Month == month && x.CreatedAt.Year == year);
                result.Add(monthlyBakeries.Count());
            }
            else if (type == "CUSTOMERS")
            {
                var orders = await _unitOfWork.OrderRepository.WhereAsync(
                    x => x.CreatedAt.Year == year
                    && x.OrderStatus == OrderStatusConstants.COMPLETED
                );
                var monthlyOrders = orders.Where(x => x.CreatedAt.Month == month);
                result.Add(monthlyOrders.Select(x => x.CustomerId).Distinct().Count());
            }
        }

        return result;
    }

}

