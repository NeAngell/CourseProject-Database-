using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class SimpleUserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPermissions { get; set; }

        public SimpleUserModel() { }
        public SimpleUserModel(int userId, string username, string userPermissions)
        {
            UserId = userId;
            Username = username;
            UserPermissions = userPermissions;
        }
    }
}
