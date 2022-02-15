using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.Interpretator
{
    public interface AbstractExpression
    {
        void Evaluate(Context context);
    }
}
