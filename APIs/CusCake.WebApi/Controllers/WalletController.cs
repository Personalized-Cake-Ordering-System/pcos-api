using System.Linq.Expressions;
using CusCake.Application.Services;
using CusCake.Application.ViewModels;
using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class WalletController(IWalletTransactionService walletTransactionService) : BaseController
{
    private readonly IWalletTransactionService _walletTransactionService = walletTransactionService;

    [HttpGet("{id}/transactions")]
    [Authorize]
    public async Task<IActionResult> GetAllTransactions(
        Guid id,
        int pageIndex = 0,
        int pageSize = 10
    )
    {
        Expression<Func<WalletTransaction, bool>> filter = x =>
                         (x.WalletId == id);
        var result = await _walletTransactionService.GetAllAsync(pageIndex, pageSize, filter);

        return Ok(ResponseModel<object, object>.Success(result.Item2, result.Item1));

    }

}