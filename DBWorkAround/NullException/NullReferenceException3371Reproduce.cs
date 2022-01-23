using System;
using LinqToDB.Mapping;
using System.Linq;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Text;
using DBWorkAround.NullException;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB;
using System.Linq.Expressions;

namespace DBWorkAround
{
    public partial class Db
    {
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

                db.Insert(new Employee2());


                foreach (var o in db.GetTable<PayRate2>())
                    Console.WriteLine($"Id: {o.Id}, Name: {o.Name}");

                Console.WriteLine("~~~~~~~~~~~~~Employee table ~~~~~~~~~~~~~~~~~~~~");

                foreach (var o in db.GetTable<Employee2>())
                    Console.WriteLine($"Id: {o.Id}, PayRateID:  {o.PayRateId}");
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

                Expression<Func<Employee2, bool>> predicate = (item) => item.PayRate.Name.Equals("test");

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
                    .Where(item => item.PayRate.Name.Equals("good-test"));
                //.Where(Compare());

                var good = queryNavProp.ToList();

                foreach (var item in good)
                {
                    var strRepresent = $"Id: {item.Id}, PayRateId:  {item.PayRate.Id}, PayRateName: {item.PayRate.Name}";
                    Console.WriteLine($"Id: {item.Id}, PayRateId:  {item.PayRate.Id}, PayRateName: {item.PayRate.Name}");
                }

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
                    .Where(item => item.PayRate.Name.Equals("bad-test"));
                    //.Where( item => item.Id == 0 );
                     

                var bad = queryFK.ToList(); // System.NullReferenceException

                foreach (var item in bad)
                {
                    var strRepresent = $"Id: {item.Id}, PayRateId:  {item?.PayRate?.Id}, PayRateName: {item?.PayRate?.Name}";
                    Console.WriteLine(strRepresent);
                }



            }

           

        }

        
    }

    public partial class Db
    {
        private static Expression<Func<Employee2, bool>> Compare()
        {
            Expression<Func<Employee2, bool>> EqualsExpression = (item) => item.PayRate.Name.Equals("test");
            return EqualsExpression;

        }

        private static Func<Employee2, bool> EqualsPredicate()
        {
            Expression<Func<Employee2, bool>> EqualsExpression = (item) => item.PayRate.Name.Equals("test");
            return EqualsExpression.Compile();

        }

        private static Func<Employee2, bool> Compare2()
        {
            var personNameParameter = Expression.Parameter(typeof(string), "personName");

            var EqualsMethod = typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string), typeof(string) });

            var PayRateType = (typeof(Employee2).GetProperty(nameof(Employee2.PayRate)).PropertyType).GetProperty(nameof(Employee2.PayRate.Name));

            var call = Expression.Call(EqualsMethod, Expression.Parameter(
                (typeof(PayRate2).GetProperty(nameof(PayRate2.Name)).PropertyType)),
                personNameParameter);

            var lambda = Expression.Lambda<Func<Employee2, bool>>(call, personNameParameter);

            return lambda.Compile();

        }

        public static void TestPredicateExpression()
        {
            var localPred = EqualsPredicate();
            var res = localPred(new Employee2());
        }

    }

}
