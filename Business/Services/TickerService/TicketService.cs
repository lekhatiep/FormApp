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

        public async Task<int> UpdateStatus(int ticketID)
        {
            var rs = 1;
            try
            {          
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"UPDATE RequestTicket SET Status = 'Done', 
                                    DateApproved = @DateApprove
                                    WHERE TicketID = @TicketID";
                    await connection.ExecuteAsync(sql, new { DateApprove = DateTime.Now, TicketID = ticketID});

                }
                
            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<int> UpdateStepTicket(UpdateTicketDto ticketDto)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {
                    var dateApprove = DateTime.Now;
                    string sql = @"UPDATE RequestTicket SET ActiveStep = ActiveStep + 1, DateApproved = @dateApprove WHERE TicketID = @TicketID";
                    await connection.ExecuteAsync(sql, new { TicketID = ticketDto.TicketID, dateApprove });
                }

            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
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

        public async Task<int> UpdatePreviousNote(UpdateTicketDto ticketDto)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"UPDATE RequestTicket SET Note = @Note WHERE TicketID = @TicketID";
                    await connection.ExecuteAsync(sql, new { Note = ticketDto.Note, TicketID = ticketDto.TicketID });
                }

            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<int> DisapproveTicket(UpdateTicketDto updateTicketDto)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"UPDATE RequestTicket SET Status = 'Update',ActiveStep = @ActiveStep, DateApproved = @DateReject  WHERE TicketID = @TicketID";
                    await connection.ExecuteAsync(sql, new {
                        TicketID = updateTicketDto.TicketID,
                        DateReject = DateTime.Now,
                        ActiveStep = -1
                    });
                }

            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<int> UpdateTicketData(UpdateTicketDto ticketDto)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"UPDATE RequestTicket
                                    SET TicketData = @TicketData,
                                    Status = 'Waiting'
                                    WHERE  TicketID = @TicketID";
                    await connection.ExecuteAsync(sql, new { TicketData = ticketDto.TicketData, TicketID = ticketDto.TicketID });
                }

            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<List<TicketDto>> GetListTicketByRole(string roleName)
        {
            try
            {
                var listActive = new List<int>();
                roleName = roleName.ToLower().Trim();
                switch (roleName)
                {
                    case "verifier":
                        listActive.Add(1);
                        break;
                    case "approver":
                        listActive.Add(2);
                        break;
                    case "executor":
                        listActive.Add(3);
                        break;
                    case "admin":
                        listActive.Add(1);
                        listActive.Add(2);
                        listActive.Add(3);
                        listActive.Add(4);
                        listActive.Add(-1);
                        break;
                    default:
                        listActive.Add(1);
                        break;
                }
                var activeStepParam = string.Join(",", listActive);
                using (var connection = _dapperDbConnection.CreateConnection())
                {

                    string sql = @"SELECT rt.TicketID, df.FormName , rt.DateCreated, rt.DateApproved, ISNULL(Status,'Waiting') Status, Note, a.UserName, ActiveStep
                                    FROM RequestTicket rt 
                                    JOIN DynamicForm df ON df.FormID = rt.FormID 
                                    JOIN Profile p on p.ProfileID = rt.ProfileID 
                                    JOIN Account a ON p.AccountID = a.AccountID
                                    WHERE ActiveStep IN @ActiveStep
                                    ORDER BY rt.TicketID DESC";

                    var list = await connection.QueryAsync<TicketDto>(sql, new { ActiveStep = listActive });

                    return list.Count() > 0 ? list.ToList() : new List<TicketDto>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
