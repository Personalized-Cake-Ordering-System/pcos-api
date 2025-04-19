using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.ReviewModels;
using CusCake.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;


    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.CUSTOMER)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReviewUpdateModel model)
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
    public async Task<IActionResult> CreateAsync([FromBody] ReviewCreateModel model)
    {
        return Ok(ResponseModel<object, object>.Success(await _reviewService.CreateAsync(model)));
    }
}