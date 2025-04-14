using Business.Dto.UserDto;
using Business.Dtos.MailDto;
using Business.Ultilities;
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
        private readonly IMailjetSend _mailjet;
        private readonly ISendMail _sendMail;

        public UserService(IDapperDbConnection dapperDbConnection, IMailjetSend mailjet, ISendMail sendMail)
        {
            this._dapperDbConnection = dapperDbConnection;
            _mailjet = mailjet;
            _sendMail = sendMail;
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

        public async Task<Account> GetAccountByID(int accountID, bool defaultIfEmpty = false)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = "SELECT * FROM Account WHERE AccountID = @accountID ";
                var account = await connection.QueryFirstOrDefaultAsync<Account>(sql, new { accountID });

                if (defaultIfEmpty)
                {
                    if (account == null)
                        return new Account();
                }

                return account;
            }
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

        public async Task<Profile> GetProfileByEmail(string email, bool defaultIfEmpty = false)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = "SELECT TOP 1 * FROM Profile p  WHERE p.Email = @email ";
                var user = await connection.QueryFirstOrDefaultAsync<Profile>(sql, new { email });

                if (defaultIfEmpty)
                {
                    if (user == null)
                        return new Profile();
                }

                return user;
            }
        }
        public async Task SendMailNewPassword(string email, string apiKey, string privateKey, string user, string pw)
        {
            var profile = await GetProfileByEmail(email, true);

            if(profile.AccountID > 0)
            {
                await UpdatePassword(profile.AccountID, "123456");
                try
                {
                    string htmlBody = String.Format(@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='UTF-8'>
                            <title>Mật khẩu mới</title>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                    color: #333;
                                    background-color: #f5f5f5;
                                    padding: 20px;
                                }}
                                .email-container {{
                                    background-color: #ffffff;
                                    border-radius: 6px;
                                    padding: 30px;
                                    max-width: 600px;
                                    margin: auto;
                                    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
                                }}
                                .footer {{
                                    margin-top: 20px;
                                    font-size: 13px;
                                    color: #999;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class='email-container'>
                                <h2>Chào {0} {1},</h2>
                                <p>Đây là mật khẩu mới được cấp cho bạn:</p>
                                <p><strong style='font-size: 18px;'>123456</strong></p>
                                <p>Nếu bạn không yêu cầu thay đổi mật khẩu, vui lòng liên hệ quản trị viên.</p>
                                <p>Trân trọng,<br>Admin Form App</p>
                                <div class='footer'>
                                    Email này được gửi tự động, vui lòng không trả lời lại.
                                </div>
                            </div>
                        </body>
                        </html>
                    ", profile.FirstName, profile.LastName);




                    var mailDto = new MailDto()
                {
                    FromName = "Admin Form App",
                    NameRecipient = profile.FirstName + " " + profile.LastName,
                    Recipient = email,
                    //BodyHtml = $@"NEW PASSWORD: <h2>123456</h2>",
                    BodyHtml = htmlBody,
                    Subject = "[FORM_APP] Mật khẩu mới được cấp "
                };

                //await _mailjet.SendAsync(mailDto, apiKey, privateKey);
                await _sendMail.GmailSendAsync(mailDto, user, pw);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public async Task CreateNewAccount(NewAccountDto newAccountDto)
        {
            var roleID = (int)Enum.Role.student;

            if (newAccountDto.RoleID > 0)
            {
                roleID = newAccountDto.RoleID;
            }

            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"INSERT INTO [dbo].[Account]
                                    ([UserName]
                                    ,[Password]
                                    ,[RoleID])
                                VALUES
                                    (@username
                                    ,@password
                                    ,@roleID) 
                                SELECT AccountID  FROM Account WHERE AccountID = SCOPE_IDENTITY();";
                var accountID = await connection.QueryFirstOrDefaultAsync<int>(sql, new {
                    username = newAccountDto.UserName,
                    password = newAccountDto.Password,
                    roleId = roleID // Student default     
                });
                
                if(accountID > 0)
                {
                    string sqlInsert = @"INSERT INTO [dbo].[Profile]
                                               ([AccountID]
                                               ,[FirstName]
                                               ,[LastName]
                                               ,[Email])
                                         VALUES
                                               (@AccountID
                                               ,@FirstName
                                               ,@LastName
                                               ,@Email)";

                    await connection.ExecuteAsync(sqlInsert, new
                    {
                        AccountID = accountID,
                        FirstName = newAccountDto.FirstName,
                        LastName = newAccountDto.LastName,
                        Email = newAccountDto.Email    
                    });
                }
            }
        }

        public Task<Profile> GetProfileByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePassword(int accountID, string newPassword)
        {
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {
                    string sql = @"UPDATE Account SET Password = @Password
                                     WHERE AccountID = @AccountID";
                   var rs = await connection.ExecuteAsync(sql, new
                    {
                       Password = newPassword,
                       AccountID = accountID,
                    });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Role>> GetListRole()
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT * FROM ROLE ";
                var data = await connection.QueryAsync<Role>(sql);

                if (data != null)
                {
                    return data.ToList();
                }
                else
                {
                    return new List<Role>();
                }
            }
        }
    }
}
