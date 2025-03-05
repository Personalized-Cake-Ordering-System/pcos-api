using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakePartModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/cake_parts")]
public class CakePartController(ICakePartService cakePartService) : ControllerBase
{
    private readonly ICakePartService _cakePartService = cakePartService;



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakePartService.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] List<CakePartCreateModel> models)
    {
        var cakes = await _cakePartService.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, List<CakePart>> { StatusCode = 201, Payload = cakes });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10)
    {

        var result = await _cakePartService.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] CakePartUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakePartService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _cakePartService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}