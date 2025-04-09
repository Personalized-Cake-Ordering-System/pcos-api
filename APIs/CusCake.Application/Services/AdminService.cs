using AutoMapper;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;


public interface IAdminService
{
    Task<Admin> CreateAsync(AdminCreateModel model);

    Task<List<Admin>> GetAllAsync();
}


public class AdminService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : IAdminService
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;

    public async Task<Admin> CreateAsync(AdminCreateModel model)
    {
        var admin = _mapper.Map<Admin>(model);

        var result = await _unitOfWork.AdminRepository.AddAsync(admin);

        await _unitOfWork.SaveChangesAsync();

        await _authService.CreateAsync(new AuthCreateModel
        {
            Email = model.Email,
            Password = model.Password,
            EntityId = admin.Id,
            Role = RoleConstants.ADMIN,
            AdminId = admin.Id
        });
        return result;
    }

    public async Task<List<Admin>> GetAllAsync()
    {
        return await _unitOfWork.AdminRepository.GetAllAsync();
    }

}
