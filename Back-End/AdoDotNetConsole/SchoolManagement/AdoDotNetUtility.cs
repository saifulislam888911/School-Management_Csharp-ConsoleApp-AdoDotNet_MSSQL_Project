using Microsoft.Data.SqlClient;


namespace SchoolManagement
{
    internal class AdoDotNetUtility
    {
        private readonly string _connectionString;

        public AdoDotNetUtility(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlCommand CreateCommand(string sql,
            Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            return command;
        }

        public void ExecuteSql(string sql, 
            Dictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sql, parameters);
            int effection = command.ExecuteNonQuery();

            int affectedRowCount = effection;
            Console.WriteLine("\nRow affected : " + affectedRowCount + "; Operation Done");
        }

        public IList<Dictionary<string, object>> GetData(string sql, 
            Dictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sql, parameters);
            using SqlDataReader reader = command.ExecuteReader();

            var items = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    data.Add(reader.GetName(i), reader.GetValue(i));
                }

                items.Add(data);
            }

            return items;
        }      
    }
}