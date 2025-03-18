using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IWalletService
{
    Task<Wallet> MakeBillingAsync(Wallet wallet, double amount, string type);
}


public class WalletService(IUnitOfWork unitOfWork) : IWalletService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Wallet> MakeBillingAsync(Wallet wallet, double amount, string type)
    {
        wallet.Balance += amount;

        if (wallet.Balance < 0) throw new BadRequestException("Wallet is not enough money!");

        await _unitOfWork.WalletTransaction.AddAsync(new WalletTransaction
        {
            Amount = amount,
            WalletId = wallet.Id,
            TransactionType = type
        });

        _unitOfWork.WalletRepository.Update(wallet);

        await _unitOfWork.SaveChangesAsync();
        return wallet;
    }
}
