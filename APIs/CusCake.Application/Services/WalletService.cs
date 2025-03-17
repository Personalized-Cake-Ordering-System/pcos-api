using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IWalletService
{
    Task<Wallet> MakeBillingAsync(Wallet wallet, double amount);
}


public class WalletService(IUnitOfWork unitOfWork) : IWalletService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Wallet> MakeBillingAsync(Wallet wallet, double amount)
    {
        wallet.Balance += amount;

        await _unitOfWork.WalletTransaction.AddAsync(new WalletTransaction
        {
            Amount = amount,
            WalletId = wallet.Id,
            TransactionType = WalletTransactionTypeConstants.BILL_PAYMENT
        });

        _unitOfWork.WalletRepository.Update(wallet);

        await _unitOfWork.SaveChangesAsync();
        return wallet;
    }
}
