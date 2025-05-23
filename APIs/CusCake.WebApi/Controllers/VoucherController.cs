using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.VoucherModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class VoucherController(IVoucherService voucherService) : BaseController
{
    private readonly IVoucherService _voucherService = voucherService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _voucherService.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> CreateAsync([FromBody] VoucherCreateModel models)
    {
        var voucher = await _voucherService.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = voucher });
    }

    /// <summary>
    /// Get all vouchers with optional filtering
    /// </summary>
    /// <param name="bakeryId">Optional bakery ID to filter vouchers</param>
    /// <param name="pageIndex">Page index for pagination (default: 0)</param>
    /// <param name="pageSize">Page size for pagination (default: 10)</param>
    /// <param name="type">Filter by voucher type (GLOBAL, PRIVATE, or null for all)</param>
    /// <returns>List of vouchers with pagination data</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] Guid? bakeryId,
        int pageIndex = 0,
        int pageSize = 10,
        [FromQuery] string? type = null)
    {
        // Normalize type to uppercase for consistent comparison
        type = type?.ToUpper();

        // Build the filter expression
        Expression<Func<Voucher, bool>> filter = x =>
            x.VoucherType != VoucherTypeConstants.SYSTEM &&
            (bakeryId == null || x.BakeryId == bakeryId) &&
            (string.IsNullOrEmpty(type) || ((type != VoucherTypeConstants.GLOBAL) && (type != VoucherTypeConstants.PRIVATE)) || x.VoucherType == type);

        // Call service with filter and ordering
        var result = await _voucherService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] VoucherUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _voucherService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _voucherService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }


    [HttpPost("{id}/assigns")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> AssignVoucherToCustomer(Guid id, AssignVoucherModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _voucherService.AssignVoucherToCustomer(id, model)));

    }


    /// <summary>
    /// Api này để bakery xem voucher đã được tặng cho customer
    /// </summary>
    [HttpGet("{id}/assigns")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> GetCustomerVouchersAsync(
        Guid id,
        bool? isApplied,
        int pageIndex = 0,
        int pageSize = 10)
    {
        Expression<Func<CustomerVoucher, bool>> filter = x =>
            (x.VoucherId == id) &&
            (isApplied != null || x.IsApplied == isApplied);
        var result = await _voucherService.GetCustomerVouchersAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, List<CustomerVoucher>>.Success(result.Item2, result.Item1));
    }

}