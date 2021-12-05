using LinqToDB.Data;
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
                var query = 
                from p in db.GetTable<Cars>()
                        join pp in db.GetTable<Owners>() 
                        on p.OwnerName equals pp.id
                        select new
                        {
                            Name = p.Name,
                            Color = p.Color,
                            OwnerName = pp.id.ToString(),
                            Owner = pp.Owner,
                            ID = pp.id
                        };
                foreach (var elem in query)
                {
                    Console.WriteLine($"Name : {elem.Name},Color : {elem.Color}, OwnerName : {elem.OwnerName}, Owner: {elem.Owner}, ID: {elem.ID}");
                }
                Console.WriteLine($"{DataConnection.TraceSwitch.ToString()}");

                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~Print Cars Table~~~~~~~~~~~~~~~~~");
                var q = db.GetTable<Cars>().Select(n => n);
                foreach (var elem in q)
                {
                    Console.WriteLine(elem.ToString());
                }

                /*
                Console.WriteLine("+++++++++++++++++++++Print Cars & Owners Joins++++++++++++++++");
                var qq = from c in db.GetTable<Cars>()
                    join pp in db.GetTable<Owners>() 
                    on c.OwnerName equals pp.id
                    where pp.id > 0
                    select c;

                foreach (var elem in qq)
                {
                    Console.WriteLine($"Name : {elem.Name},Color : {elem.Color}, OwnerName : {elem.OwnerName}");
                }
                */
            }
        }
    }
}
