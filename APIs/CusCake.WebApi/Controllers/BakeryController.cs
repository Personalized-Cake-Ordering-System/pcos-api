using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.BakeryModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;
[ApiController]
[Route("api/bakeries")]
public class BakeryController(
    IBakeryService bakeryService,
    INotificationService notificationService,
    IOrderService orderService
) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IBakeryService _bakeryService = bakeryService;


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, Bakery>.Success(await _bakeryService.GetByIdAsync(id)));
    }


    [HttpGet("{id}/approve")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> ApproveBakery(Guid id, bool isApprove = true)
    {
        await _bakeryService.ApproveBakeryAsync(id, isApprove);
        return StatusCode(200, new ResponseModel<object, object> { StatusCode = 200 });
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] BakeryCreateModel model)
    {
        var bakery = await _bakeryService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = bakery });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        [FromQuery] string? bakeryName = null
    )
    {

        Expression<Func<Bakery, bool>> filter = x =>
            (string.IsNullOrEmpty(bakeryName) || x.BakeryName.Contains(bakeryName, StringComparison.CurrentCultureIgnoreCase));

        var result = await _bakeryService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, ICollection<Bakery>>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] BakeryUpdateModel model)
    {
        return Ok(ResponseModel<object, Bakery>.Success(await _bakeryService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _bakeryService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }

    [HttpGet("{id}/notifications")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> GetNotificationAsync(
    Guid id,
    int pageIndex = 0,
    int pageSize = 10)
    {
        Expression<Func<Notification, bool>> filter = x =>
           (x.BakeryId == id);
        var result = await _notificationService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Notification>>.Success(result.Item2, result.Item1));
    }

    [HttpGet("{id}/orders")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> GetOrdersAsync(
     Guid id,
     int pageIndex = 0,
     int pageSize = 10)
    {
        Expression<Func<Order, bool>> filter = x =>
           (x.BakeryId == id);
        var result = await _orderService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Order>>.Success(result.Item2, result.Item1));
    }

}