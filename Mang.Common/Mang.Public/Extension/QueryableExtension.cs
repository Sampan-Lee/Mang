using System;
using System.Collections.Generic;
using System.Linq;

namespace Mang.Public.Extension
{
    public static class QueryableExtension
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> queryable, bool condition,
            Func<T, bool> predicate)
        {
            if (queryable == null)
                throw new ArgumentNullException(nameof(queryable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            return condition ? queryable.Where(predicate) : queryable;
        }
    }
}