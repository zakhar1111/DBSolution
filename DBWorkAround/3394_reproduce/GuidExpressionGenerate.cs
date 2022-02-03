using System;
using System.Linq.Expressions;


namespace DBWorkAround._3394_reproduce
{
    public partial class Reproduce3394
    {

        public bool ConstructCompare(Guid a, Guid b)
        {
            return a.CompareTo(b) > 0;
        }


        //Guid Compare Expression
        public Expression<Func<Guid, bool>> ConstructCompare()//OK  a = a.CompareTo(Guid.Empty) > 0
        {
            var leftParameter = Expression.Parameter(typeof(Guid), "left");
            var fixtureGuid = Guid.Empty; // Guid.NewGuid();
            var rightConst = Expression.Constant(fixtureGuid);
            var compareToMethod = typeof(GuidExt).GetMethod(nameof(GuidExt.CompareTo), new[] { typeof(Guid), typeof(Guid) });
            var compareRes = Expression.Call(compareToMethod, leftParameter, rightConst);

            // var lambda = Expression.Lambda<Func<Guid, int>>(compareRes, leftParameter);//optional

            var condBool = Expression.GreaterThan(compareRes, Expression.Constant(-1));
            var ternaryCondition = Expression.Condition(condBool, Expression.Constant(true), Expression.Constant(false));
            var lambda1 = Expression.Lambda<Func<Guid, bool>>(ternaryCondition, leftParameter);

            return lambda1;
        }

        public Expression<Func<Guid, int>> ConstructCompare2()//OK  a = a.CompareTo(Guid.Empty)
        {
            var leftParameter = Expression.Parameter(typeof(Guid), "left");
            var fixtureGuid = Guid.Empty; // Guid.NewGuid();
            var rightConst = Expression.Constant(fixtureGuid);
            var compareToMethod = typeof(GuidExt).GetMethod(nameof(GuidExt.CompareTo), new[] { typeof(Guid), typeof(Guid) });
            var compareRes = Expression.Call(compareToMethod, leftParameter, rightConst);

            var lambda = Expression.Lambda<Func<Guid, int>>(compareRes, leftParameter);//optional

            return lambda;
        }

        public Expression<Func<Guid, Guid, int>> ConstructCompare3()//OK (a,b) = a.CompareTo(b)
        {
            var leftParameter = Expression.Parameter(typeof(Guid), "left");
            var rightParametr = Expression.Parameter(typeof(Guid), "right");

            var compareToMethod = typeof(GuidExt).GetMethod(nameof(GuidExt.CompareTo), new[] { typeof(Guid), typeof(Guid) });
            var compareRes = Expression.Call(compareToMethod, leftParameter, rightParametr);

            var lambda = Expression.Lambda<Func<Guid, Guid, int>>(compareRes, leftParameter, rightParametr);//optional

            return lambda;
        }

        public Expression<Func<Guid, Guid, bool>> ConstructCompare4()
        {
            Expression<Func<Guid, Guid, bool>> ret = (a, b) => a.CompareTo(b) > -1;
            return ret;
        }

    }
}
