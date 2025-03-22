using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CartModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class CartController(ICartService cartService) : BaseController
{
    private readonly ICartService _cartService = cartService;


    [HttpGet]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _cartService.GetCartAsync();
        return Ok(ResponseModel<object, object>.Success(result));
    }
    // [HttpGet("all")]
    // [Authorize(Roles = RoleConstants.CUSTOMER)]
    // public async Task<IActionResult> GetAllAsync()
    // {
    //     var result = await _cartService.GetAllCartAsync();
    //     return Ok(ResponseModel<object, object>.Success(result));
    // }

    [HttpPut]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpsertAsync(CartActionModel cart)
    {
        return Ok(ResponseModel<object, object>.Success(await _cartService.UpsertAsync(cart)));
    }

    // [HttpPost]
    // [Authorize(Roles = RoleConstants.CUSTOMER)]
    // public async Task<IActionResult> InsertAsync(CartEntity cart)
    // {
    //     await _cartService.InsertAsync(cart);
    //     return Ok(ResponseModel<object, object>.Success(cart));
    // }

    [HttpDelete]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> DeleteAsync()
    {
        await _cartService.DeleteAsync();
        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}