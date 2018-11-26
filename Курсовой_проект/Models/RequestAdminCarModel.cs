using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Курсовой_проект.Models
{
    public class RequestAdminCarModel
    {
        public string Data { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
