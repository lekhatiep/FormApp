using Business.Dto.TicketDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.TickerService
{
    public interface ITicketService
    {
        Task<int> CreateNewTicket(CreateTicketDto createTicketDto);
        Task<List<TicketDto>> GetListTicket();
        Task<List<TicketDto>> GetListTicketByUserName(string userName);
        Task<TicketDataDto> GetDataByTicketID(int ID, bool defaultEmpty = false);
        Task UpdateStepTicket();
        Task UpdateStatus();
    }
}
