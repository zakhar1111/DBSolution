using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround.Quarable
{
    public partial class FakeSimpleQueryable
    {
        public static void TestQuarable()
        {
            var provider = new FakeSimpleQueryable<string>("", null);
            int result = provider
                   .Where(s => s.Contains("substring"))
                   .Where(s => s != "some string")
                   .Count();

        }
    }
}
