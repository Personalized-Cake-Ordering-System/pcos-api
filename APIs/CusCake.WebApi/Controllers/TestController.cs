using CusCake.Application.Services;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CusCake.WebApi.Controllers;

public class TestController(INotificationService notificationService, IOrderService orderService) : BaseController
{
    private readonly IOrderService _orderService = orderService;
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet("test")]
    public async Task<IActionResult> GetAllAsync()
    {
        var userId = "cd8c2dae-76c3-485b-a674-dcd7e57a1b64";
        var order = await _orderService.GetOrderByIdAsync(Guid.Parse("038564a7-4880-4c80-bff0-c482e30e2dc7"));
        var json = JsonConvert.SerializeObject(order);
        await _notificationService.SendNotificationAsync(Guid.Parse(userId), json, NotificationType.NEW_ORDER);
        return Ok();
    }


}