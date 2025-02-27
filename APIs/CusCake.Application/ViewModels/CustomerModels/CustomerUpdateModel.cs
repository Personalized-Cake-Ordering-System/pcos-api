using System.ComponentModel.DataAnnotations;

namespace CusCake.Application.ViewModels.CustomerModels;

public class CustomerUpdateModel : CustomerCreateModel
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
}
