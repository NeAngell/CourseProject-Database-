using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Models;
using Курсовой_проект.Services;

namespace Курсовой_проект.Controllers
{
    [Route("[controller]")]
    public class ImagesController : Controller
    {
        IImagesService imagesService;

        public ImagesController(IImagesService imagesService) {
            this.imagesService = imagesService;
        }

        [HttpGet("{name}")]
        public IActionResult GetImage(string name) {

            FileStream fileStream = imagesService.GetImage(name);

            if (fileStream == null)
            {
                return NotFound();
            }

            return File(fileStream, "image/png");
        }
    }
}
