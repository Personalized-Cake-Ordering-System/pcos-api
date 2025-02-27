using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.BakeryModel;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class BakeryController(IBakeryService bakeryService) : BaseController
{
    private readonly IBakeryService _bakeryService = bakeryService;


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, Bakery>.Success(await _bakeryService.GetByIdAsync(id)));
    }


    [HttpGet("{id}/approve")]
    public async Task<IActionResult> ApproveBakery(Guid id, bool isApprove = true)
    {
        await _bakeryService.ApproveBakeryAsync(id, isApprove);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201 });
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> CreateAsync([FromBody] BakeryCreateModel model)
    {
        var bakery = await _bakeryService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = bakery });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        var result = await _bakeryService.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, ICollection<Bakery>>.Success(result.Item2, result.Item1));
    }

    [HttpPut("id")]
    [Authorize(Roles = RoleConstants.BAKERY + "," + RoleConstants.ADMIN)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] BakeryUpdateModel model)
    {
        return Ok(ResponseModel<object, Bakery>.Success(await _bakeryService.UpdateAsync(id, model)));
    }
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _bakeryService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}