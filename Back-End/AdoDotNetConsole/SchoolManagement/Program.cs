using SchoolManagement;

string[] parts = Console.ReadLine().Split(',');
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("name", parts[0]);
parameters.Add("address", parts[1]);
parameters.Add("cgpa", parts[2]);

string connectionString = "Server=ASUS-TUF\\SQLEXPRESS;Database=SchoolManagement;User Id=Practice_Admin;Password=12345678;Trust Server Certificate=True;";

// [NOTE : Non-Query Commands (No Data Returned)]
/* [NOTE :
    # Parameterized SQL :
      for using Parameterized SQL, SQL Injection can not be performed by Hackers.]
*/
string insertSql = "INSERT INTO Student(StudentName, StudentAddress, StudentCGPA) VALUES(@name, @address, @cgpa);";
string updateSql = "UPDATE Student SET StudentName='SLLL' WHERE StudentName='Slll';";
string deleteSql = "DELETE FROM Student WHERE StudentId=1;";

AdoDotNetUtility adoDotNetUtility = new AdoDotNetUtility(connectionString);

adoDotNetUtility.ExecuteSql(insertSql, parameters);
//adoDotNetUtility.ExecuteSql(updateSql);
//adoDotNetUtility.ExecuteSql(deleteSql);

int id = int.Parse(Console.ReadLine());
Dictionary<string, object> parameters2 = new Dictionary<string, object>();
parameters2.Add("id", id);

// [NOTE : Query Commands (That returns Data)]
string selectSql = "SELECT * FROM Student;";
string selectConditionIdSql = "SELECT * FROM Student WHERE StudentId = @id;";

// IList<Dictionary<string, object>> resultFromTable = adoDotNetUtility.GetData(selectSql, parameters);
IList<Dictionary<string, object>> resultFromTable = adoDotNetUtility.GetData(selectConditionIdSql, parameters2);

foreach (var row in resultFromTable)
{
    foreach(var col in row)
    {
        Console.Write(col.Value);
        Console.Write(", ");
    }

    Console.WriteLine();
}

Console.WriteLine("DONE...");

