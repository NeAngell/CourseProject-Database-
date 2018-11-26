using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Курсовой_проект.Models;
using Курсовой_проект.Services;

namespace Курсовой_проект.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IAuthService authService;

        public AuthController(IAuthService authService) {
            this.authService = authService;
        }

        [HttpPost("SignIn")]
        public SignInResponseModel SignIn([FromBody] SignInModel model) {
            SignInResponseModel responseModel = authService.SignIn(model);

            if (!responseModel.Error) {
                Response.Cookies.Append("session", responseModel.Session);
            }

            return responseModel;
        }

        [HttpPost("SignUp")]
        public string SignUp([FromBody] SignUpModel model) {
            string response = "";

            if (model.Username.Length < 3) {
                response = "Короткое имя";
            }
            else if (!Regex.IsMatch(model.Username, "^[A-Za-z0-9_]{1,}$"))
            {
                response = "Имя пользователя содержит недопустимые символы";
            }
            else if (!Regex.IsMatch(model.Email.ToLower(), "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$"))
            {
                response = "Неправильный формат e-Mail";
            }
            else if (model.Phone.Length < 5)
            {
                response = "Неправильный формат телефона";
            }
            else if (model.Password.Length < 8)
            {
                response = "Короткий пароль";
            }
            else {
                response = authService.SignUp(model);
            }

            return response;
        }

        [HttpGet("emailConfirmation/{activationString}")]
        public IActionResult EmailConfirmation(string activationString) {
            if (authService.EmailConfirmation(activationString))
            {
                return View("~/Views/SuccessfulConfirmation/SuccessfulConfirmation.cshtml");
            }
            else {
                return StatusCode(500);
            }
        }

        [HttpGet("LogOut")]
        public void LogOut() {
            if (Request.Cookies.ContainsKey("session")) {
                authService.LogOut(Request.Cookies["session"]);
                Response.Cookies.Delete("session");
            }
        }
    }
}
