using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.Quarable
{
    public partial class FakeSimpleQueryable<TSource> : ISimpleQueryable<TSource>
    {
        private readonly object _dataSource;
        public string QueryDescription { get; private set; }

        public FakeSimpleQueryable(string queryDescription, object dataSource)
        {
            _dataSource = dataSource;
            QueryDescription = queryDescription;
        }

        public ISimpleQueryable<TSource> CreateNewQueryable(string queryDescription)
        {
            return new FakeSimpleQueryable<TSource>(queryDescription, _dataSource);
        }

        public TResult Execute<TResult>()
        {
            //Здесь должна быть обработка QueryDescription и применение запроса к dataSource
            //throw new NotImplementedException();
            // if (_dataSource == null)
            //var res = QueryDescription.GetType();
            return  (TResult)_dataSource; //default;// QueryDescription.Length;

        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return Execute<IEnumerator<TSource>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
