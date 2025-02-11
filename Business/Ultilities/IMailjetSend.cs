using Business.Dtos.MailDto;
using System.Threading.Tasks;

namespace Business.Ultilities
{
    public interface IMailjetSend
    {
        Task SendAsync(MailDto mailDto, string apiKey = "", string apiPrivateKey = "");
    }
}
