using CusCake.Application.Extensions;
using CusCake.Application.ViewModels.AdminReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IAdminDashboardService
{
    Task<AdminOverviewModel> GetAdminOverviewAsync();
    // Task<List<object>> GetAdminSalesOverviewAsync(string type, int year);
    // Task<object> GetAdminProductPerformanceAsync();
    // Task<object> GetAdminCategoryDistributionAsync();
}

public class AdminDashboardService(IUnitOfWork unitOfWork) : IAdminDashboardService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private static readonly DateTime CurrentMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    private static readonly DateTime LastMonth = CurrentMonth.AddMonths(-1);

    public async Task<AdminOverviewModel> GetAdminOverviewAsync()
    {
        var orders = await _unitOfWork.OrderRepository.WhereAsync(
           x => x.OrderStatus == OrderStatusConstants.COMPLETED
        );

        var bakeries = await _unitOfWork.BakeryRepository.GetAllAsync();
        var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
        var reports = await _unitOfWork.ReportRepository.GetAllAsync();

        return new AdminOverviewModel
        {
            TotalRevenue = orders.Sum(x => x.AppCommissionFee),
            TotalBakeries = bakeries.Count,
            TotalReports = reports.Count,
            TotalCustomer = customers.Count
        };
    }
}