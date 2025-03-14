namespace CusCake.Application.ViewModels.OrderModels;


public class OrderStateTransition
{
    public string[] From { get; set; } = default!;
    public string To { get; set; } = default!;
    public Func<Guid, bool>? Guard { get; set; }
    public Func<Guid, Task<bool>>? Validate { get; set; }
}
