using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.NullException
{
    public class Employee2
    {
        public int Id { get; set; }
        public PayRate PayRate { get; private set; }
        public int? PayRateId { get; private set; }
    }
}
