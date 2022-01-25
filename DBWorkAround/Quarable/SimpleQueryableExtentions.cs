using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DBWorkAround.Quarable
{
    public static class SimpleQueryableExtentions
    {
        public static ISimpleQueryable<TSource> Where<TSource>(this ISimpleQueryable<TSource> queryable,
                                                                Expression<Func<TSource, bool>> predicate)
        {
            string newQueryDescription = queryable.QueryDescription + ".Where(" + predicate.ToString() + ")";
            return queryable.CreateNewQueryable(newQueryDescription);
        }

        public static int Count<TSource>(this ISimpleQueryable<TSource> queryable)
        {
            string newQueryDescription = queryable.QueryDescription + ".Count()";
            ISimpleQueryable<TSource> newQueryable = queryable.CreateNewQueryable(newQueryDescription);
            return newQueryable.Execute<int>();
        }
    }
}
