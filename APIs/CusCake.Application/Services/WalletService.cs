using CusCake.Application.GlobalExceptionHandling.Exceptions;
using CusCake.Domain.Constants;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IWalletService
{
    Task<Wallet> MakeBillingAsync(Wallet wallet, double amount, string type, params object[] optionalParams);
}


public class WalletService(IUnitOfWork unitOfWork) : IWalletService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Wallet> MakeBillingAsync(Wallet wallet, double amount, string type, params object[] optionalParams)
    {
        wallet.Balance += amount;

        if (wallet.Balance < 0) throw new BadRequestException("Wallet is not enough money!");

        var transaction = new WalletTransaction
        {
            Amount = amount,
            WalletId = wallet.Id,
            TransactionType = type,
            OrderTargetId = optionalParams.Length > 0 ? (Guid)optionalParams[0] : null,
            OrderTargetCode = optionalParams.Length > 1 ? (string)optionalParams[1] : null,
            TargetUserId = optionalParams.Length > 2 ? (Guid)optionalParams[2] : null,
            TargetUserType = optionalParams.Length > 3 ? (string)optionalParams[3] : null
        };

        // Tạo content với placeholder
        var content = WalletTransactionTypeConstants.GetContentByType(type)
            .Replace("{Amount}", amount.ToString("#,##0"))
            .Replace("{TransactionId}", transaction.Id.ToString())
            .Replace("{OrderCode}", transaction.OrderTargetCode ?? "N/A");

        transaction.Content = content;

        await _unitOfWork.WalletTransaction.AddAsync(transaction);
        _unitOfWork.WalletRepository.Update(wallet);
        await _unitOfWork.SaveChangesAsync();

        return wallet;
    }
}
