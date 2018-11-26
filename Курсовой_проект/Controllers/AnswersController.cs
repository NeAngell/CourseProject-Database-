using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;
using Курсовой_проект.Models;

namespace Курсовой_проект.Controllers
{
    [Route("api/[controller]")]
    public class AnswersController : Controller
    {
        IQuestionsService questionsService;
        IAuthService authService;

        public AnswersController(IQuestionsService questionsService, IAuthService authService) {
            this.questionsService = questionsService;
            this.authService = authService;
        }

        [HttpGet("{id}")]
        public List<AnswerModel> GetAnswers(int id)
        {
            List<AnswerModel> answers = new List<AnswerModel>();
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    answers = questionsService.GetAnswers(id);
                }
            }
            return answers;
        }

        [HttpPost("add")]
        public void AddAnswer([FromBody] AnswerModel model) {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    model.User = new SimpleUserModel() { UserId = authService.GetUser(Request.Cookies["session"]).UserId };
                    questionsService.AddAnswer(model);
                }
            }
        }

    }
}
