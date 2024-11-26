using Dapper;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IDapperDbConnection _dapperDbConnection;

        public UserService(IDapperDbConnection dapperDbConnection)
        {
            this._dapperDbConnection = dapperDbConnection;
        }
        public async Task<Account> CheckAccountInfo(string userName, string password)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT TOP 1 r.Name as RoleName, * FROM Account a INNER JOIN Role r on r.RoleID = a.RoleID  WHERE UserName = @userName AND Password = @password";
                var account = await connection.QueryFirstOrDefaultAsync<Account>(sql, new { userName, password });

                if(account != null)
                {
                    return account;                
                }
                else
                {
                    return new Account();
                }
            }
        }

        public async Task<IEnumerable<Account>> GetListAccount()
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = "SELECT * FROM Account";
                return await connection.QueryAsync<Account>(sql);
            }
        }

        public Task<IEnumerable<Account>> GetListStaffEmail()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetStudentEmail()
        {
            throw new NotImplementedException();
        }
    }
}
