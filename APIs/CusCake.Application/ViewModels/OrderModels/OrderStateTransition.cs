using CusCake.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.OrderModels;

public class OrderStateTransition<TResult>
{
    public string[] From { get; set; } = default!;
    public string To { get; set; } = default!;
    public Func<Order, bool>? Guard { get; set; }
    public Func<(Order order, List<IFormFile> files), Task<bool>>? Validate { get; set; }
    public Func<Order, Task<TResult>>? Action { get; set; }
}