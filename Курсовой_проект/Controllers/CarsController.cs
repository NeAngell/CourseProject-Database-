using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Курсовой_проект.Services;
using Курсовой_проект.Models;
using Newtonsoft.Json;
using System.IO;

namespace Курсовой_проект.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        ICarsService carsService;
        IAuthService authService;
        IImagesService imagesService;

        public CarsController(ICarsService carsService, IAuthService authService, IImagesService imagesService)
        {
            this.carsService = carsService;
            this.authService = authService;
            this.imagesService = imagesService;
        }

        [HttpGet]
        public List<SimpleCarModel> GetCars() {
            return carsService.GetCars();
        }

        [HttpGet("{markName}")]
        public List<SimpleCarModel> GetCarsByMark(string markName) {
            return carsService.GetCarsByMark(markName);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCar([FromForm] RequestAdminCarModel model) {
            if (Request.Cookies.ContainsKey("session")) {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A") {
                    AdminCarModel carModel = JsonConvert.DeserializeObject<AdminCarModel>(model.Data);

                    if (model.ImageFile != null)
                    {
                        carModel.Image = await imagesService.SaveImage(model.ImageFile);
                    }

                    carsService.AddCar(carModel);
                }
            }

            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditCar([FromForm] RequestAdminCarModel model)
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    AdminCarModel carModel = JsonConvert.DeserializeObject<AdminCarModel>(model.Data);

                    if(model.ImageFile != null)
                    {
                        carModel.Image = await imagesService.SaveImage(model.ImageFile);
                    }

                    carsService.EditCar(carModel);
                }
            }

            return Ok();
        }

        [HttpPost("remove")]
        public void RemoveCar([FromBody] int id)
        {
            if (Request.Cookies.ContainsKey("session"))
            {
                if (authService.GetUserPermissions(Request.Cookies["session"]).ToUpper() == "A")
                {
                    carsService.RemoveCar(id);
                }
            }
        }

        [HttpPost("getByPrice")]
        public List<SimpleCarModel> GetByPrice([FromBody] int[] values) {
            List<SimpleCarModel> cars = new List<SimpleCarModel>();
            if (values.Length == 2) {
                cars = carsService.GetCarsByPrice(values[0], values[1]);
            }
            return cars;
        }

        [HttpPost("search")]
        public List<SimpleCarModel> Search([FromBody] string template)
        {
            return carsService.Search(template);
        }
    }
}
