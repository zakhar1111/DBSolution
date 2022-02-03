using LinqToDB;
using LinqToDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DBWorkAround._3394_reproduce
{
    
    static class GuidCompareExtension
    {
        [ExpressionMethod(nameof(GuidCompareToImpl))]
        public static bool GuidCompareTo(Guid left, Guid right)
        {
            return left.CompareTo(right) > 0;
        }

        private static Expression<Func<Guid, Guid, bool>> GuidCompareToImpl()
        {
            Expression<Func<Guid, Guid, bool>> ret = (a, b) => a.CompareTo(b) > 0;
            return ret;
        }

        static GuidCompareExtension()
        {
            LinqToDB.Linq.Expressions.MapMember( (Guid left, Guid right) => left.CompareTo(right) > 0 , (v, w)  => GuidCompareExtension.GuidCompareTo(v, w));
        }
    }

}
