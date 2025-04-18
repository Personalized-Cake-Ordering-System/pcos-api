using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.ReportModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class ReportController(IReportService reportService) : BaseController
{

    private readonly IReportService _reportService = reportService;


    [HttpGet("{id}/approve")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> ApproveBakery(Guid id, bool isApprove = true)
    {
        await _reportService.ApproveAsync(id, isApprove);
        return StatusCode(200, new ResponseModel<object, object> { StatusCode = 200 });
    }

    /// <summary>
    /// Tạo report cho order thì orderId - Tạo report cho bakery thì để orderId là null 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ReportCreateModel model)
    {
        var report = await _reportService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = report });
    }

    /// <summary>
    /// Example to filter multiple status: PENDING.ACCEPTED.REJECTED
    /// </summary>
    [HttpGet]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllAsync(
        string? status,
        int pageIndex = 0,
        int pageSize = 10
    )
    {
        List<string> statusList = string.IsNullOrEmpty(status)
                          ? []
                          : [.. status.Split(".")];
        Expression<Func<Report, bool>> filter = x =>
            (string.IsNullOrEmpty(status) || statusList.Count == 0 || statusList.Contains(x.Status!));

        var result = await _reportService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, ICollection<Report>>.Success(result.Item2, result.Item1));
    }


    [HttpGet("{id}")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetByIdAsync(Guid id
    )
    {
        var result = await _reportService.GetByIdAsync(id);
        return Ok(ResponseModel<object, Report>.Success(result));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReportUpdateModel model)
    {
        return Ok(ResponseModel<object, Report>.Success(await _reportService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _reportService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }

}