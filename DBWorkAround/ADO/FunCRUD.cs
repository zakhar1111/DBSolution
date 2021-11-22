using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBWorkAround
{
    public partial class Program
    {
        private static void ReadTable(string connectionString)
        {
            using (var connection = new  SqlConnection(connectionString))
            {
                using (var sqlData = new  SqlDataAdapter("SELECT * FROM dbo.Products", connection))
                {
                    var dataTable = new  DataTable();
                    sqlData.Fill(dataTable);
                    foreach ( DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("Name: {0}, Description : {1}", row["Name"], row["Description"]);
                    }
                }
            }
        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (var connection = new  SqlConnection(connectionString))
            {

                using (var command = new  SqlCommand(queryString, connection))
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
