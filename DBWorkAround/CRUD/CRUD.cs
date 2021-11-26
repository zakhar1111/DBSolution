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

    public partial class StorTestConnection : DataConnection
    {
        public StorTestConnection() : //SqlServerTools.CreateDataConnection(Program.StorTestConnectionString)
                                      //base(Program.StorTestConnectionString) 
            base(ProviderName.SqlServer2012, Program.StorTestConnectionString)

        { }



        // ... other tables ...
        public ITable<ToDoTable> ToDoTables
        {
            get { return this.GetTable<ToDoTable>(); }
        }
        public ITable<ToDoTable2> ToDoTables2 => GetTable<ToDoTable2>();


    }
        
}
