using FluentValidation;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

namespace CusCake.Application.ViewModels.ReportModels;

public class ReportCreateModel
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("report_files")]
    public List<Guid> ReportFileIds { get; set; } = default!;

    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("order_id")]
    public Guid? OrderId { get; set; }
}

public class ReportUpdateModel : ReportCreateModel
{

}

public class ReportCreateModelValidator : AbstractValidator<ReportCreateModel>
{
    public ReportCreateModelValidator()
    {
        RuleFor(x => x.ReportFileIds)
                   .NotNull().WithMessage("ReportFileIds is required.")
                   .NotEmpty().WithMessage("ReportFileIds is required.")
                   .Must(files => files!.All(file => file != Guid.Empty)).WithMessage("ReportFileIds contains an invalid GUID.")
                   .Must(files => files.Distinct().Count() == files.Count).WithMessage("ReportFileIds must be unique.");

    }
}
