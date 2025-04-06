using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;
[ApiController]
[Route("api/bakeries")]
public class BakeryReportController(IBakeryReportService reportService) : ControllerBase
{
    private readonly IBakeryReportService _reportService = reportService;

    [HttpGet("{id}/overview")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetOverviewAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryOverviewAsync(id)));
    }


    /// <summary>
    /// Gồm 3 type: REVENUE, ORDERS, CUSTOMERS
    /// </summary>
    [HttpGet("{id}/sales_overview")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetSalesOverviewAsync(Guid id, [FromQuery] string type, [FromQuery] int year)
    {
        return Ok(ResponseModel<object, List<object>>.Success(await _reportService.GetBakerySalesOverviewAsync(id, type, year)));
    }

    /// <summary>
    /// Top 10 bánh bán chạy nhất - Không có returns 
    /// </summary>

    [HttpGet("{id}/products_performance")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetProductPerformanceAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryProductPerformanceAsync(id)));
    }

    [HttpGet("{id}/category_distribution")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> GetCategoryDistributionAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _reportService.GetBakeryCategoryDistributionAsync(id)));
    }
}

