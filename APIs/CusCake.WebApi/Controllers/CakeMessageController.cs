using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeMessageModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/message_options")]
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
    public async Task<IActionResult> CreateAsync([FromBody] List<CakeMessageOptionCreateModel> models)
    {
        var cakes = await _cakeMessageService.CreateAsync(models);
        return StatusCode(201, new ResponseModel<object, List<CakeMessageOption>> { StatusCode = 201, Payload = cakes });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
         [FromQuery] Guid? bakeryId,
        int pageIndex = 0,
        int pageSize = 10)
    {

        Expression<Func<CakeMessageOption, bool>> filter = x =>
                  (bakeryId == null || x.BakeryId == bakeryId);
        var result = await _cakeMessageService.GetAllAsync(pageIndex, pageSize, filter);
        var groupedOptions = result.Item2
                .GroupBy(option => option.Name)
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
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] CakeMessageOptionUpdateModel model)
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