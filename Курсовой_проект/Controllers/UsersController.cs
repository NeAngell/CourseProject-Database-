using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Models;
using Курсовой_проект.Services;

namespace Курсовой_проект.Controllers
{
    [Route("[controller]")]
    public class UsersController: Controller
    {
        IAuthService authService;

        public UsersController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult GetUsers() {
            List<UserModel> users = new List<UserModel>();

            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    return View("~/Views/Users/Users.cshtml");
                }
            }

            return StatusCode(404);
        }
    }
}
