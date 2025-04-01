using System.Linq.Expressions;

namespace CusCake.Application.Utils;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> ApplyOrder<T>(
        this IQueryable<T> source,
        Expression<Func<T, object>> orderBy,
        bool isDescending,
        bool isFirstOrder = true)
    {
        if (isFirstOrder)
        {
            return isDescending
                ? source.OrderByDescending(orderBy)
                : source.OrderBy(orderBy);
        }
        else
        {
            var ordered = (IOrderedQueryable<T>)source;
            return isDescending
                ? ordered.ThenByDescending(orderBy)
                : ordered.ThenBy(orderBy);
        }
    }
}