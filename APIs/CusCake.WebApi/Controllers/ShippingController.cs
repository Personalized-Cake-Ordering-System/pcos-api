using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class ShippingController(IOrderService orderService) : BaseController
{
    public readonly IOrderService _orderService = orderService;


    [HttpGet]
    public async Task<IActionResult> CustomerLogin(string bakeryLat, string bakeryLng, string orderLat, string orderLng)
    {
        var result = await _orderService.CalculateShippingFee(bakeryLat, bakeryLng, orderLat, orderLng);
        return Ok(ResponseModel<object, object>.Success(result));
    }


}