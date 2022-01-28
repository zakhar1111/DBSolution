using System;
using System.Linq;

using LinqToDB;
using LinqToDB.DataProvider.SqlServer;


namespace DBWorkAround._3394_reproduce
{
    public class Reproduce3394
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

        public static void Good3394Test()
        {
            CreateFixtureTo3394Reproduce(Program.StorTestConnectionString);

            using (var db = new Db())
            {
                
                var id = Guid.NewGuid();
 
                var res = db.Entity.Where(item1 => item1.Id.ToString().CompareTo(id.ToString()) > 0);
                var resQuery = res.ToList();

            }
        }

        public static void Reproduce3394Test()
        {
            CreateFixtureTo3394Reproduce(Program.StorTestConnectionString);

            using (var db = new Db())
            {

                var id = Guid.NewGuid();

                var res = db.Entity.Where(item1 => item1.Id.CompareTo(id) > 0);
                var resQuery = res.ToList();

            }
        }
    }

}
