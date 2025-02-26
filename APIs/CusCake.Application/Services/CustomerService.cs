
using System.Linq.Expressions;
using AutoMapper;
using CusCake.Application.Extensions;
using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Application.Utils;
using CusCake.Application.ViewModels.CustomerModels;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface ICustomerService
{
    Task<bool> CreateAsync(CustomerCreateModel model);
    Task<CustomerViewModel> GetByIdAsync(Guid id);

    Task<Pagination<Customer>> GetAllAsync(int pageIndex = 0, int pageSize = 10);

    Task DemoAsync();
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

    public async Task DemoAsync()
    {
        List<Guid> shopImageFiles = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

        // Tạo đối tượng Bakery
        var bakery = new Bakery
        {
            ShopName = "Delicious Bakery",
            OwnerName = "John Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            Address = "123 Bakery Street, Sweet Town",
            IdentityCardNumber = "123456789",
            FrontCardFileId = Guid.NewGuid(),
            BackCardFileId = Guid.NewGuid(),
            TaxCode = "TAX123456",
            ShopImageFiles = shopImageFiles // Gán danh sách GUID
        };
        await _unitOfWork.BakeryRepository.AddAsync(bakery);
        await _unitOfWork.SaveChangesAsync();
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
