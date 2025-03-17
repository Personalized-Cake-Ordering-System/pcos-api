using CusCake.Application.Annotations;
using CusCake.Application.ViewModels.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace CusCake.WebApi.Controllers;

public class TransactionController : BaseController
{

    /// <summary>
    /// It is webhook to handle payment
    /// </summary>
    [HttpPost("/transaction-webhook-handler")]
    [SepayAuthorize]
    public async Task<IActionResult> TransactionWebhookHandler(TransactionWebhookModel model)
    {
        await Task.Delay(0);

        return Ok(new { success = true });
    }
}