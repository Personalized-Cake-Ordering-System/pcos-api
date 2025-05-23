using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;
using Firebase.Auth;

namespace CusCake.Application.Services;


public interface IAuthService
{
    Task<(AuthResponseModel, object)> SignIn(AuthRequestModel model);
    string Revoke(RevokeModel revoke);
    Task<Auth> CreateAsync(AuthCreateModel model);
    Task<bool> UpdateAsync(AuthUpdateModel model);
    Task<bool> DeleteAsync(Guid entityId);
    Task<Auth> GetAuthByIdAsync(Guid entityId);
    Task<Auth> GetAdminAsync();
    Task<(AuthResponseModel, object)> SignWithGoogle(AuthGoogleRequestModel model);
}

public class AuthService(
    IUnitOfWork unitOfWork,
    IJWTService jWTService,
    IMapper mapper,
    AppSettings appSettings
) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJWTService _jWTService = jWTService;
    private readonly AppSettings _appSettings = appSettings;
    private readonly IMapper _mapper = mapper;

    public async Task<(AuthResponseModel, object)> SignIn(AuthRequestModel model)
    {

        // var includes = QueryHelper.Includes<Auth>(x => x.Customer!, x => x.Admin!, x => x.Bakery!);

        var auth = await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email &
                x.Password == model.Password, includes: x => x.Wallet
            ) ?? throw new BadRequestException("Incorrect email or password!");

        var authResponse = new AuthResponseModel
        {
            AccessToken = _jWTService.GenerateAccessToken(auth.EntityId, auth.Role),
            RefreshToken = _jWTService.GenerateRefreshToken(auth.EntityId, auth.Role)
        };

        var result = _mapper.Map<AuthViewModel>(auth);
        if (auth.Role == RoleConstants.ADMIN)
            result.Entity = await _unitOfWork.AdminRepository.GetByIdAsync(auth.EntityId) ?? throw new BadRequestException("Not found");
        else if (auth.Role == RoleConstants.CUSTOMER)
            result.Entity = await _unitOfWork.CustomerRepository.GetByIdAsync(auth.EntityId) ?? throw new BadRequestException("Not found");
        else
            result.Entity = await _unitOfWork.BakeryRepository.GetByIdAsync(auth.EntityId) ?? throw new BadRequestException("Not found");

        return (authResponse, result);
    }

    public string Revoke(RevokeModel revoke)
    {
        return _jWTService.RevokeToken(revoke);
    }

    public async Task<Auth> CreateAsync(AuthCreateModel model)
    {
        var auth = _mapper.Map<Auth>(model);
        var isExist = await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email &&
                x.Role == model.Role);
        if (isExist != null) throw new BadRequestException($"Email {model.Email} has already exist!");
        var wallet = await CreateWalletAsync();

        auth.WalletId = wallet.Id;

        await _unitOfWork.AuthRepository.AddAsync(auth);
        await _unitOfWork.SaveChangesAsync();

        return auth;
    }

    private async Task<Wallet> CreateWalletAsync()
    {
        var wallet = new Wallet();
        await _unitOfWork.WalletRepository.AddAsync(wallet);

        return wallet;
    }
    public async Task<bool> UpdateAsync(AuthUpdateModel model)
    {
        var auth = await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x => x.EntityId == model.EntityId) ?? throw new BadRequestException("Error at update auth!");

        _mapper.Map(model, auth);

        _unitOfWork.AuthRepository.Update(auth);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid entityId)
    {
        var auth = await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x => x.EntityId == entityId) ?? throw new BadRequestException("Error at update auth!");

        _unitOfWork.AuthRepository.SoftRemove(auth);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Auth> GetAuthByIdAsync(Guid entityId)
    {
        return await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x =>
                x.EntityId == entityId,
                includes: x => x.Wallet
            ) ?? throw new BadRequestException("Error at update auth!");

    }

    public async Task<Auth> GetAdminAsync()
    {
        return await _unitOfWork.AuthRepository.FirstOrDefaultAsync(x => x.Email.Equals("admin@gmail.com"), includes: x => x.Wallet)
                  ?? throw new NotFoundException("Admin not found!");
    }

    public async Task<(AuthResponseModel, object)> SignWithGoogle(AuthGoogleRequestModel model)
    {
        var authGoogle = new FirebaseAuthProvider(new FirebaseConfig(apiKey: _appSettings.FirebaseSettings.ApiKeY));
        var userGoogleProfile = await authGoogle.GetUserAsync(model.AccessToken)
            ?? throw new Exception($"Error at {nameof(AuthService)}: Firebase can not get this user");

        var auth = await _unitOfWork.AuthRepository
            .FirstOrDefaultAsync(x => x.Email == userGoogleProfile.Email) ?? await CreateCustomerAsync(userGoogleProfile);

        var customer = await _unitOfWork.CustomerRepository
            .FirstOrDefaultAsync(x => x.Id == auth.EntityId & x.AccountType == CustomerRegisterConstants.GOOGLE) ?? throw new BadRequestException("Account is not exist!");

        var authResponse = new AuthResponseModel
        {
            AccessToken = _jWTService.GenerateAccessToken(auth.EntityId, auth.Role),
            RefreshToken = _jWTService.GenerateRefreshToken(auth.EntityId, auth.Role)
        };

        var result = _mapper.Map<AuthViewModel>(auth);
        result.Entity = customer;

        return (authResponse, result);
    }

    private async Task<Auth> CreateCustomerAsync(User userGoogleProfile)
    {
        var wallet = await CreateWalletAsync();

        var result = await _unitOfWork.CustomerRepository.AddAsync(new Customer
        {
            Email = userGoogleProfile.Email,
            Name = userGoogleProfile.DisplayName,
            AccountType = CustomerRegisterConstants.GOOGLE
        });

        var auth = await _unitOfWork.AuthRepository.AddAsync(new Auth
        {
            Email = userGoogleProfile.Email,
            Role = RoleConstants.CUSTOMER,
            EntityId = result.Id,
            WalletId = wallet.Id,
        });

        await _unitOfWork.SaveChangesAsync();

        return auth;
    }

}
