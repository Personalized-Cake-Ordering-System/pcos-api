namespace CusCake.Application.ViewModels.CustomerModels;

public class CustomerViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
