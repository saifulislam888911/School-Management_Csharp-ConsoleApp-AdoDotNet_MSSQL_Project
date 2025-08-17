using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    internal class AdoDotNetUtility
    {
        private readonly string _connectionString;

        public AdoDotNetUtility(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteSql(string sql)
        {  
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            int effectRowCount = command.ExecuteNonQuery();
            Console.WriteLine("Row Efected : " + effectRowCount + "; Operation Done");
        }
    }
}
