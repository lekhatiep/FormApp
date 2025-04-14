using Business.Dto.UserDto;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<Account>> GetListAccount();
        Task<Account> CheckAccountInfo(string userName, string password);
        Task<List<Profile>> GetListStaffEmail(string roleName);
        Task<Account> GetStudentEmail();

        Task<Profile> GetProfileByUserName(string userName, bool defaultIfEmpty = false);
        Task<Profile> GetProfileByUserID(int userId);
        Task<Profile> GetProfileByEmail(string email, bool defaultIfEmpty = false);
        Task SendMailNewPassword(string email, string apiKey = "", string privateKey = "", string user = "",  string pw = "");
        Task CreateNewAccount(NewAccountDto newAccountDto);
        Task<Account> GetAccountByID(int accountID, bool defaultIfEmpty = false);
        Task UpdatePassword(int accountID, string newPassword);
        Task<List<Role>> GetListRole();
    }
}
