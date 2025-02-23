using CusCake.Application.Repositories;

namespace CusCake.Application;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    IBakeryRepository BakeryRepository { get; }
    IStorageRepository StorageRepository { get; }
    Task<bool> SaveChangesAsync();
}