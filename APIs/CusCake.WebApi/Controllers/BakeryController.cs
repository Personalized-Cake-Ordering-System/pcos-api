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
[ApiController]
[Route("api/bakeries")]
public class BakeryController(
    IBakeryService bakeryService,
    INotificationService notificationService,
    IOrderService orderService,
    IAvailableCakeService availableCakeService,
    ICustomCakeService customCakeService,
    IReportService reportService
) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IBakeryService _bakeryService = bakeryService;
    private readonly IAvailableCakeService _availableCakeService = availableCakeService;
    private readonly ICustomCakeService _customCakeService = customCakeService;
    private readonly IReportService _reportService = reportService;
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


    /// <summary>
    /// Action are BAN or UN_BAN
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action">BAN or UN_BAN</param>
    [HttpGet("{id}/ban_action")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> BanBakery(Guid id, string action)
    {
        await _bakeryService.BanedBakeryAsync(id, action);
        return StatusCode(200, new ResponseModel<object, object> { StatusCode = 200 });
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] BakeryCreateModel model)
    {
        var bakery = await _bakeryService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = bakery });
    }

    /// <summary>
    /// Example to filter multiple status: PENDING.CONFIRMED.REJECT
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        string? status,
        int pageIndex = 0,
        int pageSize = 10,
        [FromQuery] string? bakeryName = null
    )
    {
        List<string> statusList = string.IsNullOrEmpty(status)
                          ? []
                          : [.. status.Split(".")];
        Expression<Func<Bakery, bool>> filter = x =>
            (string.IsNullOrEmpty(bakeryName) || x.BakeryName.Contains(bakeryName, StringComparison.CurrentCultureIgnoreCase)) &&
            (string.IsNullOrEmpty(status) || statusList.Count == 0 || statusList.Contains(x.Status!));

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

    /// <summary>
    /// Example to filter multiple type: NEW_ORDER.PROCESSING_ORDER.SHIPPING_ORDER
    /// </summary>
    [HttpGet("{id}/notifications")]
    [Authorize(Roles = RoleConstants.BAKERY)]
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
           (x.BakeryId == id) &&
           (string.IsNullOrEmpty(type) || typeList.Count == 0 || typeList.Contains(x.Type!));

        var result = await _notificationService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Notification>>.Success(result.Item2, result.Item1));
    }

    /// <summary>
    /// Example to filter multiple status: PENDING.COMPLETED.SHIPPING
    /// </summary>
    [HttpGet("{id}/orders")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> GetOrdersAsync(
     Guid id,
     string? status,
     int pageIndex = 0,
     int pageSize = 10
     )
    {
        List<string> statusList = string.IsNullOrEmpty(status)
                    ? []
                    : [.. status.Split(".")];
        Expression<Func<Order, bool>> filter = x =>
            x.BakeryId == id &&
            (string.IsNullOrEmpty(status) || (statusList.Count == 0 || statusList.Contains(x.OrderStatus!)));

        var result = await _orderService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Order>>.Success(result.Item2, result.Item1));
    }

    /// <summary>
    /// Example to filter price : 0-100000
    /// Sort format: fieldName:asc/desc,anotherField:asc/desc
    /// Example sort: price:desc,name:asc
    /// </summary>
    [HttpGet("{id}/available_cakes")]
    public async Task<IActionResult> GetAvailableCakesAsync(
        Guid id,
        int pageIndex = 0,
        int pageSize = 10,
        string? type = null,
        string? price = null,
        string? name = null,
        string? sort = null)
    {
        List<double> prices = string.IsNullOrEmpty(price)
                           ? []
                           : [.. price.Split("-").Select(p => double.TryParse(p, out double result) ? result : (double?)null).Where(p => p.HasValue).Cast<double>()];

        List<string> typeList = string.IsNullOrEmpty(type)
                                  ? []
                                  : [.. type.Split(".")];

        Expression<Func<AvailableCake, bool>> filter = x =>
            (x.BakeryId == id) &&
            (string.IsNullOrEmpty(name) || x.AvailableCakeName.Contains(name, StringComparison.CurrentCultureIgnoreCase)) &&
            (string.IsNullOrEmpty(type) || typeList.Count == 0 || typeList.Contains(x.AvailableCakeType!)) &&
            (string.IsNullOrEmpty(price) || (x.AvailableCakePrice >= prices.First() && x.AvailableCakePrice <= prices.Last()))
            && x.Bakery.IsDeleted == false && x.Bakery.Status == BakeryStatusConstants.CONFIRMED;

        var orderByList = SortingHelper.ParseSortingParameters(sort, EntitySortingMappings.AvailableCakeMappings);

        var result = await _availableCakeService.GetAllAsync(pageIndex, pageSize, filter: filter, orderByList: orderByList);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpGet("{id}/custom_cakes")]
    [Authorize]
    public async Task<IActionResult> GetCustomCakesAsync(
        Guid id,
        [FromQuery] Guid? customerId,
        int pageIndex = 0,
        int pageSize = 10)
    {

        Expression<Func<CustomCake, bool>> filter = x =>
                    (x.BakeryId == id) &&
                    (customerId == null || x.CustomerId == customerId);
        var result = await _customCakeService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    /// <summary>
    /// Example to filter multiple status: PENDING.ACCEPTED.REJECTED
    /// </summary>
    [HttpGet("{id}/reports")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllAsync(
        Guid id,
        string? status,
        int pageIndex = 0,
        int pageSize = 10
    )
    {
        List<string> statusList = string.IsNullOrEmpty(status)
                          ? []
                          : [.. status.Split(".")];
        Expression<Func<Report, bool>> filter = x =>
            (x.BakeryId == id) &&
            (string.IsNullOrEmpty(status) || statusList.Count == 0 || statusList.Contains(x.Status!));

        var result = await _reportService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, ICollection<Report>>.Success(result.Item2, result.Item1));
    }

}