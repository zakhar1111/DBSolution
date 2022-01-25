using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.Quarable
{
    public interface ISimpleQueryable<TSource> : IEnumerable<TSource>
    {
        string QueryDescription { get; }
        ISimpleQueryable<TSource> CreateNewQueryable(string queryDescription);
        TResult Execute<TResult>();
    }
}
