using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace DBWorkAround
{
    public static partial class FSharpTranspiler
    {
        private class Visitor : ExpressionVisitor
        {
            private readonly StringBuilder _buffer;

            public Visitor(StringBuilder buffer)
            {
                _buffer = buffer;
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                _buffer.Append("fun (");
                _buffer.AppendJoin(", ", node.Parameters.Select(p => p.Name));
                _buffer.Append(") ->");

                return base.VisitLambda(node);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.DeclaringType == typeof(Console) &&
                    node.Method.Name == nameof(Console.WriteLine))
                {
                    _buffer.Append("printfn ");

                    if (node.Arguments.Count > 1)
                    {
                        // For simplicity, assume the first argument is a string (don't do this)
                        var format = (string)((ConstantExpression)node.Arguments[0]).Value;
                        var formatValues = node.Arguments.Skip(1).ToArray();

                        _buffer.Append("\"");
                        _buffer.Append(Regex.Replace(format, @"\{\d+\}", "%O"));
                        _buffer.Append("\" ");

                        _buffer.AppendJoin(" ", formatValues.Select(v => $"({v.ToString()})"));
                    }
                }

                return base.VisitMethodCall(node);
            }
        }

        public static string Convert<T>(Expression<T> expression)
        {
            var buffer = new StringBuilder();
            new Visitor(buffer).Visit(expression);

            return buffer.ToString();
        }
    }

    public static partial class FSharpTranspiler
    {
        public static void TranstateTest()
        {
            var fsharpCode = FSharpTranspiler.Convert<Action<int, int>>( (a, b) => Console.WriteLine("a + b = {0}", a + b));
        }
    }
}
