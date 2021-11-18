//using LinqToDB.DataProvider.SqlServer;
using LinqToDB;
 
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Mapping;
using System;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
//using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DBWorkAround
{

    public partial class Program
    {
        const string ConnectionString =
            @"Data Source=.;Database=AdventureWorksDW2016;Integrated Security=SSPI";

        public const string ConnectionString2 =
            @"Data Source=(localdb)\ProjectsV13;Database=StorTest;user=DBAuser;password=dbuserpasswordqwWA12!@;";

        const string Con3 = @"Data Source=(localdb)\ProjectsV13;Database=StoreCars;user=DBAuser;password=dbuserpasswordqwWA12!@;";


        static void Main(string[] args)
        {

            TestCRUDE_function();

            //OK
            InnerJoin(Con3);  
            
            TestCRUDE_DataConnection();

            //OK   TestSelectADO_MySQLDataReader();
            //OK   MyDataAdapter.TestReadTable(ConnectionString2, "SELECT * FROM Products");

            //ok PrintNumLinq();
            Console.Read();
        }

       

        private static void TestCRUDE_DataConnection()
        {
            StorTestConnection.WriteNewColumn();
            StorTestConnection.WriteColumn();
            StorTestConnection.WriteExistedColumnToEnd();
            StorTestConnection.WriteTo();
            StorTestConnection.WriteIdentityRaw();
            //err StorTestConnection.InserOrUpdateRaw();
            //err StorTestConnection.InsertOrReplaceRaw(); 
            StorTestConnection.UpdateRaw();

            StorTestConnection.UpdateColumn();
            StorTestConnection.UpdateWhereColumn();
            StorTestConnection.UpdateWhereSetColumn();
            StorTestConnection.Read();
        }

        private static void TestCRUDE_function()
        {
            //OK             ReadTableWithLinq(ConnectionString2);


            //OK             ReadFromTable(ConnectionString2);


            CreateTable(ConnectionString2);
            InsertRaw(ConnectionString2);
            InsertModifiedRaw(ConnectionString2);
            InsertModifiedRaw2(ConnectionString2);
            InsertIntoRaw3(ConnectionString2);
            InsertWithIdentityRaw(ConnectionString2);

            //err InserOrUpdateRaw(ConnectionString2);
            //err InserOrReplaceRaw(ConnectionString2);

            UpdateRaw(ConnectionString2);
            UpdateColumn(ConnectionString2);
            UpdateWhereColumn(ConnectionString2);
            UpdateWhereSetColumn(ConnectionString2);
            //UpdateDelete(ConnectionString2);
            //BulkCopy(ConnectionString2);
        }




        private static void TestSelectADO_MySQLDataReader()
        {
            //OK 
            MySQLDataReader.TestSqlConnection(ConnectionString2, "SELECT * FROM Products");
            //OK 
            MySQLDataReader.TestSqlConnectionAsync(ConnectionString2, "SELECT * FROM Products");
        }
        private static void TestADO_fun()
        {
            //OK   ADO           
            ReadTable(ConnectionString2);
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private static void PrintNumLinq()
        {
            var temp = Enumerable.Range(0, 100);
            temp.Select(o => o);
            foreach (var a in temp)
                Console.WriteLine(a);

            var q = from e in temp select e;
            foreach (var ii in q)
                Console.WriteLine(ii);
        }
        private static void ActivateTrace()
        {
            //Fail  traice outPut 
            //DataConnection.TurnTraceSwitchOn();
            //Action<string, string, TraceLevel> act = new Action<string, string, TraceLevel>(s, s2) => { Debug.WriteLine(s, s2); };
            //DataConnection.WriteTraceLine = (msg ) => Debug.WriteLine(msg );
        }
    }

   

   

 
}
