using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.CakeReviewModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/cake_reviews")]
public class CakeReviewController(ICakeReviewService reviewService) : ControllerBase
{
    private readonly ICakeReviewService _reviewService = reviewService;


    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CakeReviewUpdateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _reviewService.UpdateAsync(id, model)));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _reviewService.DeleteAsync(id);
        return StatusCode(204, new ResponseModel<object, object> { StatusCode = 204 });
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> CreateAsync([FromBody] CakeReviewCreateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _reviewService.CreateAsync(model)));
    }
}