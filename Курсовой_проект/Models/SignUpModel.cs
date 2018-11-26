using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SignUpModel
    {
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Password { get; set; }

        public SignUpModel(string username, string phone, string email, string firstName, string lastName, string patronymic, string password) {
            Username = username;
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Password = password;
        }
    }
}
