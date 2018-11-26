using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public SimpleUserModel User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public AnswerModel(int answerId, int questionId, SimpleUserModel user, string text, DateTime date) {
            AnswerId = answerId;
            QuestionId = questionId;
            User = user;
            Text = text;
            Date = date;
        }
    }
}
