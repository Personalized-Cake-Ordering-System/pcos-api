using CusCake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CusCake.WebApi.Helpers;

public static class EntitySortingMappings
{
    // Định nghĩa mapping cho AvailableCake
    public static readonly Dictionary<string, Expression<Func<AvailableCake, object>>> AvailableCakeMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        ["name"] = x => x.AvailableCakeName, // Alias
        ["price"] = x => x.AvailableCakePrice, // Alias
        ["type"] = x => x.AvailableCakeType!, // Alias
        ["created_at"] = x => x.CreatedAt,
    };

    // Định nghĩa mapping cho các entity khác
    public static readonly Dictionary<string, Expression<Func<Bakery, object>>> BakeryMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        ["name"] = x => x.BakeryName, // Alias
        ["created_at"] = x => x.CreatedAt,
    };
    public static readonly Dictionary<string, Expression<Func<BakeryMetric, object>>> BakeryMetricMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        ["total_revenue"] = x => x.TotalRevenue// Alias
    };

}