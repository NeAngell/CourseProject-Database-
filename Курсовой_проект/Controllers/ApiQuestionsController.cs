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
    public class ApiQuestionsController: Controller
    {
        IQuestionsService questionsService;
        IAuthService authService;

        public ApiQuestionsController(IQuestionsService questionsService, IAuthService authService)
        {
            this.questionsService = questionsService;
            this.authService = authService;
        }

        [HttpGet]
        public List<QuestionModel> Question() {
            List<QuestionModel> questions = new List<QuestionModel>();
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.IsAuthorized(Request.Cookies["session"])) {
                    questions = questionsService.GetQuestions();
                }
            }
            return questions;
        }

        [HttpPost("add")]
        public void AddQuestion([FromBody] QuestionModel model)
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "U") {
                        model.User = new SimpleUserModel() { UserId = authService.GetUser(Request.Cookies["session"]).UserId };
                        questionsService.AddQuestion(model);
                    }
                }
            }
        }

        [HttpPost("search")]
        public List<QuestionModel> Search([FromBody] string template)
        {
            return questionsService.Search(template);
        }
    }
}
