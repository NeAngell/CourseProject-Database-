using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Date { get; set; }
        public string UserPermissions { get; set; }

        public UserModel() { }
        public UserModel(int userId, string username, string phone, string email, string firstName, string lastName, string patronymic, DateTime date, string userPermissions)
        {
            UserId = userId;
            Username = username;
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Date = date;
            UserPermissions = userPermissions;
        }
    }
}