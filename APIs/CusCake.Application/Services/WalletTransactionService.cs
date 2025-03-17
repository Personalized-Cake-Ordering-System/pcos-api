using System.Linq.Expressions;
using CusCake.Application.Extensions;
using CusCake.Application.Utils;
using CusCake.Domain.Entities;

namespace CusCake.Application.Services;

public interface IWalletTransactionService
{
    Task<(Pagination, List<WalletTransaction>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<WalletTransaction, bool>>? filter = null);
}


public class WalletTransactionService(IUnitOfWork unitOfWork) : IWalletTransactionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(Pagination, List<WalletTransaction>)> GetAllAsync(int pageIndex = 0, int pageSize = 10, Expression<Func<WalletTransaction, bool>>? filter = null)
    {
        var includes = QueryHelper.Includes<WalletTransaction>(x => x.Wallet);
        return await _unitOfWork.WalletTransaction.ToPagination(pageIndex, pageSize, filter: filter, includes: includes);
    }
}
