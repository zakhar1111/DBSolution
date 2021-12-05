using System;
using LinqToDB.Mapping;
using System.Linq;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Text;
using DBWorkAround.NullException;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB;

namespace DBWorkAround
{
    public partial class Db
    {
        public static void Test()
        {
            var builder = MappingSchema.Default.GetFluentMappingBuilder();

            builder.Entity<Employee>()
                 .Association(x => x.PayRate, x => x.PayRateId, x => x.Id);

            builder.Entity<PayRate>();


            using (var db = new Db())
            {
                var queryNavProp = db.Employees
                    .Select(x => new
                    {
                        x.Id,
                        PayRate = x.PayRate == null // nav property
                        ? null
                        : new
                        {
                            x.Id,
                            x.PayRate.Name,
                        }
                    })
                    .Where(item => item.PayRate.Name.Equals("test"));

                var good = queryNavProp.ToList();

                var queryFK = db.Employees
                    .Select(x => new
                    {
                        x.Id,
                        PayRate = x.PayRateId == null // FK property
                        ? null
                        : new
                        {
                            x.Id,
                            x.PayRate.Name,
                        }
                    })
                    .Where(item => item.PayRate.Name.Equals("test"));

                var bad = queryFK.ToList(); // System.NullReferenceException
            }
        }






        public static void CreateFixtureToReproduce(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                try
                {
                    db.DropTable<PayRate2>();
                    db.DropTable<Employee2>();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                db.CreateTable<PayRate2>();
                db.CreateTable<Employee2>();

                db.Insert(new PayRate2 { Id = 1, Name = "Name1" });
                db.Insert(new PayRate2 { Id = 2, Name = "Name2" });
                db.Insert(new PayRate2 { Id = 3, Name = "Test" });
                foreach (var o in db.GetTable<PayRate2>())
                    Console.WriteLine($"Id: {o.Id}, Name: {o.Name}");
            }
        }


        public static void Reproduce3371()
        {
            CreateFixtureToReproduce(Program.StorTestConnectionString);

            var builder = MappingSchema.Default.GetFluentMappingBuilder();

            builder.Entity<Employee2>()
                 .Association(x => x.PayRate, x => x.PayRateId, x => x.Id);

            builder.Entity<PayRate2>();


            using (var db = new Db())
            {
                var queryNavProp = db.Employees2
                    .Select(x => new
                    {
                        x.Id,
                        PayRate = x.PayRate == null // nav property
                        ? null
                        : new
                        {
                            x.Id,
                            x.PayRate.Name,
                        }
                    })
                    .Where(item => item.PayRate.Name.Equals("test"));

                var good = queryNavProp.ToList();

                var queryFK = db.Employees2
                    .Select(x => new
                    {
                        x.Id,
                        PayRate = x.PayRateId == null // FK property
                        ? null
                        : new
                        {
                            x.Id,
                            x.PayRate.Name,
                        }
                    })
                    .Where(item => item.PayRate.Name.Equals("test"));

                var bad = queryFK.ToList(); // System.NullReferenceException
            }

        }
    }
}
