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
    public class ElectrocarsController: Controller
    {
        IAuthService authService;
        ICarsService carsService;
        IMarksService marksService;

        public ElectrocarsController(IAuthService authService, ICarsService carsService, IMarksService marksService)
        {
            this.authService = authService;
            this.carsService = carsService;
            this.marksService = marksService;
        }

        [HttpGet]
        public ActionResult Electrocars()
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper())
                    {
                        case "U":
                            return View("~/Views/Electrocars/ElectrocarsUser.cshtml");
                        case "A":
                            return View("~/Views/Electrocars/ElectrocarsAdmin.cshtml");
                    }
                }
            }
            return View("~/Views/Electrocars/Electrocars.cshtml");
        }

        [HttpGet("details/{id}")]
        public ActionResult ElectrocarDetails(int id) {
            CarModel car = carsService.GetCarById(id);

            if (car != null) {
                if (Request.Cookies.ContainsKey("session"))
                {
                    if (authService.IsAuthorized(Request.Cookies["session"]))
                    {
                        switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper())
                        {
                            case "U":
                                return View("~/Views/Electrocars/ElectrocarDetailsUser.cshtml", car);
                            case "A":
                                AdminCarModel adminCar = carsService.GetAdminCarById(id);
                                return View("~/Views/Electrocars/ElectrocarDetailsAdmin.cshtml", adminCar);
                        }
                    }
                }
                return View("~/Views/Electrocars/ElectrocarDetails.cshtml", car);
            }
            return StatusCode(404);
        }

        [HttpGet("{markName}")]
        public ActionResult ElectrocarByMark(string markName)
        {
            MarkModel mark = marksService.GetMark(markName);

            if(mark != null)
            {
                if (Request.Cookies.ContainsKey("session"))
                {
                    if (authService.IsAuthorized(Request.Cookies["session"]))
                    {
                        switch (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper())
                        {
                            case "U":
                                return View("~/Views/Electrocars/ElectrocarsByMarkUser.cshtml", mark);
                            case "A":
                                return View("~/Views/Electrocars/ElectrocarsByMarkAdmin.cshtml", mark);
                        }
                    }
                }
                return View("~/Views/Electrocars/ElectrocarsByMark.cshtml", mark);
            }
            return StatusCode(404);
        }

        [HttpGet("add")]
        public ActionResult ElectrocarAdd()
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.IsAuthorized(Request.Cookies["session"]))
                {
                    if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                    {
                        return View("~/Views/Electrocars/ElectrocarAdd.cshtml");
                    }
                }
            }
            return StatusCode(404);
        }

        [HttpGet("edit/{id}")]
        public ActionResult ElectrocarEdit(int id)
        {
            AdminCarModel car = carsService.GetAdminCarById(id);

            if (car != null)
            {
                if (Request.Cookies.ContainsKey("session"))
                {
                    if (authService.IsAuthorized(Request.Cookies["session"]))
                    {
                        if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                        {
                            return View("~/Views/Electrocars/ElectrocarEdit.cshtml", car);
                        }
                    }
                }
            }
            return StatusCode(404);
        }
    }
}
