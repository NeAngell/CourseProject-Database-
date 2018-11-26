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
    [Route("[controller]")]
    public class EmailConfirmationController : Controller
    {
        IAuthService authService;

        public EmailConfirmationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("{activationString}")]
        public IActionResult EmailConfirmation(string activationString)
        {
            if (authService.EmailConfirmation(activationString))
            {
                return View("~/Views/SuccessfulConfirmation/SuccessfulConfirmation.cshtml");
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}