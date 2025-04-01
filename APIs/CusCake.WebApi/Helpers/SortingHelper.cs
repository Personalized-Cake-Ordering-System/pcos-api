using CusCake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CusCake.WebApi.Helpers;

public static class SortingHelper
{
    /// <summary>
    /// Phân tích chuỗi sắp xếp và chuyển đổi thành danh sách các biểu thức sắp xếp
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <param name="sortString">Chuỗi sắp xếp, định dạng "field1:asc/desc,field2:asc/desc,..."</param>
    /// <param name="fieldMappings">Dictionary ánh xạ tên trường với biểu thức sắp xếp</param>
    /// <returns>Danh sách các biểu thức sắp xếp và hướng sắp xếp</returns>
    public static List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>? ParseSortingParameters<TEntity>(
        string? sortString,
        Dictionary<string, Expression<Func<TEntity, object>>> fieldMappings)
        where TEntity : BaseEntity
    {
        if (string.IsNullOrEmpty(sortString))
            return null;

        var orderByList = new List<(Expression<Func<TEntity, object>> OrderBy, bool IsDescending)>();
        var sortParams = sortString.Split(',');

        foreach (var param in sortParams)
        {
            var parts = param.Split(':');
            if (parts.Length != 2)
                continue;

            var fieldName = parts[0].Trim().ToLower();
            var direction = parts[1].Trim().ToLower();
            bool isDescending = direction == "desc";

            // Kiểm tra xem fieldName có trong mappings không
            if (fieldMappings.TryGetValue(fieldName, out var orderByExpression))
            {
                orderByList.Add((orderByExpression, isDescending));
            }
        }

        // Nếu không có sắp xếp hợp lệ, trả về null
        return orderByList.Count > 0 ? orderByList : null;
    }
}