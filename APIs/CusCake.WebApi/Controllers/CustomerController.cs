using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;


public class CustomerController(ICustomerService customerService) : BaseController
{
    private readonly ICustomerService _customerService = customerService;

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, Customer>.Success(await _customerService.GetByIdAsync(id)));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateModel model)
    {
        await _customerService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201 });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        var result = await _customerService.GetAllAsync(pageIndex, pageSize);
        return Ok(ResponseModel<object, ICollection<Customer>>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CustomerUpdateModel model)
    {
        return Ok(ResponseModel<object, Customer>.Success(await _customerService.UpdateAsync(id, model)));
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _customerService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}

