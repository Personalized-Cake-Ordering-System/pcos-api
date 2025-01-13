using CusCake.Application.Repositories;

namespace CusCake.Application;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    Task<bool> SaveChangesAsync();
}