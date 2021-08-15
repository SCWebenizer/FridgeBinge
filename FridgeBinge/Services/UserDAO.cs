using FridgeBinge.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBinge.Services
{
    public class UserDAO
    {
        private string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TEst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.MortiiMatii WHERE username = @username AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return success;
        }

        // Read all
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> foundUsers = new();

            string sqlStatement = "SELECT * FROM dbo.MortiiMatii";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundUsers.Add(new UserModel { Id = (int)reader[0], Username = (string)reader[1], Password = (string)reader[2] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundUsers;
        }
    }
}
