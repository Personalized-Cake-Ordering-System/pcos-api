using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeExtraModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/extra_options")]
public class CakeExtraController(ICakeExtraService cakeExtraService) : ControllerBase
{
    private readonly ICakeExtraService _cakeExtraService = cakeExtraService;



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakeExtraService.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] List<CakeExtraCreateModel> models)
    {
        var cakes = await _cakeExtraService.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, List<CakeExtraOption>> { StatusCode = 201, Payload = cakes });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] Guid? bakeryId,
        int pageIndex = 0,
        int pageSize = 10)
    {
        Expression<Func<CakeExtraOption, bool>> filter = x =>
                  (bakeryId == null || x.BakeryId == bakeryId);
        var result = await _cakeExtraService.GetAllAsync(pageIndex, pageSize, filter);
        var groupedOptions = result.Item2
               .GroupBy(option => option.Type)
               .Select(group => new
               {
                   Type = group.Key,
                   Items = group.ToList()
               })
               .ToList();
        return Ok(ResponseModel<object, object>.Success(groupedOptions, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CakeExtraUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakeExtraService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _cakeExtraService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}