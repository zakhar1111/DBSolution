using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    public partial class MySQLDataReader
    {


        public static void TestRunQuery(string strConnection, string strSqlCmd)
        {
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.RunQuery(strSqlCmd);
        }

        public static void TestRunQueryAsync(string strConnection, string strSqlCmd)
        {
            Console.WriteLine("MySQLDataReader.TestRunQueryAsync()");
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.RunQueryAsync(strSqlCmd).GetAwaiter();

        }

        public static void TestWrapperAsync(string strConnection, string sqlQuery)
        {
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.WrapperRunQueryAsync(sqlQuery).GetAwaiter();
        }

        public static void TestRunQueryWraper(string strConnection, string strSqlCmd)
        {
            Console.WriteLine("MySQLDataReader.TestRunQueryWraper()");
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.WrapperRunQueryAsync2(strSqlCmd).GetAwaiter();
        }
    }
}
