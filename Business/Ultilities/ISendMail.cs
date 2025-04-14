using Business.Dtos.MailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ultilities
{
    public interface ISendMail
    {
        Task MailTrapSendAsync(MailDto mailDto, string apiKey = "");
        Task GmailSendAsync(MailDto mailDto, string user = "", string pw = "");
    }
}
