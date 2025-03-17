using CusCake.Domain.Entities;

namespace CusCake.Application.Repositories;
public interface IAuthRepository : IGenericRepository<Auth>
{
}
public interface IWalletRepository : IGenericRepository<Wallet>
{
}
public interface IWalletTransactionRepository : IGenericRepository<WalletTransaction>
{
}

