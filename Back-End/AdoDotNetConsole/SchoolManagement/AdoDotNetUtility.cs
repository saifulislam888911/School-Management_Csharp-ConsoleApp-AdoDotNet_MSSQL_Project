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

        public void ExecuteSql(string sql, Dictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sql, parameters);
            
            int effection = command.ExecuteNonQuery();

            int affectedRowCount = effection;
            Console.WriteLine("Row Efected : " + affectedRowCount + "; Operation Done");
        }

        public IList<Dictionary<string, object>> GetData(string sql, Dictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sql, parameters);

            using SqlDataReader reader = command.ExecuteReader();

            var items = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    /* [NOTE :
                        # Dictionary<key,  value> :
                            string colName = reader.GetName(i);   // কলামের নাম : (key)
                            object colValue = reader.GetValue(i); // রো অনুযায়ী ভ্যালু : (value)

                            // ডিবাগ লগ
                            Console.WriteLine($"Index: {i} | Name: {colName} | Value: {colValue}");

                            // ডিকশনারিতে যোগ
                            data[colName] = colValue;
                    */

                    data.Add(reader.GetName(i), reader.GetValue(i));
                }

                items.Add(data);
            }

            return items;
        }

        private SqlCommand CreateCommand(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            return command;
        }
    }
}
