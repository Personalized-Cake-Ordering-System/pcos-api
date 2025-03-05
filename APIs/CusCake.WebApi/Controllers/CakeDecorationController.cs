using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeDecorationModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/cake_decorations")]
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
        return StatusCode(201, new ResponseModel<object, List<CakeDecoration>> { StatusCode = 201, Payload = decorations });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10)
    {

        var result = await _service.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] CakeDecorationUpdateModel model)
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