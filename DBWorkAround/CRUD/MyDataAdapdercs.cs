using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBWorkAround
{
    public class MyDataAdapter
    {
        //string connectionStr;
        //string sqlExpressionStr;

        DataTable dataTable;
        public MyDataAdapter(string connectionString, string sqlCmd)
        {
            // connectionStr = connectionStr;
            //sqlExpressionStr = sqlCmd;
            this.dataTable = new DataTable();

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var sqlData = new System.Data.SqlClient.SqlDataAdapter(sqlCmd, connection))
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
