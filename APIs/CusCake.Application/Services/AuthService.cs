using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;


public interface IAuthService
{
    Task<(AuthResponseModel, Customer)> CustomerSignIn(AuthRequestModel model);
    Task<(AuthResponseModel, Admin)> AdminSignIn(AuthRequestModel model);
    Task<(AuthResponseModel, Bakery)> BakerySignIn(AuthRequestModel model);
    string Revoke(RevokeModel revoke);
}

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private IJWTService _jWTService;

    public AuthService(IUnitOfWork unitOfWork, IJWTService jWTService)
    {
        _unitOfWork = unitOfWork;
        _jWTService = jWTService;
    }
    public async Task<(AuthResponseModel, Admin)> AdminSignIn(AuthRequestModel model)
    {
        var admin = await _unitOfWork.AdminRepository.FirstOrDefaultAsync(x => x.Email == model.Email & x.Password == model.Password) ?? throw new BadRequestException("Incorrect email or password!");
        var authResponse = new AuthResponseModel
        {
            AccessToken = _jWTService.GenerateAccessToken(admin.Id, RoleConstants.ADMIN),
            RefreshToken = _jWTService.GenerateRefreshToken(admin.Id, RoleConstants.ADMIN)
        };
        return (authResponse, admin);
    }

    public async Task<(AuthResponseModel, Bakery)> BakerySignIn(AuthRequestModel model)
    {
        var bakery = await _unitOfWork.BakeryRepository.FirstOrDefaultAsync(x => x.Email == model.Email & x.Password == model.Password) ?? throw new BadRequestException("Incorrect email or password!");
        var authResponse = new AuthResponseModel
        {
            AccessToken = _jWTService.GenerateAccessToken(bakery.Id, RoleConstants.BAKERY),
            RefreshToken = _jWTService.GenerateRefreshToken(bakery.Id, RoleConstants.BAKERY)
        };
        return (authResponse, bakery);
    }

    public async Task<(AuthResponseModel, Customer)> CustomerSignIn(AuthRequestModel model)
    {
        var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Email == model.Email & x.Password == model.Password) ?? throw new BadRequestException("Incorrect email or password!");
        var authResponse = new AuthResponseModel
        {
            AccessToken = _jWTService.GenerateAccessToken(customer.Id, RoleConstants.CUSTOMER),
            RefreshToken = _jWTService.GenerateRefreshToken(customer.Id, RoleConstants.CUSTOMER)
        };
        return (authResponse, customer);
    }

    public string Revoke(RevokeModel revoke)
    {
        return _jWTService.RevokeToken(revoke);
    }
}
