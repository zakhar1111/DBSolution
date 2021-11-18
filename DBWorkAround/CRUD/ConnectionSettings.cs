using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LinqToDB.Configuration;
using LinqToDB;

namespace DBWorkAround
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }
    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
            => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "StorTest";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "StorTest",
                        ProviderName = ProviderName.SqlServer,
                        ConnectionString = @"Data Source=(localdb)\ProjectsV13;Database=StorTest;user=DBAuser;password=dbuserpasswordqwWA12!@;"
                    };
            }
        }
    }
}
