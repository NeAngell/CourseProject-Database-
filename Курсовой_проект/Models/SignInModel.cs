using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SignInModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SignInModel(string username, string password) {
            Username = username;
            Password = password;
        }
    }
}
