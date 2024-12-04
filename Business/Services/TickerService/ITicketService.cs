using Business.Dto.TicketDto;
using System.Threading.Tasks;

namespace Business.Services.TickerService
{
    public interface ITicketService
    {
        Task<int> CreateNewTicket(CreateTicketDto createTicketDto);
    }
}
