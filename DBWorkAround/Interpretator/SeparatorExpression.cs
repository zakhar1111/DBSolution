using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.Interpretator
{
    class SeparatorExpression : AbstractExpression
    {
        public void Evaluate(Context context)
        {
            string expression = context.expression;
            context.expression = expression.Replace('-', '/');
        }
    }
}
