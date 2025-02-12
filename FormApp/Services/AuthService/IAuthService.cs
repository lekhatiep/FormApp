using FormApp.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Services.AuthService
{
    public interface IAuthService
    {
        AuthReponseDto GenerateToken(string userName, string email);
    }
}
