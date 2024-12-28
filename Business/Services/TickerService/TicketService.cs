using Business.Dto.TicketDto;
using Business.Services.UserService;
using Dapper;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.TickerService
{
    public class TicketService : ITicketService
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        private readonly IUserService _userService;
        public TicketService(IDapperDbConnection dapperDbConnection, IUserService userService)
        {
            this._dapperDbConnection = dapperDbConnection;
            this._userService = userService;
        }

        public async Task<int> CreateNewTicket(CreateTicketDto createTicketDto)
        {
            var rs = 1;
            try
            {
                var user = await _userService.GetProfileByUserName(createTicketDto.UserID, true);

                if (user.ProfileID > 0)
                {
                    using (var connection = _dapperDbConnection.CreateConnection())
                    {
                        createTicketDto.DateCreated = DateTime.Now;
                        createTicketDto.ProfileID = user.ProfileID;
                        createTicketDto.ActiveStep = 1;

                        string sql = @"INSERT INTO RequestTicket (DateCreated, FormID, TicketData,ProfileId,ActiveStep) VALUES (@DateCreated, @FormID, @TicketData,@ProfileId,@ActiveStep)";
                        await connection.ExecuteAsync(sql, createTicketDto);

                    }
                }
            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<List<TicketDto>> GetListTicket()
        {
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"SELECT rt.TicketID, df.FormName , rt.DateCreated, rt.DateApproved, ISNULL(Status,'Waiting') Status, Note, a.UserName, ActiveStep
                                    FROM RequestTicket rt 
                                    JOIN DynamicForm df ON df.FormID = rt.FormID 
                                    JOIN Profile p on p.ProfileID = rt.ProfileID 
                                    JOIN Account a ON p.AccountID = a.AccountID
                                    ORDER BY rt.TicketID DESC";

                    var list = await connection.QueryAsync<TicketDto>(sql);

                    return list.Count() > 0 ? list.ToList() : new List<TicketDto>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TicketDto>> GetListTicketByUserName(string userName)
        {
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"SELECT rt.TicketID, df.FormName , rt.DateCreated, rt.DateApproved, ISNULL(Status,'Waiting') Status, Note, a.UserName, ActiveStep
                                    FROM RequestTicket rt 
                                    JOIN DynamicForm df ON df.FormID = rt.FormID 
                                    JOIN Profile p on p.ProfileID = rt.ProfileID 
                                    JOIN Account a ON p.AccountID = a.AccountID
                                    WHERE a.UserName = @userName
                                    ORDER BY rt.TicketID DESC";

                    var list = await connection.QueryAsync<TicketDto>(sql, new { userName });

                    return list.Count() > 0 ? list.ToList() : new List<TicketDto>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task UpdateStatus()
        {
            throw new NotImplementedException();
        }

        public Task UpdateStepTicket()
        {
            throw new NotImplementedException();
        }

        public async Task<TicketDataDto> GetDataByTicketID(int ID, bool defaultEmpty = false)
        {
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"SELECT TicketData, Note, Username, Status, ActiveStep, Email, a.*
                                    FROM RequestTicket rt 
                                    JOIN Profile p ON p.ProfileID = rt.ProfileID 
                                    JOIN Account a ON a.AccountID = p.AccountID  
                                    WHERE rt.TicketID = @ID";

                    var data = await connection.QueryFirstOrDefaultAsync<TicketDataDto>(sql, new { ID });

                    if (defaultEmpty)
                    {
                        return data == null ? new TicketDataDto() : data;
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
