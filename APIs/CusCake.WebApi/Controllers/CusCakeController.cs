using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Application.ViewModels.CustomCakeModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/custom_cakes")]
public class CusCakeController(ICustomCakeService customCakeService) : ControllerBase
{
    private readonly ICustomCakeService _customCakeService = customCakeService;


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] CustomCakeCreateModel model)
    {
        var cake = await _customCakeService.CreateAsync(model);
        return StatusCode(200, new ResponseModel<object, CustomCake> { StatusCode = 200, Payload = cake });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
         [FromQuery] Guid? bakeryId,
         [FromBody] Guid? customerId,
        int pageIndex = 0,
        int pageSize = 10)
    {

        Expression<Func<CustomCake, bool>> filter = x =>
                    (bakeryId == null || x.BakeryId == bakeryId) &&
                    (customerId == null || x.CustomerId == customerId);
        var result = await _customCakeService.GetAllAsync(pageIndex, pageSize, filter);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    // [HttpPut("{id}")]
    // [Authorize(Roles = RoleConstants.BAKERY)]
    // public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CakePartUpdateModel model)
    // {
    //     return Ok(ResponseModel<object, object>.Success(await _cakePartService.UpdateAsync(id, model)));
    // }

    // [HttpDelete("{id}")]
    // [Authorize(Roles = RoleConstants.BAKERY)]
    // public async Task<IActionResult> DeleteAsync(Guid id)
    // {
    //     await _cakePartService.DeleteAsync(id);

    //     return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    // }
}