using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    public partial class Program
    {
        private static void ReadTable(string connectionString)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var sqlData = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM dbo.Products", connection))
                {
                    var dataTable = new System.Data.DataTable();
                    sqlData.Fill(dataTable);
                    foreach (System.Data.DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("Name: {0}, Description : {1}", row["Name"], row["Description"]);
                    }
                }
            }
        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {

                using (var command = new System.Data.SqlClient.SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    Console.WriteLine("Connection opened successfully");
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + "rows selected");
                }
            }
        }
    }
}
