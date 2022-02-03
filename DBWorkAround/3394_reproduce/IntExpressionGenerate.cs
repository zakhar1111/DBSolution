using System;
using System.Linq.Expressions;

namespace DBWorkAround._3394_reproduce
{
    public partial class Reproduce3394
    {

        //Int Compare Expression
        public Expression<Func<int, bool>> ConstructGreater()//OK
        {
            var leftParameter = Expression.Parameter(typeof(int), "left");
            var condBool = Expression.GreaterThan(leftParameter, Expression.Constant(0));

            //Expression.Not(leftParameter, Expression.Constant(0));
            var conditional = Expression.Condition(condBool, Expression.Constant(true), Expression.Constant(false));

            var lambda = Expression.Lambda<Func<int, bool>>(condBool, leftParameter);
            return lambda;
        }

        public Expression<Func<int, bool>> ConstructGreater2()//TODO
        {
            var leftParameter = Expression.Parameter(typeof(int), "left");
            var condBool = Expression.GreaterThan(leftParameter, Expression.Constant(0));

            //Expression.Not(leftParameter, Expression.Constant(0));
            var conditional = Expression.Condition(condBool, Expression.Constant(true), Expression.Constant(false));

            var lambda = Expression.Lambda<Func<int, bool>>(condBool, leftParameter);
            return lambda;
        }
    }
}
