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

        public async Task<List<Profile>> GetListStaffEmail(string roleName)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT profileID, FirstName, LastName, Email FROM Account a INNER JOIN Role r ON r.RoleID = a.RoleID 
                                INNER JOIN Profile p ON p.AccountID = A.AccountID
                                WHERE R.Name = @roleName ";
                var data = await connection.QueryAsync<Profile>(sql, new { roleName });

                if (data != null)
                {
                    return data.ToList();
                }
                else
                {
                    return new List<Profile>();
                }
            }
        }

        public Task<Profile> GetProfileByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> GetProfileByUserName(string userName, bool defaultIfEmpty = false)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = "SELECT * FROM Profile p INNER JOIN Account a ON a.AccountID = p.AccountID WHERE a.UserName = @userName ";
                var user = await connection.QueryFirstOrDefaultAsync<Profile>(sql, new { userName } );

                if (defaultIfEmpty)
                {
                    if (user == null)
                        return new Profile();
                }

                return user;
            }
        }

        public Task<Account> GetStudentEmail()
        {
            throw new NotImplementedException();
        }
    }
}
