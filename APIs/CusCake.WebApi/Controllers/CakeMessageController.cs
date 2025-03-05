using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/cake_messages")]
public class CakeMessageController(ICakeMessageService cakeMessageService) : ControllerBase
{
    private readonly ICakeMessageService _cakeMessageService = cakeMessageService;



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakeMessageService.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] List<CakeMessageCreateModel> models)
    {
        var cake = await _cakeMessageService.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = cake });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10)
    {

        var result = await _cakeMessageService.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] CakeMessageUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _cakeMessageService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _cakeMessageService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}