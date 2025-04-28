using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;
[ApiController]
[Route("api/bakeries")]
public class BakeryDashboardController(IBakeryDashboardService service) : ControllerBase
{

    private readonly IBakeryDashboardService _reportService = service;

    [HttpGet("{id}/overview")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetOverviewAsync(Guid id, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryOverviewAsync(id, dateFrom, dateTo)));
    }


    /// <summary>
    /// Gồm 3 type: REVENUE, ORDERS, CUSTOMERS
    /// </summary>
    [HttpGet("{id}/sales_overview")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetSalesOverviewAsync(Guid id, [FromQuery] string type, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakerySalesOverviewAsync(id, type, dateFrom, dateTo)));
    }

    /// <summary>
    /// Top 10 bánh bán chạy nhất - Không có returns 
    /// </summary>

    [HttpGet("{id}/products_performance")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetProductPerformanceAsync(Guid id, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryProductPerformanceAsync(id, dateFrom, dateTo)));
    }

    [HttpGet("{id}/category_distribution")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetCategoryDistributionAsync(Guid id, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryCategoryDistributionAsync(id, dateFrom, dateTo)));
    }
}

