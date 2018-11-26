using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;

namespace Курсовой_проект.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        IAuthService authService;

        public OrdersController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Orders() {
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.IsAuthorized(Request.Cookies["session"])) {
                    switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper()) {
                        case "U":
                            return View("~/Views/Orders/OrdersUser.cshtml");
                        case "A":
                            return View("~/Views/Orders/OrdersAdmin.cshtml");
                    }
                }
            }
            return StatusCode(404);
        }
    }
}
