using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AuthModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    public readonly IAuthService _authService = authService;


    [HttpPost("customer")]
    public async Task<IActionResult> CustomerLogin([FromBody] AuthRequestModel model)
    {
        var result = await _authService.CustomerSignIn(model);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }
    [HttpPost("admin")]
    public async Task<IActionResult> AdminLogin([FromBody] AuthRequestModel model)
    {
        var result = await _authService.AdminSignIn(model);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }
    [HttpPost("bakery")]
    public async Task<IActionResult> BakeryLogin([FromBody] AuthRequestModel model)
    {
        var result = await _authService.BakerySignIn(model);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }
    [HttpPost("revoke")]
    public IActionResult Revoke([FromBody] RevokeModel model)
    {
        var result = _authService.Revoke(model);
        return Ok(new ResponseModel<object, object> { StatusCode = 200, MetaData = result });
    }

}