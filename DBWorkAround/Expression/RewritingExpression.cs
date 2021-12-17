using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DBWorkAround
{
    public class VisitNodes : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Console.WriteLine($"Visited method call: {node}");
        
            return base.VisitMethodCall(node);
        }
         
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.WriteLine($"Visited binary expression: {node}");

            return base.VisitBinary(node);
        }
    }

    public class RewriteVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newMethodCall = node.Method == typeof(Math).GetMethod(nameof(Math.Sin))
                ? typeof(Math).GetMethod(nameof(Math.Cos))
                : node.Method;
            Console.WriteLine($"Visited binary expression: {node}");

            return Expression.Call(newMethodCall, node.Arguments);
        }
    }

    class RewritingExpression
    {
        public static void TraverseExpressionTest()
        {
            Expression<Func<double>> expr = () => Math.Sin(Guid.NewGuid().GetHashCode()) / 10;

            var loc = new VisitNodes().Visit(expr);
        }
    
        public static void RewriteExpressionTest()
        {
            Expression<Func<double>> expr = () => Math.Sin(Guid.NewGuid().GetHashCode()) / 10;

            var loc = new RewriteVisitor().Visit(expr);
        }
    }
}
