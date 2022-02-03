using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround._3394_reproduce
{
    public partial class Reproduce3394
    {

        public static void ExpressionTest()
        {
            var local = new Reproduce3394();
            var getGreatings = local.ConstructGreetingFunction();

            var greetingForJohn = getGreatings("John");

            Console.WriteLine(greetingForJohn + " testOK");

            if (local.IsEquals("a", "a")) Console.WriteLine(" testOK");

            //String Expressions
            //Func<>> : (a,b) => string.Equals(a, b)
            var getEquals = local.ConstructCompareFunction();
            if (getEquals("Greetings")) Console.WriteLine("expression testOK");

            //Expression<Func<>>  : (a,b) => string.Equals(a, b)
            var getExpressionEquals = local.ConstructCompareFunction3();
            var getExpressionEqualsCompiled = getExpressionEquals.Compile();
            if (getExpressionEqualsCompiled("Greetings")) Console.WriteLine("(a,b) => string.Equals(a, b)    testOK");

            // xInt => xInt > 0 ;
            var greatExpressionCompiled = local.ConstructGreater().Compile();
            if (greatExpressionCompiled(1)) Console.WriteLine("xInt => xInt > 0   testOK");
            else Console.WriteLine("greatExpression testBROKEN");

            //Guid Compare Expression
            // Expression<Func<>>  : a => a.CompareTo(Guid.Empty) > 0
            var guidCompareExpressionCompiled = local.ConstructCompare().Compile(); // guidCompareExpression.Compile();
            if (guidCompareExpressionCompiled(Guid.Empty)) Console.WriteLine("a => a.CompareTo(Guid.Empty) > 0       testOK");
            else Console.WriteLine("a = a.CompareTo(Guid.Empty) > 0      testBroken");


            // Expression<Func<>>  : a => a.CompareTo(Guid.Empty) 
            var last = local.ConstructCompare2().Compile();
            if (last(Guid.Empty) == 0) Console.WriteLine("a => a.CompareTo(Guid.Empty)    testOK");

            // Expression<Func<>>  : (a,b) => a.CompareTo(b) 
            var lt = local.ConstructCompare3().Compile();
            if (lt(Guid.NewGuid(), Guid.Empty) > 0) Console.WriteLine("(a,b) => a.CompareTo(b)     testOK");
            else Console.WriteLine("(a,b) => a.CompareTo(b) testOK");

            // Expression<Func<>>  : (a,b) => a.CompareTo(b) > 0;
            var lt4 = local.ConstructCompare4().Compile();
            if (lt4(Guid.Empty, Guid.Empty)) Console.WriteLine("lt4    (a,b) => a.CompareTo(b) > 0;     testOK");
            else Console.WriteLine("lt4    (a,b) => a.CompareTo(b) > 0;     test broken");
        }

    }
}
