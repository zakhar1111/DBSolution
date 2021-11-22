using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBWorkAround
{
    public class MyDataAdapter
    {
        DataTable dataTable;
        public MyDataAdapter(string connectionString, string sqlCmd)
        {
            this.dataTable = new DataTable();

            using (var connection = new  SqlConnection(connectionString))
            {
                using (var sqlData = new  SqlDataAdapter(sqlCmd, connection))
                {
                    sqlData.Fill(this.dataTable);
                }
            }
        }

        public void ReadTable()
        {

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("ProductID: {0}, Name: {1}, Description : {2}", row["ProductID"], row["Name"], row["Description"]);
            }
        }

        public static void TestReadTable(string connectionString, string sqlCmd)
        {
            var myAdapter = new MyDataAdapter(connectionString, sqlCmd);
            myAdapter.ReadTable();
        }
    }
}
