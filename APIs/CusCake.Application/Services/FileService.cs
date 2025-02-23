using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Domain.Entities;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.Services;

public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile fileUpload, string folder);

    Task<bool> RemoveFileAsync(Guid fileId, string folder);

    Task<Storage> GetFileAsync(Guid fileId, string folder);

}


public class FileService : IFileService
{
    private readonly AppSettings _appSettings;
    private readonly IUnitOfWork _unitOfWork;

    public FileService(AppSettings appSettings, IUnitOfWork unitOfWork)
    {
        _appSettings = appSettings;
        _unitOfWork = unitOfWork;
    }
    public async Task<Storage> GetFileAsync(Guid fileId, string folder)
    {
        var file = await _unitOfWork.StorageRepository.GetByIdAsync(fileId) ?? throw new BadRequestException("File not found!");
        return file;
    }

    public Task<bool> RemoveFileAsync(Guid fileId, string folder)
    {
        throw new Exception();
    }

    public async Task<Guid> UploadFileAsync(IFormFile fileUpload, string folder)
    {
        if (fileUpload.Length > 0)
        {
            var fs = fileUpload.OpenReadStream();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey: _appSettings.FirebaseSettings.ApiKeY));

            var a = await auth.SignInWithEmailAndPasswordAsync(email: _appSettings.FirebaseSettings.AuthEmail, password: _appSettings.FirebaseSettings.AuthPassword);

            var extension = Path.GetExtension(fileUpload.FileName);

            var newFileName = $"{Guid.NewGuid()}{extension}";

            var cancellation = new FirebaseStorage(
                _appSettings.FirebaseSettings.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("assets/" + folder)
                .Child(newFileName)
                .PutAsync(fs, CancellationToken.None);

            try
            {
                var url = await cancellation;
                var result = await _unitOfWork.StorageRepository.AddAsync(new Storage { FileName = newFileName, FileUrl = url });
                await _unitOfWork.SaveChangesAsync();
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        else
        {
            throw new BadRequestException("File is not existed!");
        }
    }
}
