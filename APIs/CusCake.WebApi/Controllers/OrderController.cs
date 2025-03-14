using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.OrderModels;
using CusCake.Domain.Constants;
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
        return Ok(ResponseModel<object, object>.Success(await _orderService.GetOrderByIdAsync(id)));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OrderUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _orderService.UpdateAsync(id, model)));
    }

}