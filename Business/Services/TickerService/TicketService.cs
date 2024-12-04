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

    }
}
