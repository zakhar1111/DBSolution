using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DBWorkAround
{
    public partial class ConcatExpresion
    {
        public Expression ConstructGreetingExpression()
        {
            var personNameParameter = Expression.Parameter(typeof(string), "personName");

            // Condition
            var isNullOrWhiteSpaceMethod = typeof(string).GetMethod(nameof(string.IsNullOrWhiteSpace));

            var condition = Expression.Not(Expression.Call(isNullOrWhiteSpaceMethod, personNameParameter));

            // True clause

            var concatMethod =  typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });
            var trueClause = Expression.Call(
                concatMethod,
                Expression.Constant("Greetings, "),
                personNameParameter);
            //Expression.Add(Expression.Constant("Greetings, "),personNameParameter);

            // False clause
            var falseClause = Expression.Constant(null, typeof(string));

            // Ternary conditional
            return Expression.Condition(condition, trueClause, falseClause);
        }

        public Func<string, string> EvaluatedConcatExpresionFunction()
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
            var falseClause = Expression.Constant(string.Empty, typeof(string));

            var conditional = Expression.Condition(condition, trueClause, falseClause);

            var lambda = Expression.Lambda<Func<string, string>>(conditional, personNameParameter);

            return lambda.Compile();
        }

        public static void ConcatExpresionTest()
        {
            var temp = new ConcatExpresion();
            var exp = temp.ConstructGreetingExpression();
            var expToStr = exp.ToString();
            //var lambda = Expression.Lambda<Func<string, string>>(exp, true).Compile().ToString();
            //var expStr = lambda("like");

            var exp2 = temp.EvaluatedConcatExpresionFunction();
            var toStrexp2 = exp2.ToString();
            var exp2Str = exp2("john");
            var exp2Str2 = exp2("");
        }


    }
}
