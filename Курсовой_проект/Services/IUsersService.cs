using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface IUsersService
    {
        List<UserModel> GetUsers();
        List<UserModel> GetUsersByAccessLevel(string accessLevel);
        void SetUserPermissions(int userId, string userPermisions);
    }
}
