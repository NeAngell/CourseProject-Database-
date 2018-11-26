using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface IQuestionsService
    {
        void AddQuestion(QuestionModel model);
        void DeleteQuestion(int id);
        QuestionModel GetQuestion(int id);
        List<QuestionModel> GetQuestions();

        void AddAnswer(AnswerModel model);
        void DeleteAnswer(int id);
        List<AnswerModel> GetAnswers(int questionId);

        int GetCountOfAnswersQuestions(int questionId);

        List<QuestionModel> Search(string template);
    }
}
