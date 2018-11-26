using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public class UsersService : IUsersService
    {
        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Users ORDER BY User_Id", dbConnection.myConnection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new UserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["Phone"], (string)reader["Email"],
                                (string)reader["First_Name"], (string)reader["Last_Name"], (string)reader["Patronymic"], Convert.ToDateTime(reader["Register_Date"]), (string)reader["User_Permissions"]));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }

            return users;
        }

        public List<UserModel> GetUsersByAccessLevel(string userPermisions)
        {
            List<UserModel> users = new List<UserModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE User_Permisions = @User_Permisions ORDER BY User_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Permisions", userPermisions);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new UserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["Phone"], (string)reader["Email"],
                                (string)reader["First_Name"], (string)reader["Last_Name"], (string)reader["Patronymic"], Convert.ToDateTime(reader["Username"]), (string)reader["Access_Level"]));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }

            return users;
        }

        public void SetUserPermissions(int userId, string userPermisions) {

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Users SET User_Permissions = @User_Permissions WHERE User_Id = @User_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Permissions", userPermisions);
                    command.Parameters.AddWithValue("@User_Id", userId);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
