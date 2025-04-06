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
        var userId = "11f56ffc-6e29-4528-8e05-dadbc618dd5a";
        var order = await _orderService.GetOrderByIdAsync(Guid.Parse("038564a7-4880-4c80-bff0-c482e30e2dc7"));
        var json = JsonConvert.SerializeObject(order);
        await _notificationService.SendNotificationAsync(Guid.Parse(userId), json, NotificationType.PAYMENT_SUCCESS);
        return Ok();
    }


}