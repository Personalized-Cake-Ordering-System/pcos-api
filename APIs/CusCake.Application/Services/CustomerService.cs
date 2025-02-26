using AutoMapper;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICustomerService
{
    Task<bool> CreateAsync(CustomerCreateModel model);
    Task<CustomerViewModel> GetByIdAsync(Guid id);

    Task<Pagination<Customer>> GetAllAsync(int pageIndex = 0, int pageSize = 10);

}

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(CustomerCreateModel model)
    {
        var customer = _mapper.Map<Customer>(model);
        await _unitOfWork.CustomerRepository.AddAsync(customer);
        return await _unitOfWork.SaveChangesAsync();
    }


    public async Task<Pagination<Customer>> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        var result = await _unitOfWork.CustomerRepository.ToPagination(pageIndex, pageSize);
        // throw new BadRequestException("Error");

        // var customers = await _unitOfWork.CustomerRepository.GetAllAsync(includes: QueryHelper.Includes<Customer>(x => x.CustomCakes!, x => x.Orders!));
        return result;
    }

    public async Task<CustomerViewModel> GetByIdAsync(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        return _mapper.Map<CustomerViewModel>(customer);
    }
}
