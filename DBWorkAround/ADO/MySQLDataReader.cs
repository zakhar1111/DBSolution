using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DBWorkAround
{
    public partial class MySQLDataReader
    {
        readonly string connectionString;
        public MySQLDataReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void RunQuery(string SqlQuery)
        {
            using (var connection = new  SqlConnection(this.connectionString))
            {
                connection.Open();
                var cmd = new  SqlCommand(SqlQuery, connection);

                using ( SqlDataReader readerObj = cmd.ExecuteReader())
                {
                    if (readerObj.HasRows)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", readerObj.GetName(0), readerObj.GetName(1), readerObj.GetName(2));
                    }
                    while (readerObj.Read())
                    {
                        int id = (int) readerObj["ProductID"];
                        string name = (string) readerObj["Name"];
                        string description = (string) readerObj["Description"];
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, description);
                    }
                }
            }
        }

        public async Task RunQueryAsync(string cmdExpression)
        {
            using (var connectionObj = new  SqlConnection(this.connectionString))
            {
                await connectionObj.OpenAsync();
                var cmd = new  SqlCommand(cmdExpression, connectionObj);

                using ( SqlDataReader readerObj = await cmd.ExecuteReaderAsync())
                {
                    if (readerObj.HasRows)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", readerObj.GetName(0), readerObj.GetName(1), readerObj.GetName(2));
                    }
                    while (await readerObj.ReadAsync())
                    {
                        int id = (int) readerObj["ProductID"];
                        string name = (string) readerObj["Name"];
                        string description = (string) readerObj["Description"];
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, description);
                    }
                }
            }
        }

        public async Task WrapperRunQueryAsync(string sqlQuery)
        {
            //Action<string> RunQueryDelegate =
            await Task.Run(() => MySQLDataReader.TestRunQuery(Program.StorTestConnectionString, sqlQuery));
        }

        public async Task WrapperRunQueryAsync2(string sqlQuery)
        {
            Action<string> RunQueryDelegate = (query) => { RunQuery(query); };
             
            //await Task.Run(RunQueryDelegate);
            Action RunQueryEmptyArgsDelegate = () => { RunQuery(sqlQuery); };
            await Task.Run(RunQueryEmptyArgsDelegate);
        }



    }
}
