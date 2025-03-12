using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/decoration_options")]
public class CakeDecorationController(ICakeDecorationService service) : ControllerBase
{
    private readonly ICakeDecorationService _service = service;



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _service.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] List<CakeDecorationCreateModel> models)
    {
        var decorations = await _service.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, List<CakeDecorationOption>> { StatusCode = 201, Payload = decorations });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] Guid? bakeryId,
        int pageIndex = 0,
        int pageSize = 10
    )
    {
        Expression<Func<CakeDecorationOption, bool>> filter = x =>
                  (bakeryId == null || x.BakeryId == bakeryId);

        var result = await _service.GetAllAsync(pageIndex, pageSize, filter);
        var groupedOptions = result.Item2
               .GroupBy(option => option.Type)
               .Select(group => new
               {
                   Name = group.Key,
                   Items = group.ToList()
               })
               .ToList();

        return Ok(ResponseModel<object, object>.Success(groupedOptions, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CakeDecorationUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _service.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}