using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SimpleCarModel
    {
        public int CarId { get; set; }
        public string MarkName { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public SimpleCarModel(int carId, string markName, string model, string description, double price, string image) {
            CarId = carId;
            MarkName = markName;
            Model = model;
            Description = description;
            Price = price;
            Image = image;
        }
    }
}
