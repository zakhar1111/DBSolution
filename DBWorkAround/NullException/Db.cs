using DBWorkAround.NullException;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.DataProvider.SqlServer;

namespace DBWorkAround
{
    public partial class Db : DataConnection
    {
        public Db() : //base(ProviderName.SqlServer2017, "data source=localhost;initial catalog=xxx;integrated security=True;MultipleActiveResultSets=True") { }
             base(ProviderName.SqlServer2012, Program.StorTestConnectionString)
        { }
        public ITable<Employee> Employees => GetTable<Employee>();

        public ITable<Employee2> Employees2 => GetTable<Employee2>();

    }
}
