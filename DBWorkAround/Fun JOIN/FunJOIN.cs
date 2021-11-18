using LinqToDB.DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBWorkAround
{
    public partial class Program
    {
        private static void InnerJoin(string con)
        {
            Console.WriteLine("----------------Inner JOIN ----------------");
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //var query = 
                /*from p in db.GetTable<Cars>()
                        join pp in db.GetTable<Owners>() 
                        on p.OwmerName equals pp.id.ToString()  
                        select new
                        {
                            Name = p.Name,
                            Color = p.Color,
                            OwnerName = c.id.ToString(),
                            Owner = c.Owner
                        };
                         */
                var q = db.GetTable<Cars>().Select(n => n);
                foreach (var elem in q)
                {
                    Console.WriteLine(elem.ToString());
                }

                q = from c in db.GetTable<Cars>()
                    join pp in db.GetTable<Owners>() 
                    on c.OwnerName equals pp.id
                    where pp.id > 0
                    select c;

                foreach (var elem in q)
                {
                    Console.WriteLine($"Name : {elem.Name},Color : {elem.Color}, OwnerName : {elem.OwnerName}");
                }
            }
        }
    }
}
