using LinqToDB;
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
     
        private static void ReadTableWithLinq(string connectionString)
        {
            using (var db = SqlServerTools.CreateDataConnection(connectionString))
            {
                var tbl = db.GetTable<Products>();
                Console.WriteLine(tbl.SqlText);
            }
        }

        private static void ReadFromTable(string connectionString)
        {
            using (var db =  SqlServerTools.CreateDataConnection(connectionString))
            {
                //var q = db.GetTable<Products>().ToList<Products>().Select(l => l);//.Select(j => j);
                /*var tb = db.GetTable<Products>()
                var query =
                                from c in db.Category
                                from p in db.Product.InnerJoin(pr => pr.CategoryID == c.CategoryID)
                                where !p.Discontinued
                                select c;*/
                var newProduct = new Products
                {
                    Name = "glovers",
                    Description = "srotware",
                    Category = "martial-arts",
                    Price = 10.0m,
                };
                var qq = db.GetTable<Products>().Append(newProduct);
                var q = from c in db.GetTable<Products>() select c;
                foreach (var elem in q)
                    Console.WriteLine(elem.ToString());
                //Console.WriteLine(elem.ToString());
            }

        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private static void CreateTable(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                try
                {
                    db.DropTable<ToDoTable>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                db.CreateTable<ToDoTable>();

                try
                {
                    db.DropTable<ToDoTable2>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                db.CreateTable<ToDoTable2>();

            }
        }

        private static void InsertRaw(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert single obj
                db.Insert(new ToDoTable
                {
                    Name = "Migrate App one",
                    Description = "Reconfig pipelines"
                });
            }

        }
        private static void InsertModifiedRaw(string con)
        {
            //using (var db = new DataConnection(con))
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert expresion tree
                db.GetTable<ToDoTable>().Insert(() => new ToDoTable
                {
                    Name = "Crazy Frog",
                    CreatedON = DateTime.Now
                });
            }

        }

        private static void InsertModifiedRaw2(string con)
        {
            //using (var db = new DataConnection(con))
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert into/ select
                db.GetTable<ToDoTable>()
                  .Where(t => t.Name == "Crazy Frog")
                  .Insert(db.GetTable<ToDoTable2>(),t => new ToDoTable2
                    {
                        Name = t.Name + "vvv",
                        CreatedON = t.CreatedON.Value.AddDays(1)
                    }
                    );
            }

        }

        private static void InsertIntoRaw3(string con)
        {
            //using (var db = new DataConnection(con))
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert into/ where
                db.GetTable<ToDoTable>()
                  .Where(t => t.Name == "Crazy Frog")
                  .Into(db.GetTable<ToDoTable2>())
                      .Value(t => t.Name, t => t.Name + "YYY")
                      .Value(t => t.CreatedON, t => t.CreatedON.Value.AddDays(1))
                  .Insert();

            }

        }

        private static void InsertWithIdentityRaw(string con)
        {
            //using (var db = new DataConnection(con))
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert with identity
                var identity = db.GetTable<ToDoTable>()
                    .InsertWithIdentity(() => new ToDoTable
                    { Name = "Crazy Frog", CreatedON = Sql.CurrentTimestamp }
                   );
                Console.WriteLine(identity);

            }

        }

        private static void InserOrUpdateRaw(string con) //err
        {
            //using (var db = new DataConnection(con))
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                //insert or update
                var identity = db.GetTable<ToDoTable>()
                    .InsertOrUpdate(
                    () => new ToDoTable
                    {
                        ID = 5,
                        Name = "Crazy Frog"
                    },
                    t => new ToDoTable
                    {

                        Name = "Crazy Frog ZZZZ"
                    }
                   );
                Console.WriteLine(identity);

            }

        }

        private static void InserOrReplaceRaw(string con) //err
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.InsertOrReplace(
                    new ToDoTable
                    {
                        ID = 5,
                        Name = "Crazy Fox"
                    });
            }
        }

        private static void UpdateRaw(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.Update(new ToDoTable
                    {
                        ID = 3,
                        Name = "Crazy Fox ZZZ",
                        Description = "game playing",
                        CreatedON = DateTime.Now.Date
                    });
            }
        }

        private static void UpdateAllColumn(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.GetTable<ToDoTable>()
                    .Where(t => t.ID == 1)
                    .Update(t => new ToDoTable
                    {
                        Name = "Crazy Fox ZZZ"
                    });
            }
        }
        private static void UpdateColumn(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.GetTable<ToDoTable>()
                    .Where(t => t.ID == 1)
                    .Update(t => new ToDoTable
                    {
                        Name = "Crazy Fox ZZZ"
                    });
            }
        }

        private static void UpdateWhereColumn(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.GetTable<ToDoTable>()
                    .Update(t => t.ID == 2, t => new ToDoTable
                    {
                        Name = "Crazy Fox QQ"
                    });
            }
        }

        private static void UpdateWhereSetColumn(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.GetTable<ToDoTable>()
                    .Where(t => t.ID == 2)
                    .Set(t => t.Name, t => "Mad Frog WWW")
                    .Set(t => t.CreatedON, t => t.CreatedON.Value.AddDays(2))
                    .Update();

            }
        }

        private static void UpdateDelete(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.GetTable<ToDoTable>()
                    .Where(t => t.ID == 1)
                    .Delete();

            }
        }

        private static void BulkCopy(string con)
        {
            using (var db = SqlServerTools.CreateDataConnection(con))
            {
                db.BulkCopy(Enumerable.Range(0, 100)
                                .Select(n => new ToDoTable
                                {
                                    Name = n.ToString(),
                                })
                    );


            }
        }


    }

}
