using System.Linq.Expressions;

namespace CusCake.Application.Extensions;

public static class QueryHelper
{
    public static Expression<Func<T, object>>[] Includes<T>(params Expression<Func<T, object>>[] includes)
    {
        return includes;
    }
}
