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
        Task<IEnumerable<Account>> GetListStaffEmail();
        Task<Account> GetStudentEmail();
    }
}
