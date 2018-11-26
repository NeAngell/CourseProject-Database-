using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;

namespace Pineapple.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        IAuthService authService;

        public IndexController(IAuthService authService) {
            this.authService = authService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.IsAuthorized(Request.Cookies["session"])) {
                    return Redirect("/electrocars");
                }
            }
            return View();
        }
    }
}