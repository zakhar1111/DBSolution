using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;

//https://github.com/linq2db/linq2db/issues/3394
//http://blog.linq2db.com/                        How to teach LINQ to DB convert custom .NET methods and objects to SQL

//https://github.com/linq2db/linq2db/issues/2468  MapMember
//https://github.com/linq2db/linq2db/issues/1878  incorrectly formed using GUIDs   mappingSchema.SetValueToSqlConverter
//https://github.com/linq2db/linq2db/issues/973   sql operator "in"                IExtensionCallBuilder
namespace DBWorkAround._3394_reproduce
{
    static class GuidExt
    {
        public static int CompareTo( Guid a, Guid b)
        {
            return a.CompareTo(b);
        }
    }

    public partial class Reproduce3394
    {
        public static void CreateFixtureTo3394Reproduce(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                try
                {
                    db.DropTable<Entity>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                db.CreateTable<Entity>();

                db.Insert(new Entity { Id = Guid.NewGuid() });
                db.Insert(new Entity { Id = Guid.NewGuid() });
                db.Insert(new Entity { Id = Guid.NewGuid() });

                foreach (var o in db.GetTable<Entity>())
                    Console.WriteLine($"Id: {o.Id} ");
            }
        }

        public static void Reproduce3394TestGood()
        {
            CreateFixtureTo3394Reproduce(Program.StorTestConnectionString);

            using (var db = new Db())
            {
                
                var id = Guid.NewGuid();
 
                var res = db.Entity.Where(item1 => item1.Id.ToString().CompareTo(id.ToString()) > 0);
                var resQuery = res.ToList();

            }
        }

        public static void Reproduce3394TestBroken()
        {
            CreateFixtureTo3394Reproduce(Program.StorTestConnectionString);
            var local = new Reproduce3394();

            using (var db = new Db())
            {

                var id = Guid.NewGuid();



                //actual from start
                var res = db.Entity.Where(item1 => item1.Id.CompareTo(id) > 0); //err
                var goodString = db.Entity.Where(item1 => item1.Id.ToString().CompareTo(id.ToString()) > 0);//OK
                var goodEquals = db.Entity.Where(x => x.Id.Equals(id));


                //var CompareToExp = local.ConstructCompare3().Compile(); // guidCompareExpression.Compile();
                //guidCompareExpressionCompiled(Guid.Empty))

                //var res = db.Entity.Where(item1 => CompareToExp(item1.Id, id) > 0 );err
                //var res = db.Entity.Where(local.ConstructCompare3()); c9mpile err



                //Expression<Func<Guid, Guid, bool>> ret = (a, b) => a.CompareTo(b) > 0;


                //err
                Expression<Func<Entity, bool>> retEntity = (a) => a.Id.CompareTo(id) > 0; //err for local lambda + closure
                var resLocalLambda = db.Entity.Where(retEntity);



                //Lambda 
                Expression<Func<Guid, Guid, bool>>  p = local.ConstructCompare4();
                var bronekLambda = db.Entity.Where(x => p.Compile()(x.Id, id));


                //Expressions.MapMember((StatusEnum v) => v.ToString(), v => MyExtensions.StatusEnumToString(v));

                Expression<Func<Guid,  bool>> lambdaAPI = local.ConstructCompare();
                var compiled_lambdaAPI = lambdaAPI.Compile();
                var brokenAPI = db.Entity.Where( el => compiled_lambdaAPI(el.Id));

                //LinqToDB.Linq.Expressions.MapMember((Guid left, Guid right) => left.CompareTo(right) > 0, (v, w) => GuidCompareExtension.GuidCompareTo(v, w));
                var resMapExtention = db.Entity.Where( item1 => GuidCompareExtension.GuidCompareTo(item1.Id, id) );

                var resQuery = goodString.ToList();

            }
            ExpressionTest();
        }

        
    }

}
