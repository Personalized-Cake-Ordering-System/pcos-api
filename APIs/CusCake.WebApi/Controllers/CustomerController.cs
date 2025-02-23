using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Entities;
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
        var customer = await _customerService.GetByIdAsync(id);
        return Ok(ResponseModel<object, CustomerViewModel>.Success(customer));
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
        var customers = await _customerService.GetAllAsync(pageIndex, pageSize);
        var metaData = new { TotalItemsCount = customers.TotalItemsCount };
        return Ok(ResponseModel<object, ICollection<Customer>>.Success(customers!.Items!, metaData));
    }
}
