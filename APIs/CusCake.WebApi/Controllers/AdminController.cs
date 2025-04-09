using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using CusCake.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CusCake.WebApi.Controllers;

public class AdminController(IAdminService adminService, INotificationService notificationService) : BaseController
{
    public readonly IAdminService _adminService = adminService;
    public readonly INotificationService _notificationService = notificationService;

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
}