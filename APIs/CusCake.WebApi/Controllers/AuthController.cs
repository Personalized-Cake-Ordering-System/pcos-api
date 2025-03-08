using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.AuthModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    public readonly IAuthService _authService = authService;


    [HttpPost]
    public async Task<IActionResult> CustomerLogin([FromBody] AuthRequestModel model)
    {
        var result = await _authService.SignIn(model);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPost("revoke")]
    public IActionResult Revoke([FromBody] RevokeModel model)
    {
        var result = _authService.Revoke(model);
        return Ok(new ResponseModel<object, object> { StatusCode = 200, MetaData = result });
    }

}