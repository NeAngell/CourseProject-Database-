using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Курсовой_проект.Services;

namespace Курсовой_проект.DBMigration
{
    class TypeComparerRise : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class TypeComparerWaning : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name) * (-1);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*int choise = 0;

            Console.WriteLine("Write 1 (to apply all migrations) or 2 (revert one migration). ");

            do
            {
                try
                {
                    Console.Write("Write your choise: ");
                    choise = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Entered incorrect value.");
                }

            } while (choise != 1 && choise != 2);

            CheckOfMigrationsTableExist();

            if (choise == 1)
            {
                ApplyMigrations();
                Console.WriteLine("\nSuccess apply migrations");
            }
            else
            {
                RevertMigration();
                Console.WriteLine("\nSuccess revert migration");
            }

            Console.ReadLine();*/

            using (DBConnection dbConnection = new DBConnection())
            {
                int i = 0;
                while (i < 1000) {
                    try
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO Products(Mark_Name, Vendor_Code, Model, Description, Price) " +
                                                        "VALUES(@Mark_Name, @Vendor_Code, @Model, @Description, @Price)", dbConnection.myConnection);
                        command.Parameters.AddWithValue("@Mark_Name", "BMW");
                        command.Parameters.AddWithValue("@Vendor_Code", new Random().Next(0, 10000).ToString());
                        command.Parameters.AddWithValue("@Model", new Random().Next(0, 10000).ToString());
                        command.Parameters.AddWithValue("@Description", "");
                        command.Parameters.AddWithValue("@Price", new Random().Next(0, 100000));
                        command.ExecuteNonQuery();

                        Console.WriteLine("New line added");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    i++;
                }
            }

        }

        static public void CheckOfMigrationsTableExist()
        {
            using (DBConnection dbConnection = new DBConnection()) {
                try
                {
                    SqlDataReader reader = null;
                    SqlCommand command = new SqlCommand("SELECT OBJECT_ID (N'dbo.MigrationHistory', N'U')", dbConnection.myConnection);
                    reader = command.ExecuteReader();

                    reader.Read();

                    if (reader.GetValue(0) == DBNull.Value)
                    {
                        reader.Close();

                        string query = "CREATE TABLE dbo.MigrationHistory (Id int PRIMARY KEY IDENTITY(1,1), ClassNumber bigint NULL, DateApplied datetime NULL);";

                        command = new SqlCommand(query, dbConnection.myConnection);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static public void ApplyMigrations()
        {
            var type = typeof(IMigration);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)).ToList();

            IComparer<Type> comparer = new TypeComparerRise();
            types.Sort(comparer);

            Int64 res;
            foreach (var t in types)
            {
                if (Int64.TryParse(t.Name.Replace("_", ""), out res))
                {
                    var elem = (IMigration)Activator.CreateInstance(t);

                    Migrator.Apply(Convert.ToInt64(t.Name.Replace("_", "")), elem.ApplyQuery());
                }
            }
        }

        static public void RevertMigration()
        {
            var type = typeof(IMigration);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)).ToList();

            IComparer<Type> comparer = new TypeComparerWaning();
            types.Sort(comparer);

            Int64 res;
            foreach (var t in types)
            {
                if (Int64.TryParse(t.Name.Replace("_", ""), out res))
                {
                    var elem = (IMigration)Activator.CreateInstance(t);

                    if (Migrator.Revert(Convert.ToInt64(t.Name.Replace("_", "")), elem.RevertQuery()))
                    {
                        break;
                    }
                }
            }
        }
    }
}
