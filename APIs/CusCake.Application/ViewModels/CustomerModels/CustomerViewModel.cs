namespace CusCake.Application.ViewModels.CustomerModels;

using System.Text.Json.Serialization;

public class CustomerViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = default!;

    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
