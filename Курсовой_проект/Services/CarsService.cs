using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Курсовой_проект.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Курсовой_проект.Services
{
    public class CarsService : ICarsService
    {
        public void AddCar(AdminCarModel car)
        {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Products(Mark_Name, Vendor_Code, Model, Description, Price, Image) " +
                                                        "VALUES(@Mark_Name, @Vendor_Code, @Model, @Description, @Price, @Image) SELECT SCOPE_IDENTITY()", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Mark_Name", car.MarkName);
                    command.Parameters.AddWithValue("@Vendor_Code", car.VendorCode);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Description", car.Description);
                    command.Parameters.AddWithValue("@Price", car.Price);
                    command.Parameters.AddWithValue("Image", (object) car.Image ?? DBNull.Value);
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    command = new SqlCommand("INSERT INTO Products_Characteristics(Product_Id, Power, Gearbox, Acceleration, Max_Speed, Power_Reserve, Drive_Unit) " +
                                            "VALUES(@Product_Id, @Power, @Gearbox, @Acceleration, @Max_Speed, @Power_Reserve, @Drive_Unit)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Product_Id", id);
                    command.Parameters.AddWithValue("Power", car.Characteristic.Power);
                    command.Parameters.AddWithValue("Gearbox", car.Characteristic.Gearbox);
                    command.Parameters.AddWithValue("Acceleration", car.Characteristic.Acceleration);
                    command.Parameters.AddWithValue("Max_Speed", car.Characteristic.MaxSpeed);
                    command.Parameters.AddWithValue("Power_Reserve", car.Characteristic.PowerReserve);
                    command.Parameters.AddWithValue("Drive_Unit", car.Characteristic.DriveUnit);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void EditCar(AdminCarModel car)
        {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Products SET Mark_Name = @Mark_Name, Vendor_Code = @Vendor_Code, " +
                        "Model = @Model, Description = @Description, Price = @Price, Image = @Image WHERE Product_Id = @Product_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Product_Id", car.CarId);
                    command.Parameters.AddWithValue("Mark_Name", car.MarkName);
                    command.Parameters.AddWithValue("Vendor_Code", car.VendorCode);
                    command.Parameters.AddWithValue("Model", car.Model);
                    command.Parameters.AddWithValue("Description", car.Description);
                    command.Parameters.AddWithValue("Price", car.Price);
                    command.Parameters.AddWithValue("Image", (object) car.Image ?? DBNull.Value);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("UPDATE Products_Characteristics SET Power = @Power, Gearbox = @Gearbox, Acceleration = @Acceleration, " +
                        "Max_Speed = @Max_Speed, Power_Reserve = @Power_Reserve, Drive_Unit = @Drive_Unit WHERE Product_Id = @Product_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Product_Id", car.CarId);
                    command.Parameters.AddWithValue("Power", car.Characteristic.Power);
                    command.Parameters.AddWithValue("Gearbox", car.Characteristic.Gearbox);
                    command.Parameters.AddWithValue("Acceleration", car.Characteristic.Acceleration);
                    command.Parameters.AddWithValue("Max_Speed", car.Characteristic.MaxSpeed);
                    command.Parameters.AddWithValue("Power_Reserve", car.Characteristic.PowerReserve);
                    command.Parameters.AddWithValue("Drive_Unit", car.Characteristic.DriveUnit);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void RemoveCar(int id)
        {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Products WHERE Product_Id = @Product_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Product_Id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public CarModel GetCarById(int id)
        {
            CarModel car = null;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products INNER JOIN Products_Characteristics ON (Products_Characteristics.Product_Id = Products.Product_Id) " +
                                                        "WHERE Products.Product_Id = @Product_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Product_Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new CarModel(Convert.ToInt32(reader["Product_Id"]), new CharacteristicModel(Convert.ToInt32(reader["Characteristic_Id"]), (string)reader["Power"],
                                (string)reader["Gearbox"], Convert.ToDouble(reader["Acceleration"]), Convert.ToInt32(reader["Max_Speed"]), Convert.ToInt32(reader["Power_Reserve"]), (string)reader["Drive_Unit"]),
                                (string)reader["Mark_Name"], (string)reader["Model"], (string)reader["Description"], Convert.ToDouble(reader["Price"]),
                                reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return car;
        }

        public AdminCarModel GetAdminCarById(int id)
        {
            AdminCarModel car = null;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products INNER JOIN Products_Characteristics ON (Products_Characteristics.Product_Id = Products.Product_Id) " +
                                                        "WHERE Products.Product_Id = @Product_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Product_Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new AdminCarModel(Convert.ToInt32(reader["Product_Id"]), new CharacteristicModel(Convert.ToInt32(reader["Characteristic_Id"]), (string)reader["Power"],
                                (string)reader["Gearbox"], Convert.ToDouble(reader["Acceleration"]), Convert.ToInt32(reader["Max_Speed"]), Convert.ToInt32(reader["Power_Reserve"]), (string)reader["Drive_Unit"]),
                                (string)reader["Mark_Name"], (string)reader["Vendor_Code"], (string)reader["Model"], (string)reader["Description"], Convert.ToDouble(reader["Price"]),
                                reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return car;
        }

        public List<SimpleCarModel> GetCars()
        {
            List<SimpleCarModel> cars = new List<SimpleCarModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products ORDER BY Product_Id DESC", dbConnection.myConnection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"], 
                                (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return cars;
        }

        public List<SimpleCarModel> GetCarsByMark(string markName)
        {
            List<SimpleCarModel> cars = new List<SimpleCarModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Mark_Name = @Mark_Name ORDER BY Product_Id DESC", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Mark_Name", markName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"],
                                (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return cars;
        }

        public List<SimpleCarModel> GetCarsByPrice(int min, int max)
        {
            List<SimpleCarModel> cars = new List<SimpleCarModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Price >= @min AND Price <= @max ORDER BY Price", dbConnection.myConnection);
                    command.Parameters.AddWithValue("min", min);
                    command.Parameters.AddWithValue("max", max);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"],
                                (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return cars;
        }

        public List<SimpleCarModel> Search(string template)
        {
            List<SimpleCarModel> cars = new List<SimpleCarModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Products INNER JOIN Products_Characteristics ON (Products_Characteristics.Product_Id = Products.Product_Id) " +
                                                        "WHERE Mark_Name LIKE @template OR Vendor_Code LIKE @template OR Model LIKE @template " +
                                                        "OR Description LIKE @template OR Power LIKE @template OR Gearbox LIKE @template " +
                                                        "OR Drive_Unit LIKE @template ORDER BY Products.Product_Id DESC", dbConnection.myConnection);
                    command.Parameters.AddWithValue("template", "%" + template + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new SimpleCarModel(Convert.ToInt32(reader["Product_Id"]), (string)reader["Mark_Name"], (string)reader["Model"],
                                (string)reader["Description"], Convert.ToDouble(reader["Price"]), reader["Image"] != DBNull.Value ? (string)reader["Image"] : string.Empty));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return cars;
        }
    }
}
