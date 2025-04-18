using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class OrderController(IOrderService service) : BaseController
{
    private readonly IOrderService _orderService = service;

    [HttpPost]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> CreateAsync([FromBody] OrderCreateModel model)
    {
        var order = await _orderService.CreateAsync(model);
        return Ok(ResponseModel<object, object>.Success(order));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _orderService.GetOrderDetailAsync(id)));
    }

    /// <summary>
    /// Update order when Status is PENDING or CONFIRMED - Can not CANCEL after PAID
    /// </summary>
    [HttpPut("{id}/save")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _orderService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}/cancel")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.CUSTOMER)]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        await _orderService.CancelAsync(id);
        return NoContent();

    }

    /// <summary>
    /// Api này này dùng để chuyển state
    /// Move to next state
    /// </summary>
    [HttpPut("{id}/move-to-next")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.CUSTOMER)]
    public async Task<IActionResult> MoveToNextAsync(Guid id, [FromForm] List<IFormFile>? files = null)
    {
        var result = await _orderService.MoveToNextAsync<Order>(id, files);
        return Ok(ResponseModel<object, object>.Success(result!));
    }
}