using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Models;
using Курсовой_проект.Services;

namespace Курсовой_проект.Controllers
{
    [Route("api/[controller]")]
    public class ApiUsersController: Controller
    {
        IUsersService usersService;
        IAuthService authService;

        public ApiUsersController(IUsersService usersService, IAuthService authService)
        {
            this.usersService = usersService;
            this.authService = authService;
        }

        [HttpGet]
        public List<UserModel> GetUsers() {
            List<UserModel> users = new List<UserModel>();

            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    users = usersService.GetUsers();
                }
            }

            return users;
        }

        [HttpPost("setAdmin")]
        public void SetAdmin([FromBody] int id) {
            usersService.SetUserPermissions(id, "A");
        }

        [HttpPost("setUser")]
        public void SetUser([FromBody] int id)
        {
            usersService.SetUserPermissions(id, "U");
        }
    }
}
