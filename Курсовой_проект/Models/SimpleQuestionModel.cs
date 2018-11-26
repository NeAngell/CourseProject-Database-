using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SimpleQuestionModel
    {
        public int QuestionId { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int AnswersCount { get; set; }

        public SimpleQuestionModel(int questionId, string header, string text, DateTime date, int answersCount)
        {
            QuestionId = questionId;
            Header = header;
            Text = text;
            Date = date;
            AnswersCount = answersCount;
        }
    }
}
