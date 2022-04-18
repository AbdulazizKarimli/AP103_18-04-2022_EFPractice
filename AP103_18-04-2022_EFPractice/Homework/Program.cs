using Homework.Utils;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Homework
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await AddEmployee("Yusif Nazarbayov");
            //await AddEmployeeAsync("Ahmad Jabrayilov");
            //await AddEmployeeAsync("Elgun Gocayev");
            //await AddEmployeeAsync("Nergiz Ganiyeva");
            //await GetEmployeeById(1);
            //await GetAllEmployeesAsync();
            //await DeleteEmployeeAsync(5);
            //await DeleteEmployeeAsync(6);
            await FilterByNameAsync("u");
        }
        public static async Task GetEmployeeByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Constants.connectionString))
            {
                connection.Open();

                string command = "SELECT Fullname FROM Employees WHERE Id=@id";
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    string fullName = (await sqlCommand.ExecuteScalarAsync()).ToString();
                    Console.WriteLine(fullName);
                }
            }
        }
        public static async Task AddEmployeeAsync(string fullname)
        {
            using (SqlConnection connection = new SqlConnection(Constants.connectionString))
            {
                connection.Open();

                string command = "INSERT INTO Employees VALUES(@fullname)";
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@fullname", fullname);
                    int result = await sqlCommand.ExecuteNonQueryAsync();
                    if (result > 0)
                        Console.WriteLine("employee created");
                    else
                        Console.WriteLine("error");
                }
            }
        }
        public static async Task GetAllEmployeesAsync()
        {
            using (SqlConnection connection = new SqlConnection(Constants.connectionString))
            {
                connection.Open();

                string command = "SELECT * FROM Employees";
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Fullname: {reader["Fullname"]}");
                        }
                    }
                }
            }
        }
        public static async Task DeleteEmployeeAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Constants.connectionString))
            {
                connection.Open();

                string command = "DELETE FROM Employees WHERE Id=@id";
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    int result = await sqlCommand.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        Console.WriteLine("employee deleted");
                    }
                    else
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }
        public static async Task FilterByNameAsync(string search)
        {
            using (SqlConnection connection = new SqlConnection(Constants.connectionString))
            {
                connection.Open();

                string command = "SELECT * FROM Employees WHERE Fullname LIKE '%' + @search + '%'";
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@search", search);
                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Fullname: {reader["Fullname"]}");
                        }
                    }
                }
            }
        }
    }
}
