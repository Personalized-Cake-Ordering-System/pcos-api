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
    IAdminReportService adminReportService,
    IVoucherService voucherService
    ) : BaseController
{
    public readonly IAdminService _adminService = adminService;
    public readonly INotificationService _notificationService = notificationService;
    public readonly IVoucherService _voucherService = voucherService;
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
    public async Task<IActionResult> GetSalesOverviewAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null
    )
    {
        var result = await _adminReportService.GetAdminOverviewModel(dateFrom, dateTo);
        return Ok(ResponseModel<object, AdminOverviewModel>.Success(result));
    }

    [HttpGet("top-bakery-sales")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetTopBakeryMetricsAsync(
        int pageIndex = 0,
        int pageSize = 10,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var orderByList = SortingHelper.ParseSortingParameters("total_revenue:desc", EntitySortingMappings.BakeryMetricMappings);
        var result = await _adminReportService.GetTopBakeryMetrics(pageIndex, pageSize, dateFrom, dateTo, orderByList);
        return Ok(ResponseModel<object, List<BakeryMetric>>.Success(result.Item2, result.Item1));
    }


    /// <summary>
    /// REVENUE, CUSTOMERS, BAKERIES
    /// </summary>
    [HttpGet("chart")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAppReportAsync(string type,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var result = await _adminReportService.GetAdminChartAsync(type, dateFrom, dateTo);
        return Ok(ResponseModel<object, List<object>>.Success(result));
    }

    /// <summary>
    /// Get all vouchers with optional filtering
    /// </summary>
    /// <param name="bakeryId">Optional bakery ID to filter vouchers</param>
    /// <param name="pageIndex">Page index for pagination (default: 0)</param>
    /// <param name="pageSize">Page size for pagination (default: 10)</param>
    /// <param name="type">Filter by voucher type (GLOBAL, PRIVATE,SYSTEM or null for all)</param>
    /// <returns>List of vouchers with pagination data</returns>
    [HttpGet("vouchers")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllVoucherAsync(
        [FromQuery] Guid? bakeryId,
        int pageIndex = 0,
        int pageSize = 10,
        [FromQuery] string? type = null)
    {
        // Normalize type to uppercase for consistent comparison
        type = type?.ToUpper();

        // Build the filter expression
        Expression<Func<Voucher, bool>> filter = x =>
            (bakeryId == null || x.BakeryId == bakeryId) &&
            (string.IsNullOrEmpty(type) || ((type != VoucherTypeConstants.GLOBAL) && (type != VoucherTypeConstants.PRIVATE) && (type != VoucherTypeConstants.SYSTEM)) || x.VoucherType == type);

        // Call service with filter and ordering
        var result = await _voucherService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }
}