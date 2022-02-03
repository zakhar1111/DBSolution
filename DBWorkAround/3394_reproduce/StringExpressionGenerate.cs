using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DBWorkAround._3394_reproduce
{
    public partial class Reproduce3394
    {

        //Construct Expression by API
        public Func<string, string> ConstructGreetingFunction()
        {
            var personNameParameter = Expression.Parameter(typeof(string), "personName");

            // Condition
            var isNullOrWhiteSpaceMethod = typeof(string)
                .GetMethod(nameof(string.IsNullOrWhiteSpace));

            var condition = Expression.Not(
                                            Expression.Call(isNullOrWhiteSpaceMethod, personNameParameter));

            // True clause
            var concatMethod = typeof(string)
                .GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });

            var trueClause = Expression.Call(
                                            concatMethod,
                                            Expression.Constant("Greetings, "),
                                            personNameParameter);

            // False clause
            var falseClause = Expression.Constant(null, typeof(string));

            var conditional = Expression.Condition(condition, trueClause, falseClause);

            var lambda = Expression.Lambda<Func<string, string>>(conditional, personNameParameter);

            return lambda.Compile();
        }

        //String Expression
        public bool IsEquals(string a, string b)
        {
            return string.Equals(a, b);

        }
        public Func<string, bool> ConstructCompareFunction()
        {
            var leftParameter = Expression.Parameter(typeof(string), "left");
            var rightParameter = Expression.Parameter(typeof(string), "right");
            //Expression.Constant("Greetings");

            MethodInfo equalsMethod = typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string), typeof(string) });

            var equalsRes = Expression.Call(equalsMethod, leftParameter, Expression.Constant("Greetings"));

            //var condition = Expression.GreaterThan(equalsRes, Expression.Constant(0));

            var lambda = Expression.Lambda<Func<string, bool>>(equalsRes, leftParameter);

            return lambda.Compile();
        }

        public Func<string, bool> ConstructCompareFunction2()
        {
            Func<string, bool> ret = (str) => string.Equals(str, "Dan");
            return ret;
        }

        public Expression<Func<string, bool>> ConstructCompareFunction3()//OK
        {
            var leftParameter = Expression.Parameter(typeof(string), "left");
            var rightParameter = Expression.Parameter(typeof(string), "right");
            //Expression.Constant("Greetings");

            MethodInfo equalsMethod = typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string), typeof(string) });

            var equalsRes = Expression.Call(equalsMethod, leftParameter, Expression.Constant("Greetings"));

            //var condition = Expression.GreaterThan(equalsRes, Expression.Constant(0));

            var lambda = Expression.Lambda<Func<string, bool>>(equalsRes, leftParameter);

            return lambda;

        }

    }
}
