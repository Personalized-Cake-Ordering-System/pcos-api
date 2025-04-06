namespace CusCake.Application.ViewModels.BakeryReportModels.BakeryReports;

public class OverviewModel
{
    public MetricModel? TotalRevenue { get; set; }
    public MetricModel? Orders { get; set; }
    public MetricModel? Customers { get; set; }
    public MetricModel? AverageOrder { get; set; }
}


public class MetricModel
{
    public double Amount { get; set; }
    public double Change { get; set; }
    public string ComparisonPeriod { get; set; } = "last month";
}

