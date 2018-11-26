using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public class AuthService : IAuthService
    {
        public SignInResponseModel SignIn(SignInModel model) {
            SignInResponseModel responseModel = new SignInResponseModel();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Activation WHERE Activation = 1 AND User_Id IN " +
                                                        "(SELECT User_Id FROM Users WHERE (Username = @Username OR Email = @Email) AND Password = @Password)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@Email", model.Username);
                    command.Parameters.AddWithValue("@Password", CreateMD5(model.Password));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            responseModel.Error = false;
                            responseModel.Session = CreateMD5(model.Username + DateTime.UtcNow);

                            command = new SqlCommand("INSERT INTO Sessions(User_Id, Session_Id, Date, Activity) VALUES(@User_Id, @Session_Id, @Date, 1)", dbConnection.myConnection);
                            command.Parameters.AddWithValue("@User_Id", reader["User_Id"]);
                            command.Parameters.AddWithValue("@Session_Id", responseModel.Session);
                            command.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                            reader.Close();

                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            responseModel.ErrorMessage = "Неправильный логин или пароль";
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    responseModel.ErrorMessage = e.Message;
                }
            }

            return responseModel;
        }

        public string SignUp(SignUpModel model)
        {
            string response = "";

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT count(*) FROM Users WHERE Username = @Username OR Phone = @Phone OR Email = @Email", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@Phone", model.Phone);
                    command.Parameters.AddWithValue("@Email", model.Email);

                    int count = (int)command.ExecuteScalar();

                    if (count == 0)
                    {
                        DateTime date = DateTime.UtcNow;
                        command = new SqlCommand("INSERT INTO Users(Username, Phone, Email, First_Name, Last_Name, Patronymic, Password, Register_Date) " +
                                                        "VALUES(@Username, @Phone, @Email, @First_Name, @Last_Name, @Patronymic, @Password, @Register_Date)", dbConnection.myConnection);
                        command.Parameters.AddWithValue("@Username", model.Username);
                        command.Parameters.AddWithValue("@Phone", model.Phone);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@First_Name", model.FirstName);
                        command.Parameters.AddWithValue("@Last_Name", model.LastName);
                        command.Parameters.AddWithValue("@Patronymic", model.Patronymic);
                        command.Parameters.AddWithValue("@Password", CreateMD5(model.Password));
                        command.Parameters.AddWithValue("@Register_Date", date);

                        command.ExecuteNonQuery();

                        command = new SqlCommand("SELECT User_Id, User_Permissions FROM Users WHERE Username = @Username", dbConnection.myConnection);
                        command.Parameters.AddWithValue("@Username", model.Username);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            AddActivation(Convert.ToInt32(reader["User_Id"]), model.Email);
                        }
                        reader.Close();
                    }
                    else
                    {
                        response = "Пользователь с таким именем, почтой или телефоном уже существует.";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    response = e.Message;
                }
            }

            return response;
        }

        private void AddActivation(int userId, string email) {
            using (DBConnection dbConnection = new DBConnection())
            {
                string activationString = CreateMD5(userId + DateTime.UtcNow.ToString());
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Activation(User_Id, Activation_String) VALUES(@User_Id, @Activation_String)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Id", userId);
                    command.Parameters.AddWithValue("@Activation_String", activationString);

                    command.ExecuteNonQuery();

                    SendEmail(email, activationString);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void SendEmail(string email, string activationString) {
            var fromAddress = new MailAddress("maxmozgovoj13696@gmail.com", "Electrocar");
            var toAddress = new MailAddress(email);
            string fromPassword = "*********";
            string subject = "Electrocar";
            string body = "Confirm your email for successful work in Electrocar.<br/>Please go to link: <a href='http://markiz9999-001-site1.atempurl.com/emailConfirmation/" + activationString +"'>confirmation link</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public bool EmailConfirmation(string activationString) {
            bool result = true;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    DateTime date = DateTime.UtcNow;
                    SqlCommand command = new SqlCommand("UPDATE Activation SET Activation = 1, Activation_Date = @Date WHERE Activation_String = @Activation_String AND Activation = 0", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Activation_String", activationString);

                    int count = command.ExecuteNonQuery();

                    if (count == 0) {
                        result = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    result = false;
                }
            }

            return result;
        }

        public void LogOut(string session) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Sessions SET Activity = 0 WHERE Session_Id = @Session", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Session", session);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public bool IsAuthorized(string session) {
            bool flag = false;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT count(*) FROM Sessions WHERE Session_Id = @Session AND Activity = 1", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Session", session);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        flag = true;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return flag;
        }

        public string GetUserPermissions(string session)
        {
            string accessLevel = "";
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT User_Permissions FROM Users WHERE User_Id IN (SELECT User_Id FROM Sessions WHERE Session_Id = @Session AND Activity = 1)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Session", session);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accessLevel = (string)reader["User_Permissions"];
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return accessLevel;
        }

        public UserModel GetUser(string session) {
            UserModel userModel = new UserModel();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE User_Id IN (SELECT User_Id FROM Sessions WHERE Session_Id = @Session AND Activity = 1)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Session", session);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userModel = new UserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["Phone"], (string)reader["Email"],
                                (string)reader["First_Name"], (string)reader["Last_Name"], (string)reader["Patronymic"], Convert.ToDateTime(reader["Register_Date"]), (string)reader["User_Permissions"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return userModel;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                string str = "";
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    str += hashBytes[i].ToString("X2");
                }
                return str;
            }
        }
    }
}
