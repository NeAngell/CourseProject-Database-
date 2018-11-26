using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
public class MarksService : IMarksService
{
    public void AddMark(MarkModel mark)
    {
        using (DBConnection dbConnection = new DBConnection())
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO Marks(Mark_Name, Description) VALUES(@Mark_Name, @Description)", dbConnection.myConnection);
                command.Parameters.AddWithValue("@Mark_Name", mark.MarkName);
                command.Parameters.AddWithValue("@Description", mark.Description);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    public void EditMark(MarkModel mark)
    {
        using (DBConnection dbConnection = new DBConnection())
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Marks SET Mark_Name = @Mark_Name, Description = @Description WHERE Mark_Id = @Mark_Id", dbConnection.myConnection);
                command.Parameters.AddWithValue("@Mark_Id", mark.MarkId);
                command.Parameters.AddWithValue("@Mark_Name", mark.MarkName);
                command.Parameters.AddWithValue("@Description", mark.Description);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    public void RemoveMark(int id)
    {
        using (DBConnection dbConnection = new DBConnection())
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Marks WHERE Mark_Id = @Mark_Id", dbConnection.myConnection);
                command.Parameters.AddWithValue("@Mark_Id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    public List<MarkModel> GetMarks() {
        List<MarkModel> marks = new List<MarkModel>();

        using (DBConnection dbConnection = new DBConnection()) {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Marks", dbConnection.myConnection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marks.Add(new MarkModel(Convert.ToInt32(reader["Mark_Id"]), (string)reader["Mark_Name"], (string)reader["Description"]));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        return marks;
    }

    public MarkModel GetMark(string markName)
    {
        MarkModel marks = null;

        using (DBConnection dbConnection = new DBConnection())
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Marks WHERE Mark_Name = @Mark_Name", dbConnection.myConnection);
                command.Parameters.AddWithValue("Mark_Name", markName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        marks = new MarkModel(Convert.ToInt32(reader["Mark_Id"]), (string)reader["Mark_Name"], (string)reader["Description"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        return marks;
    }

    public MarkModel GetMarkById(int id)
    {
        MarkModel marks = null;

        using (DBConnection dbConnection = new DBConnection())
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Marks WHERE Mark_Id = @Mark_Id", dbConnection.myConnection);
                command.Parameters.AddWithValue("Mark_Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        marks = new MarkModel(Convert.ToInt32(reader["Mark_Id"]), (string)reader["Mark_Name"], (string)reader["Description"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        return marks;
    }
}
}
