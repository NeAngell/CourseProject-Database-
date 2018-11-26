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
    public class MarksController : Controller
    {
        IMarksService marksService;
        IAuthService authService;

        public MarksController(IMarksService marksService, IAuthService authService) {
            this.marksService = marksService;
            this.authService = authService;
        }

        [HttpGet]
        public List<MarkModel> GetMarks() {
            return marksService.GetMarks();
        }

        [HttpGet("{id}")]
        public MarkModel GetMarkById(int id) {
            return marksService.GetMarkById(id);
        }

        [HttpPost("add")]
        public void AddMark([FromBody] MarkModel model) {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    marksService.AddMark(model);
                }
            }
        }

        [HttpPost("edit")]
        public void EditMark([FromBody] MarkModel model)
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    marksService.EditMark(model);
                }
            }
        }

        [HttpPost("remove")]
        public void RemoveMark([FromBody] int id)
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    marksService.RemoveMark(id);
                }
            }
        }
    }
}
