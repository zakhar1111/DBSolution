//using LinqToDB.DataProvider.SqlServer;
using LinqToDB;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;


namespace DBWorkAround
{
    /*
    public static class Trace
    {
        private static object message;
        private static object displayName;

        static Trace()
        {
            DataConnection.TurnTraceSwitchOn();
            Action<string,string, TraceLevel> d = (message, displayName, (TraceLevel)4) => 
            {
                Console.WriteLine($"{message} {displayName}");
            };
            DataConnection.WriteTraceLine = d;
        }
    }
    */
    public partial class Program

    {

        //private static Trace st = new Trace();

        const string AdventureConnectionString =
            @"Data Source=.;Database=AdventureWorksDW2016;Integrated Security=SSPI";

        public const string StorTestConnectionString =
            @"Data Source=(localdb)\ProjectsV13;Database=StorTest;user=DBAuser;password=dbuserpasswordqwWA12!@;";

        const string StoreCarsConnectionString = @"Data Source=(localdb)\ProjectsV13;Database=StoreCars;user=DBAuser;password=dbuserpasswordqwWA12!@;";


        static void Main(string[] args)
        {

            //TestCRUDE_function();

            //OK
            //InnerJoin(StoreCarsConnectionString);  

            // TestCRUDE_DataConnection();

            //OK               
            //TestSelectADO_MySQLDataReader();

            //ok             PrintNumLinq();

            //Reproduced  Db.Test();

                         Db.Reproduce3371();

            //ConcatExpresion.ConcatExpresionTest();
           // FSharpTranspiler.TranstateTest();
           // RewritingExpression.TraverseExpressionTest();
           // RewritingExpression.RewriteExpressionTest();

            Console.Read();
        }

       

        private static void TestCRUDE_DataConnection()
        {
            //OK  MyDataAdapter.TestReadTable(StorTestConnectionString, "SELECT * FROM Products");

            StorTestConnection.WriteNewColumn();
            StorTestConnection.WriteColumn();
            StorTestConnection.WriteExistedColumnToEnd();
            StorTestConnection.WriteTo();
            StorTestConnection.WriteIdentityRaw();
            // StorTestConnection.InserOrUpdateRaw();
            //err StorTestConnection.InsertOrReplaceRaw(); 
            StorTestConnection.UpdateRaw();

            StorTestConnection.UpdateColumn();
            StorTestConnection.UpdateWhereColumn();
            StorTestConnection.UpdateWhereSetColumn();
            StorTestConnection.Read();
        }

        private static void TestCRUDE_function()
        {

            

            //OK             ReadTableWithLinq(StorTestConnectionString);


            //OK             ReadFromTable(StorTestConnectionString);


            CreateTable(StorTestConnectionString);
            InsertRaw(StorTestConnectionString);
            InsertModifiedRaw(StorTestConnectionString);
            InsertModifiedRaw2(StorTestConnectionString);
            InsertIntoRaw3(StorTestConnectionString);
            InsertWithIdentityRaw(StorTestConnectionString);

            //err InserOrUpdateRaw(StorTestConnectionString);
            //err InserOrReplaceRaw(StorTestConnectionString);

            UpdateRaw(StorTestConnectionString);
            UpdateColumn(StorTestConnectionString);
            UpdateWhereColumn(StorTestConnectionString);
            UpdateWhereSetColumn(StorTestConnectionString);
            //UpdateDelete(StorTestConnectionString);
            //BulkCopy(StorTestConnectionString);
        }




        private static void TestSelectADO_MySQLDataReader()
        {
            //OK 
            MySQLDataReader.TestRunQuery(StorTestConnectionString, "SELECT * FROM Products");
            //OK 
            MySQLDataReader.TestRunQueryAsync(StorTestConnectionString, "SELECT * FROM Products");

            Console.WriteLine("Run Wrapper");
            //MySQLDataReader.TestWrapperAsync(StorTestConnectionString, "SELECT * FROM Products");

            MySQLDataReader.TestRunQueryWraper(StorTestConnectionString, "SELECT * FROM Products");
        }
        private static void TestADO_fun()
        {
            //OK   ADO           
            ReadTable(StorTestConnectionString);
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private static void PrintNumLinq()
        {
            var temp = Enumerable.Range(0, 15);
            Func<int, bool> condition = o => (o % 5) == 0;
            temp =  temp.Where(condition);
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
 
 
