using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface ICarsService
    {
        void AddCar(AdminCarModel car);
        void EditCar(AdminCarModel car);
        void RemoveCar(int id);
        CarModel GetCarById(int id);
        AdminCarModel GetAdminCarById(int id);
        List<SimpleCarModel> GetCars();
        List<SimpleCarModel> GetCarsByMark(string markName);
        List<SimpleCarModel> GetCarsByPrice(int min, int max);
        List<SimpleCarModel> Search(string template);
    }
}
