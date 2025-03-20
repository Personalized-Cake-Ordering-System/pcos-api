using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.AvailableCakeModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/available_cakes")]
public class AvailableCakeController(IAvailableCakeService availableCakeService) : BaseController
{
    private readonly IAvailableCakeService _availableCakeService = availableCakeService;



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _availableCakeService.GetByIdAsync(id)));
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> CreateAsync([FromBody] AvailableCakeCreateModel model)
    {
        var cake = await _availableCakeService.CreateAsync(model);
        return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201, Payload = cake });
    }

    /// <summary>
    /// Example to filter price : 0-100000
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        string? type,
        int pageIndex = 0,
        int pageSize = 10,
        string? price = null,
        string? name = null,
        [FromQuery] Guid? bakeryId = null)
    {
        List<double> prices = string.IsNullOrEmpty(price)
                           ? []
                           : [.. price.Split("-").Select(p => double.TryParse(p, out double result) ? result : (double?)null).Where(p => p.HasValue).Cast<double>()];


        List<string> typeList = string.IsNullOrEmpty(type)
                                  ? []
                                  : [.. type.Split(".")];

        Expression<Func<AvailableCake, bool>> filter = x =>
            ((bakeryId == null || bakeryId == Guid.Empty) || x.BakeryId == bakeryId) &&
            (string.IsNullOrEmpty(name) || x.AvailableCakeName.Contains(name, StringComparison.CurrentCultureIgnoreCase)) &&
            (string.IsNullOrEmpty(type) || (typeList.Count == 0 || typeList.Contains(x.AvailableCakeType!))) &&
            (string.IsNullOrEmpty(price) || (x.AvailableCakePrice >= prices.First() && x.AvailableCakePrice <= prices.Last()));


        var result = await _availableCakeService.GetAllAsync(pageIndex, pageSize, filter: filter);
        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AvailableCakeUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _availableCakeService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.BAKERY)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _availableCakeService.DeleteAsync(id);

        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }
}