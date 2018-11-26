using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Курсовой_проект.Models
{
    public class AdminCarModel
    {
        public int CarId { get; set; }
        public CharacteristicModel Characteristic { get; set; }
        public string MarkName { get; set; }
        public string VendorCode { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public AdminCarModel(int carId, CharacteristicModel characteristic, string markName, string vendorCode, string model, string description, double price, string image)
        {
            CarId = carId;
            Characteristic = characteristic;
            MarkName = markName;
            VendorCode = vendorCode;
            Model = model;
            Description = description;
            Price = price;
            Image = image;
        }
    }
}
