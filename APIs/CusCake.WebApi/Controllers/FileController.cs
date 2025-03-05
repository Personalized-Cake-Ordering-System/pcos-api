using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class FileController(IFileService fileService) : BaseController
{
    private readonly IFileService _fileService = fileService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileById(Guid id)
    {
        return Ok(ResponseModel<object, object>.Success(await _fileService.GetFileAsync(id)));
    }

    [HttpGet()]
    public async Task<IActionResult> GetList(List<Guid> ids)
    {
        return Ok(ResponseModel<object, object>.Success(await _fileService.GetListAsync(ids)));
    }

    [HttpPost()]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        return Ok(ResponseModel<object, Storage>.Success(await _fileService.UploadFileAsync(formFile)));
    }
}