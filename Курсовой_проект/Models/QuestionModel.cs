using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public SimpleUserModel User { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int AnswersCount { get; set; }

        public QuestionModel() {}
        public QuestionModel(int questionId, SimpleUserModel user, string header, string text, DateTime date) {
            QuestionId = questionId;
            User = user;
            Header = header;
            Text = text;
            Date = date;
            AnswersCount = 0;
        }
        public QuestionModel(int questionId, SimpleUserModel user, string header, string text, DateTime date, int answersCount)
        {
            QuestionId = questionId;
            User = user;
            Header = header;
            Text = text;
            Date = date;
            AnswersCount = answersCount;
        }
    }
}
