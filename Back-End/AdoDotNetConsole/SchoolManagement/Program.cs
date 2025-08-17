using SchoolManagement;

string connectionString = "Server=ASUS-TUF\\SQLEXPRESS;Database=SchoolManagement;User Id=Practice_Admin;Password=12345678;Trust Server Certificate=True;";

string insertSql = "INSERT INTO Student(StudentName, StudentAddress, StudentCGPA) VALUES('Skkk', 'Dhaka', 3.50);";
string updateSql = "UPDATE Student SET StudentName='SLLL' WHERE StudentName='Slll';";
string deleteSql = "DELETE FROM Student WHERE StudentId=1;";

AdoDotNetUtility adoDotNetUtility = new AdoDotNetUtility(connectionString);
//adoDotNetUtility.ExecuteSql(insertSql);
//adoDotNetUtility.ExecuteSql(updateSql);
adoDotNetUtility.ExecuteSql(deleteSql);