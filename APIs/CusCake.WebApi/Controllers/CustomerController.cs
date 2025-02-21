using CusCake.Application.Services;
using CusCake.Application.ViewModels.CustomerModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;


public class CustomerController : BaseController
{

    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _customerService.GetByIdAsync(id));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateModel model)
    {
        await _customerService.CreateAsync(model);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        await _customerService.DemoAsync();
        return Ok(await _customerService.GetAllAsync(pageIndex, pageSize));
    }
}
