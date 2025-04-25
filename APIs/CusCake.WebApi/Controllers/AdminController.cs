using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AdminReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using CusCake.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CusCake.WebApi.Controllers;

public class AdminController(
    IAdminService adminService,
    INotificationService notificationService,
    IAdminReportService adminReportService
    ) : BaseController
{
    public readonly IAdminService _adminService = adminService;
    public readonly INotificationService _notificationService = notificationService;

    public readonly IAdminReportService _adminReportService = adminReportService;
    // [HttpPost]
    // public async Task<IActionResult> CreateAsync([FromBody] AdminCreateModel model)
    // {
    //     await _adminService.CreateAsync(model);
    //     return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201 });
    // }

    /// <summary>
    /// Just use to view list admins - not use in FE
    /// </summary>

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _adminService.GetAllAsync());
    }

    /// <summary>
    /// Example to filter multiple type: NEW_ORDER.PROCESSING_ORDER.SHIPPING_ORDER
    /// </summary>
    [HttpGet("{id}/notifications")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetNotificationAsync(
    Guid id,
    string? type,
    int pageIndex = 0,
    int pageSize = 10)
    {
        List<string> typeList = string.IsNullOrEmpty(type)
                  ? []
                  : [.. type.Split(".")];
        Expression<Func<Notification, bool>> filter = x =>
           (x.AdminId == id) &&
           (string.IsNullOrEmpty(type) || typeList.Count == 0 || typeList.Contains(x.Type!));

        var result = await _notificationService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Notification>>.Success(result.Item2, result.Item1));
    }

    [HttpGet("sales-overview")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetSalesOverviewAsync()
    {
        // var orderByList = SortingHelper.ParseSortingParameters("total_revenue:desc", EntitySortingMappings.AvailableCakeMappings);

        var result = await _adminReportService.GetAdminOverviewModel();
        return Ok(ResponseModel<object, AdminOverviewModel>.Success(result));
    }

    [HttpGet("top-bakery-sales")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetTopBakeryMetricsAsync(
        int pageIndex = 0,
        int pageSize = 10)
    {
        var orderByList = SortingHelper.ParseSortingParameters("total_revenue:desc", EntitySortingMappings.BakeryMetricMappings);
        var result = await _adminReportService.GetTopBakeryMetrics(pageIndex, pageSize, orderByList);
        return Ok(ResponseModel<object, List<BakeryMetric>>.Success(result.Item2, result.Item1));
    }


    /// <summary>
    /// api/admins/chart?type=REVENUE&dateFrom=2024-05-25&dateTo=2025-04-28
    /// api/admins/chart?type=CUSTOMERS&dateFrom=2024-01-25&dateTo=2025-04-28
    /// api/admins/chart?type=BAKERIES&dateFrom=2025-04-25&dateTo=2025-04-28
    /// Thử để thấy cách truyền tham số dateFrom, dateTo
    /// </summary>
    /// <param name="type">REVENUE, CUSTOMERS, BAKERIES`</param>
    /// <param name="dateFrom">2025-04-25</param>
    /// <param name="dateTo">2025-04-28</param>
    /// <returns></returns>
    [HttpGet("chart")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAppReportAsync(string type,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var result = await _adminReportService.GetAdminChartAsync(type, dateFrom, dateTo);
        return Ok(ResponseModel<object, List<object>>.Success(result));
    }
}