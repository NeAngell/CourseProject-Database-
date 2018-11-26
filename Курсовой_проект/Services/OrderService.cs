using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Курсовой_проект.Models;
namespace Курсовой_проект.Services
{
    public class OrderService : IOrderService
    {
        public void AddOrder(int userId, int productId) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Orders (User_Id, Product_Id, Date) VALUES(@User_Id, @Product_Id, @Date)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Id", userId);
                    command.Parameters.AddWithValue("@Product_Id", productId);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void RemoveOrder(int id) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE Order_Id = @Order_Id AND Delivery_Status = 0 AND Payment_State = 0", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Order_Id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void EditOrder(OrderModel model) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Orders SET Delivery_Status = @Delivery_Status, Payment_State = @Payment_State WHERE Order_Id = @Order_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Delivery_Status", model.DeliveryStatus);
                    command.Parameters.AddWithValue("@Payment_State", model.PaymentState);
                    command.Parameters.AddWithValue("@Order_Id", model.OrderId);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public List<OrderModel> GetOrders() {
            List<OrderModel> orders = new List<OrderModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Orders INNER JOIN Users ON (Orders.User_Id = Users.User_Id) " +
                                                        "INNER JOIN Products ON (Orders.Product_Id = Products.Product_Id) ORDER BY Order_Id DESC", dbConnection.myConnection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new OrderModel(Convert.ToInt32(reader["Order_Id"]), new UserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["Phone"], (string)reader["Email"],
                                (string)reader["First_Name"], (string)reader["Last_Name"], (string)reader["Patronymic"], Convert.ToDateTime(reader["Register_Date"]), (string)reader["User_Permissions"]),
                                new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"], (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty), 
                                Convert.ToDateTime(reader["Date"]), Convert.ToBoolean(reader["Delivery_Status"]), Convert.ToBoolean(reader["Payment_State"])));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return orders;
        }

        public List<OrderModel> GetOrdersByUser(int id) {
            List<OrderModel> orders = new List<OrderModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Orders INNER JOIN Users ON (Orders.User_Id = Users.User_Id) " +
                                                        "INNER JOIN Products ON (Orders.Product_Id = Products.Product_Id) WHERE Orders.User_Id = @User_Id ORDER BY Order_Id DESC", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new OrderModel(Convert.ToInt32(reader["Order_Id"]), new UserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["Phone"], (string)reader["Email"],
                                (string)reader["First_Name"], (string)reader["Last_Name"], (string)reader["Patronymic"], Convert.ToDateTime(reader["Register_Date"]), (string)reader["User_Permissions"]),
                                new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"], (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty),
                                Convert.ToDateTime(reader["Date"]), Convert.ToBoolean(reader["Delivery_Status"]), Convert.ToBoolean(reader["Payment_State"])));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return orders;
        }
    }
}
