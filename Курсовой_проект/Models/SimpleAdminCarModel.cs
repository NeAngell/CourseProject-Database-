using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SimpleAdminCarModel
    {
        public int CarId { get; set; }
        public string MarkName { get; set; }
        public string VendorCode { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public SimpleAdminCarModel(int carId, string markName, string vendorCode, string model, string description, double price)
        {
            CarId = carId;
            MarkName = markName;
            VendorCode = vendorCode;
            Model = model;
            Description = description;
            Price = price;
        }
    }
}
