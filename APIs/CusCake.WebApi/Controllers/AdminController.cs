using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Application.ViewModels.AdminModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class AdminController(IAdminService adminService) : BaseController
{
    public readonly IAdminService _adminService = adminService;


    /// <summary>
    /// Just use to create admin - not use in FE
    /// </summary>
    // [HttpPost]
    // public async Task<IActionResult> CreateAsync([FromBody] AdminCreateModel model)
    // {
    //     await _adminService.CreateAsync(model);
    //     return StatusCode(201, new ResponseModel<object, object> { StatusCode = 201 });
    // }

    /// <summary>
    /// Just use to view list admins - not use in FE
    /// </summary>

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _adminService.GetAllAsync());
    }


}