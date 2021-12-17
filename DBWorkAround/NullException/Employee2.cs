using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.NullException
{
    public class Employee2
    {
        public int Id { get; set; }
        public PayRate2 PayRate { get; private set; }
        public int? PayRateId { get; private set; }

        public Employee2()
        {
            Id = 0;
            PayRate = new PayRate2();
            PayRate.Id = 0;
            PayRate.Name = "test";
            PayRateId = 0;
        }
    }
}
