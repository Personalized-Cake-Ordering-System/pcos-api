using CusCake.Application.Repositories;

namespace CusCake.Application;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    IBakeryRepository BakeryRepository { get; }
    Task<bool> SaveChangesAsync();
}