using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;
using Курсовой_проект.Models;

namespace Курсовой_проект.Controllers
{
    [Route("[controller]")]
    public class QuestionsController : Controller
    {
        IQuestionsService questionsService;
        IAuthService authService;

        public QuestionsController(IQuestionsService questionsService, IAuthService authService)
        {
            this.questionsService = questionsService;
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Question() {
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.IsAuthorized(Request.Cookies["session"])) {
                    switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper()) {
                        case "A":
                            return View("~/Views/Questions/QuestionsAdmin.cshtml");
                        case "U":
                            return View("~/Views/Questions/QuestionsUser.cshtml");
                    }
                }
            }
            return StatusCode(404);
        }

        [HttpGet("{id}")]
        public IActionResult QuestionDetails(int id) {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    QuestionModel model = questionsService.GetQuestion(id);
                    if (model != null) {
                        ViewData["permissions"] = authService.GetUserPermissions(Request.Cookies["session"]).ToUpper();
                        return View("~/Views/Questions/QuestionDetails.cshtml", model);
                    }
                }
            }
            return StatusCode(404);
        }
    }
}
