using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBWorkAround
{
    public class MySQLDataReader
    {
        string connection;
        public MySQLDataReader(string connectionString)
        {
            connection = connectionString;
        }

        public void SelectComand(string cmdExpression)
        {
            using (var connectionObj = new System.Data.SqlClient.SqlConnection(this.connection))
            {
                connectionObj.Open();
                var cmd = new System.Data.SqlClient.SqlCommand(cmdExpression, connectionObj);

                using (System.Data.SqlClient.SqlDataReader readerObj = cmd.ExecuteReader())
                {
                    if (readerObj.HasRows)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", readerObj.GetName(0), readerObj.GetName(1), readerObj.GetName(2));
                    }
                    while (readerObj.Read())
                    {
                        object id = readerObj["ProductID"];
                        object name = readerObj["Name"];
                        object description = readerObj["Description"];
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, description);
                    }
                }
            }
        }

        public async Task SelectCommandAsync(string cmdExpression)
        {
            using (var connectionObj = new System.Data.SqlClient.SqlConnection(this.connection))
            {
                await connectionObj.OpenAsync();
                var cmd = new System.Data.SqlClient.SqlCommand(cmdExpression, connectionObj);

                using (System.Data.SqlClient.SqlDataReader readerObj = await cmd.ExecuteReaderAsync())
                {
                    if (readerObj.HasRows)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", readerObj.GetName(0), readerObj.GetName(1), readerObj.GetName(2));
                    }
                    while (await readerObj.ReadAsync())
                    {
                        object id = readerObj["ProductID"];
                        object name = readerObj["Name"];
                        object description = readerObj["Description"];
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, description);
                    }
                }
            }
        }

        public static void TestSqlConnection(string strConnection, string strSqlCmd)
        {
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.SelectComand(strSqlCmd);
        }

        public static void TestSqlConnectionAsync(string strConnection, string strSqlCmd)
        {
            var sqlConnection = new MySQLDataReader(strConnection);
            sqlConnection.SelectCommandAsync(strSqlCmd).GetAwaiter();
        }

    }
}
