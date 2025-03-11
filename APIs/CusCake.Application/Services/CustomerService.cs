using AutoMapper;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.AuthModels;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICustomerService
{
    Task<Customer> CreateAsync(CustomerCreateModel model);
    Task<Customer> GetByIdAsync(Guid id);
    Task<(Pagination, List<Customer>)> GetAllAsync(int pageIndex = 0, int pageSize = 10);
    Task<Customer> UpdateAsync(Guid id, CustomerUpdateModel model);
    Task DeleteAsync(Guid id);
}

public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthService _authService = authService;

    public async Task<Customer> CreateAsync(CustomerCreateModel model)
    {
        var existCustomer = await _unitOfWork.CustomerRepository.WhereAsync(x => x.Email == model.Email);

        if (existCustomer.Count != 0) throw new BadRequestException("Email '{model.Email}' already exists.");

        var customer = _mapper.Map<Customer>(model);

        customer.AccountType = CustomerRegisterConstants.NORMAL;

        var result = await _unitOfWork.CustomerRepository.AddAsync(customer);

        await _unitOfWork.SaveChangesAsync();

        await _authService.CreateAsync(new AuthCreateModel
        {
            Email = model.Email,
            Password = model.Password,
            EntityId = customer.Id,
            Role = RoleConstants.CUSTOMER,
            CustomerId = customer.Id
        });

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await GetByIdAsync(id);

        _unitOfWork.CustomerRepository.SoftRemove(customer);

        await _unitOfWork.SaveChangesAsync();
        await _authService.DeleteAsync(customer.Id);
    }

    public async Task<(Pagination, List<Customer>)> GetAllAsync(int pageIndex = 0, int pageSize = 10)
    {
        var result = await _unitOfWork.CustomerRepository.ToPagination(pageIndex, pageSize);
        // var customers = await _unitOfWork.CustomerRepository.GetAllAsync(includes: QueryHelper.Includes<Customer>(x => x.CustomCakes!, x => x.Orders!));
        return result;
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.CustomerRepository.GetByIdAsync(id) ?? throw new BadRequestException("Id is not exist!");
    }

    public async Task<Customer> UpdateAsync(Guid id, CustomerUpdateModel model)
    {

        var customer = await GetByIdAsync(id);

        _mapper.Map(model, customer);

        _unitOfWork.CustomerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync();

        await _authService.UpdateAsync(new AuthUpdateModel
        {
            EntityId = customer.Id,
            Password = customer.Password
        });

        return customer;
    }
}
