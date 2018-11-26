using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;

namespace Pineapple.Controllers
{
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        IAuthService authService;

        public ContactsController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper())
                    {
                        case "U":
                            return View("~/Views/Contacts/ContactsUser.cshtml");
                        case "A":
                            return View("~/Views/Contacts/ContactsAdmin.cshtml");
                    }
                }
            }
            return View("~/Views/Contacts/Contacts.cshtml");
        }
    }
}