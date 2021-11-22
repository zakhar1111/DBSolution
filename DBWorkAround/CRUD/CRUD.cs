using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB;
using LinqToDB.Data;
using System.Linq;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Configuration;

namespace DBWorkAround
{

    public class StorTestConnection : LinqToDB.Data.DataConnection
    {
        public StorTestConnection() : //SqlServerTools.CreateDataConnection(Program.StorTestConnectionString)
                                      //base(Program.StorTestConnectionString) 
            base(LinqToDB.ProviderName.SqlServer2012, Program.StorTestConnectionString)

        { }



        // ... other tables ...
        public ITable<ToDoTable> ToDoTables
        {
            get { return this.GetTable<ToDoTable>(); }
        }
        public ITable<ToDoTable2> ToDoTables2 => GetTable<ToDoTable2>();

        public static void Read()
        {
            using (var db = new StorTestConnection())
            {
                var notSorted = db.ToDoTables;
                //var sorted = db.ToDoTables.OrderByDescending(l =>l.Name).Select(a => a);
                //var unsortedSelection = from p in db.ToDoTables   select p;

                foreach (var o in notSorted)
                    Console.WriteLine($"ID: {o.ID}, Name: {o.Name}, Description: {o.Description}, Date: {o.CreatedON}");
            }
        }

        public static void WriteNewColumn()
        {
            using (var db = new StorTestConnection())
            {
                //insert single obj
                db.Insert(new ToDoTable { Name = "Migrate App one", Description = "Reconfig pipelines" });
            }
        }

        public static void WriteColumn()
        {
            using (var db = new StorTestConnection())
            {
                //insert expresion tree
                db.ToDoTables.Insert(() => new ToDoTable
                {
                    Name = "Crazy Frog II",
                    CreatedON = DateTime.Now
                });
            }
        }

        public static void WriteExistedColumnToEnd()
        {
            using (var db = new StorTestConnection())
            {
                db.ToDoTables
                  .Where(t => t.Name == "Migrate App one")
                  .Insert(db.ToDoTables,t => new ToDoTable
                    {
                        Name = t.Name + "vvv",
                        CreatedON = t.CreatedON.Value.AddDays(1)
                    });
            }
        }

        public static void WriteTo()
        {
            using (var db = new StorTestConnection())
            {
                db.ToDoTables
                  .Where(t => t.Name == "Migrate App onevvv")
                  .Into(db.ToDoTables)
                      .Value(t => t.Name, t => t.Name + "YYY")
                      .Value(t => t.CreatedON, t => t.CreatedON.Value.AddDays(1))
                  .Insert();
            }
        }

        public static void WriteIdentityRaw()
        {
            using (var db = new StorTestConnection())
            {
                //insert with identity
                var identity = db.ToDoTables
                    .InsertWithIdentity(() => new ToDoTable
                    {
                        Name = "Crazy Frog",
                        CreatedON = Sql.CurrentTimestamp
                    });
                Console.WriteLine("identity");
                Console.WriteLine(identity);
            }
        }

        public static void InserOrUpdateRaw() //err
        {
            //System.Data.SqlClient.SqlException: Cannot insert explicit value for identity column in table 'ToDoTable' when IDENTITY_INSERT is set to OFF.
            using (var db = new StorTestConnection())
            {
                //insert or update
                var identity = db.ToDoTables
                    .InsertOrUpdate(() => new ToDoTable
                    {
                        ID = 8,
                        Name = "Crazy Frog"
                    },
                    t => new ToDoTable
                    {
                        Name = "Crazy Frog ZZZZ++++"
                    });
                Console.WriteLine(identity);

            }

        }

        public static void InsertOrReplaceRaw() //err
        {
            // LinqToDB.Linq.LinqException: InsertOrReplace method does not support identity field 'ToDoTable.ID'.
            using (var db = new StorTestConnection())
            {
                db.InsertOrReplace(new ToDoTable
                    {
                        ID = 9,
                        Name = "Crazy Fox"
                    });
            }
        }

        public static void UpdateRaw()
        {
            using (var db = new StorTestConnection())
            {
                db.Update(new ToDoTable
                    {
                        ID = 8,
                        Name = "Crazy Fox UpdateRaw ZzZzZz",
                        Description = "game playing",
                        CreatedON = DateTime.Now.Date
                    });
            }
        }
        public static void UpdateColumn( )
        {
            using (var  db = new StorTestConnection())
            {
                db.ToDoTables
                    .Where(t => t.ID == 1)
                    .Update(t => new ToDoTable
                    {
                        Name = "Crazy Fox UpdateColumn ZZZ"
                    });
            }
        }

        public static void UpdateWhereColumn( )
        {
            using (var db = new StorTestConnection())
            {
                db.ToDoTables
                    .Update(t => t.ID == 2,t => new ToDoTable
                    {
                        Name = "Crazy Fox UpdateWhereColumn QQ"
                    });
            }
        }

        public static void UpdateWhereSetColumn( )
        {
            using (var db = new StorTestConnection())
            {
                db.ToDoTables
                    .Where(t => t.ID == 4)
                    .Set(t => t.Name, t => "Mad Frog UpdateWhereColumn WWW")
                    .Set(t => t.CreatedON, t => t.CreatedON.Value.AddDays(2))
                    .Update();

            }
        }
    }
}
