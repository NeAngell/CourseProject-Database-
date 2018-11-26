using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public class QuestionsService : IQuestionsService
    {
        public void AddQuestion(QuestionModel model) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Questions(User_Id, Header, Text, Date) " +
                                                        "VALUES(@User_Id, @Header, @Text, @Date)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@User_Id", model.User.UserId);
                    command.Parameters.AddWithValue("@Header", model.Header);
                    command.Parameters.AddWithValue("@Text", model.Text);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void DeleteQuestion(int id) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELERE FROM Questions WHERE Question_Id = @Question_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Question_Id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public List<QuestionModel> GetQuestions() {
            List<QuestionModel> questions = new List<QuestionModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Questions INNER JOIN Users ON (Questions.User_Id = Users.User_Id) ORDER BY Question_Id DESC", dbConnection.myConnection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new QuestionModel(
                                Convert.ToInt32(reader["Question_Id"]), 
                                new SimpleUserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["User_Permissions"]), 
                                (string)reader["Header"], 
                                (string)reader["Text"],
                                Convert.ToDateTime(reader["Date"]), 
                                GetCountOfAnswersQuestions(Convert.ToInt32(reader["Question_Id"]))));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return questions;
        }
        public QuestionModel GetQuestion(int id)
        {
            QuestionModel questions = null;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Questions INNER JOIN Users ON (Questions.User_Id = Users.User_Id) WHERE Question_Id = @Question_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Question_Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions = new QuestionModel(Convert.ToInt32(reader["Question_Id"]), 
                                new SimpleUserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["User_Permissions"]), 
                                (string)reader["Header"], (string)reader["Text"], Convert.ToDateTime(reader["Date"]));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return questions;
        }

        public void AddAnswer(AnswerModel model) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Answers(Question_Id, User_Id, Text, Date) " +
                                                        "VALUES(@Question_Id, @User_Id, @Text, @Date)", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Question_Id", model.QuestionId);
                    command.Parameters.AddWithValue("@User_Id", model.User.UserId);
                    command.Parameters.AddWithValue("@Text", model.Text);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void DeleteAnswer(int id) {
            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELERE FROM Answers WHERE Answer_Id = @Answer_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("@Answer_Id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public List<AnswerModel> GetAnswers(int questionId) {
            List<AnswerModel> answers = new List<AnswerModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Answers INNER JOIN Users ON (Answers.User_Id = Users.User_Id) WHERE Question_Id = @Question_Id ORDER BY Answer_Id DESC", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Question_Id", questionId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            answers.Add(new AnswerModel(Convert.ToInt32(reader["Answer_Id"]), Convert.ToInt32(reader["Question_Id"]), 
                                new SimpleUserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["User_Permissions"]),
                                (string)reader["Text"], Convert.ToDateTime(reader["Date"])));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return answers;
        }

        public int GetCountOfAnswersQuestions(int questionId) {
            int count = 0;

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT count(*) FROM Answers WHERE Question_Id = @Question_Id", dbConnection.myConnection);
                    command.Parameters.AddWithValue("Question_Id", questionId);

                    count = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return count;
        }

        public List<QuestionModel> Search(string template)
        {
            List<QuestionModel> questions = new List<QuestionModel>();

            using (DBConnection dbConnection = new DBConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Questions INNER JOIN Users ON (Questions.User_Id = Users.User_Id) " +
                        "WHERE Header LIKE @template OR Text LIKE @template OR Username LIKE @template ORDER BY Question_Id DESC", dbConnection.myConnection);
                    command.Parameters.AddWithValue("template", "%" + template + "%");
           
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new QuestionModel(
                                Convert.ToInt32(reader["Question_Id"]),
                                new SimpleUserModel(Convert.ToInt32(reader["User_Id"]), (string)reader["Username"], (string)reader["User_Permissions"]),
                                (string)reader["Header"],
                                (string)reader["Text"],
                                Convert.ToDateTime(reader["Date"]),
                                GetCountOfAnswersQuestions(Convert.ToInt32(reader["Question_Id"]))));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return questions;
        }
    }
}
