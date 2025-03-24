using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;


public class CustomerController(
    ICustomerService customerService,
    INotificationService notificationService,
    IOrderService orderService,
    IVoucherService voucherService
) : BaseController
{
    private readonly INotificationService _notificationService = notificationService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IOrderService _orderService = orderService;
    private readonly IVoucherService _voucherService = voucherService;

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, Customer>.Success(await _customerService.GetByIdAsync(id)));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateModel model)
    {
        await _customerService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201 });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        var result = await _customerService.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, ICollection<Customer>>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CustomerUpdateModel model)
    {
        return Ok(ResponseModel<object, Customer>.Success(await _customerService.UpdateAsync(id, model)));
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _customerService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }

    /// <summary>
    /// Example to filter multiple type: NEW_ORDER.PROCESSING_ORDER.SHIPPING_ORDER
    /// </summary>
    [HttpGet("{id}/notifications")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> GetNotificationsAsync(
       Guid id,
       string? type,
       int pageIndex = 0,
       int pageSize = 10)
    {
        List<string> typeList = string.IsNullOrEmpty(type)
                 ? []
                 : [.. type.Split(".")];
        Expression<Func<Notification, bool>> filter = x =>
           (x.CustomerId == id) &&
           (string.IsNullOrEmpty(type) || (typeList.Count == 0 || typeList.Contains(x.Type!)));
        var result = await _notificationService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Notification>>.Success(result.Item2, result.Item1));
    }

    /// <summary>
    /// Example to filter multiple status: PENDING.COMPLETED.SHIPPING
    /// </summary>
    [HttpGet("{id}/orders")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> GetOrdersAsync(
       Guid id,
       string? status,
       int pageIndex = 0,
       int pageSize = 10)
    {
        List<string> statusList = string.IsNullOrEmpty(status)
                   ? []
                   : [.. status.Split(".")];

        Expression<Func<Order, bool>> filter = x =>
           (x.CustomerId == id) &&
            (string.IsNullOrEmpty(status) || (statusList.Count == 0 || statusList.Contains(x.OrderStatus!)));
        var result = await _orderService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<Order>>.Success(result.Item2, result.Item1));
    }

    [HttpGet("{id}/vouchers")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.CUSTOMER)]
    public async Task<IActionResult> GetCustomerVouchersAsync(
       Guid id,
       bool? isApplied,
       int pageIndex = 0,
       int pageSize = 10)
    {
        Expression<Func<CustomerVoucher, bool>> filter = x =>
            (x.CustomerId == id) &&
            (isApplied != null || x.IsApplied == isApplied);
        var result = await _voucherService.GetCustomerVouchersAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<CustomerVoucher>>.Success(result.Item2, result.Item1));
    }

}

