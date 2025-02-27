using AutoMapper;
using CusCake.Application.ViewModels.AdminModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;


public interface IAdminService
{
    Task<Admin> CreateAsync(AdminCreateModel model);

    Task<List<Admin>> GetAllAsync();
}


public class AdminService : IAdminService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Admin> CreateAsync(AdminCreateModel model)
    {
        var admin = _mapper.Map<Admin>(model);
        var result = await _unitOfWork.AdminRepository.AddAsync(admin);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<List<Admin>> GetAllAsync()
    {
        return await _unitOfWork.AdminRepository.GetAllAsync();
    }
}
