using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SignInResponseModel
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public string Session { get; set; }

        public SignInResponseModel() {
            Error = true;
            ErrorMessage = "";
            Session = "";
        }
    }
}
