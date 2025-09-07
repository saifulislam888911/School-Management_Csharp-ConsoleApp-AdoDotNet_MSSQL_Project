using SchoolManagement;
using System.Threading.Channels;


string connectionString = "Server=ASUS-TUF\\SQLEXPRESS;Database=SchoolManagement;" +
    "User Id=Practice_Admin;Password=12345678;Trust Server Certificate=True;";

AdoDotNetUtility adoDotNetUtility = new AdoDotNetUtility(connectionString);


string insertSql = "INSERT INTO Student(StudentName, StudentAddress, StudentCGPA) VALUES(@name, @address, @cgpa);";
string updateSql = "UPDATE Student SET StudentName=@name, StudentAddress=@address, StudentCGPA=@cgpa WHERE StudentId=@id;";
string deleteSql = "DELETE FROM Student WHERE StudentId=@id;";
string selectSql = "SELECT * FROM Student;";
string selectByIdSql = "SELECT * FROM Student WHERE StudentId = @id;";


bool appRunning = true;
int option = 0;


Console.WriteLine("Welcome To \"School Management System\"");

while (appRunning)
{
    ShowOperationMenu();

    string? inputOperation = Console.ReadLine()?.Trim().ToLower();
    if (string.IsNullOrEmpty(inputOperation))
    {
        Console.WriteLine("No input detected. Please enter an option.");
        continue;
    }

    switch (inputOperation)
    {
        case "1":
        case "insert":
            option = 1;
            Console.WriteLine("Insert Operation:");
            try
            {
                var input = InputFromUser();
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("name", input.Value.name);
                parameters.Add("address", input.Value.address);
                parameters.Add("cgpa", input.Value.cgpa);

                adoDotNetUtility.ExecuteSql(insertSql, parameters);
                Console.WriteLine("Insert Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Insert Operation: " + ex.Message);
            }
            break;
            
        case "2":
        case "update":
            option = 2;
            Console.WriteLine("Update Operation:");
            try
            {
                int id = InputID();
                if (id <= 0) continue;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("id", id);

                if (!GetDataInfo(parameters)) continue;
                
                var input = InputFromUser();
                parameters.Add("name", input.Value.name);
                parameters.Add("address", input.Value.address);
                parameters.Add("cgpa", input.Value.cgpa);

                adoDotNetUtility.ExecuteSql(updateSql, parameters);
                Console.WriteLine($"Update Successful.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error-Update Operation: " + ex.Message);
            }
            break;

        case "3":
        case "delete":
            option = 3;
            Console.WriteLine("Delete Operation:");
            try
            {         
                int id = InputID();
                if (id <= 0) continue;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("id", id);

                if (!GetDataInfo(parameters)) continue;

                Console.Write($"\nDelete ID {id} ? (y / n) : ");
                string? confirmation = Console.ReadLine()?.Trim().ToLower();
                if((confirmation == "y") || (confirmation == "yes"))
                {
                    adoDotNetUtility.ExecuteSql(deleteSql, parameters);
                    Console.WriteLine($"Delete Successful.");
                }
                else
                {
                    Console.WriteLine("Canceled Delete Operation.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Delete Operation: " + ex.Message);
            }
            break;

        case "4":
        case "get data":
            option = 4;
            Console.WriteLine("Select Operation (Get Data):");
            try
            {
                int id = InputID();
                if (id <= 0) continue;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("id", id);

                if (!GetDataInfo(parameters)) continue;

                Console.WriteLine($"\nGet Data Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Get Data Operation: " + ex.Message);
            }
            break;
            
        case "5":
        case "display table":
            option = 5;
            Console.WriteLine("Display Full Table : Student");
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                if (!GetDataInfo(parameters)) continue;

                Console.WriteLine($"\nDisplayed Full Table Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Display Table Operation: " + ex.Message);
            }
            break;

        case "6":
        case "close":
            Console.Write($"Exit The Program ? (y / n): ");
            string? confirmationExit = Console.ReadLine()?.Trim().ToLower();
            if ((confirmationExit == "y") || (confirmationExit == "yes"))
            {
                appRunning = false;
            }
            else
            {
                Console.WriteLine("Continue...");
            }
            break; 
            
        default:
            Console.WriteLine("Invalid operation! Please select a valid operation.");
            break;
    }
}

void ShowOperationMenu()
{
    Console.WriteLine("\nOperations (Select The Operation Number) :\n" +
        "1. Insert\n" +
        "2. Update\n" +
        "3. Delete\n" +
        "4. Get Data\n" +
        "5. Display Table\n" +
        "6. Close\n");
}

int InputID()
{
    Console.Write("ID: ");

    if (!int.TryParse(Console.ReadLine()?.Trim(), out int inputId))
    {
        Console.WriteLine("Invalid ID!");

        return -1;
    }

    return inputId;
}

(string name, string address, decimal cgpa)? InputFromUser()
{
    if (option == 1)
    {
        Console.Write("Name,Address,CGPA: ");
    }
    else if (option == 2)
    {
        Console.Write("\nNew Data (Name,Address,CGPA): ");
    }

    string[]? parts = Console.ReadLine()?.Trim().Split(',');
    if (parts == null || parts.Length < 3)
    {
        throw new Exception("Invalid input format. Please follow the given format.");
    }

    string inputName = parts[0];
    if (string.IsNullOrEmpty(inputName))
    {
        throw new Exception("Name Field cannot be empty.");
    }

    string inputAddress = parts[1];

    if (!decimal.TryParse(parts[2], out decimal inputCgpa))
    {
        if (string.IsNullOrEmpty(parts[2]))
        { 
            inputCgpa = 0; 
        }
        else
        {
            throw new Exception("Invalid CGPA. Please enter a valid decimal number.");
        }
    }
    inputCgpa = Math.Round(inputCgpa, 2, MidpointRounding.AwayFromZero);
    
    return (inputName, inputAddress, inputCgpa);
}

bool GetDataInfo(Dictionary<string, object> parameters)
{
    string sql;
    if(option == 5)
    {
        sql = selectSql;
    }
    else
    {
        sql = selectByIdSql;
    }

    IList<Dictionary<string, object>> resultFromTable = adoDotNetUtility.GetData(sql, parameters);

    if(resultFromTable != null && resultFromTable.Count > 0)
    {
        Console.WriteLine();

        foreach (var row in resultFromTable)
        {
            foreach (var col in row)
            {
                Console.Write(col.Key);
                Console.Write(", ");
            }

            Console.WriteLine("\n");
            break;
        }

        foreach (var row in resultFromTable)
        {
            foreach (var col in row)
            {
                Console.Write(col.Value);
                Console.Write(", ");
            }

            Console.WriteLine();
        }

        return true;
    }
    else
    {
        Console.WriteLine("No Data Found.");

        return false;
    }
}

Console.WriteLine("\nDONE...");