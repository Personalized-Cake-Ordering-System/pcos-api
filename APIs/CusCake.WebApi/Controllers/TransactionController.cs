using CusCake.Application.Annotations;
using CusCake.Application.Services;
using CusCake.Application.ViewModels.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class TransactionController : BaseController
{

    private readonly ITransactionService _transactionService;
    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    /// <summary>
    /// It is webhook to handle payment
    /// </summary>
    [HttpPost("/transaction-webhook-handler")]
    [SepayAuthorize]
    public async Task<IActionResult> TransactionWebhookHandler(TransactionWebhookModel model)
    {
        await _transactionService.HandlerWebhookEvent(model);

        return Ok(new { success = true });
    }
}