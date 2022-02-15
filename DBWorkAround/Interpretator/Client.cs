using DBWorkAround.Interpretator;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    public partial class Program
    {
        public static void BuildAndInterpret()
        {
            var objExpressions = new List<AbstractExpression>();
            var context = new Context(DateTime.Now);
            context.expression = $"02-15-2022";

            string[] strArray = context.expression.Split('-');
            foreach (var item in strArray)
            {
                if (item == "DD")
                {
                    objExpressions.Add(new DayExpression());
                }
                else if (item == "MM")
                {
                    objExpressions.Add(new MonthExpression());
                }
                else if (item == "YYYY")
                {
                    objExpressions.Add(new YearExpression());
                }
            }

            objExpressions.Add(new SeparatorExpression());
            foreach (var obj in objExpressions)
            {
                obj.Evaluate(context);
            }

            Console.WriteLine(context.expression);
        }
    }
}
