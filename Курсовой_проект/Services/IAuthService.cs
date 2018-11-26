using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Курсовой_проект.Models;

namespace Курсовой_проект.Services
{
    public interface IAuthService
    {
        SignInResponseModel SignIn(SignInModel model);
        void LogOut(string session);
        bool IsAuthorized(string session);
        string GetUserPermissions(string session);
        UserModel GetUser(string session);

        string SignUp(SignUpModel model);
        bool EmailConfirmation(string activationString);
    }
}
