using Business.Dtos.MailDto;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Business.Ultilities
{
    public class MailjetSend : IMailjetSend
    {
        public IConfiguration _configuration { get; }
        public MailjetSend(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(MailDto mailDto, string apiKey="", string apiPrivateKey = "")
        {
           // var apiKey = _configuration.GetValue<string>("Mailjet_API_KEY");
            //var apiPrivateKey = _configuration.GetValue<string>("Mailjet_PRIVATE_KEY");

            MailjetClient client = new MailjetClient(apiKey, apiPrivateKey){};
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "popcap112024@gmail.com")
            .Property(Send.FromName, mailDto.FromName)
            .Property(Send.Subject, mailDto.Subject)
            .Property(Send.TextPart, mailDto.BodyText)
            .Property(Send.HtmlPart, mailDto.BodyHtml)
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", mailDto.Recipient}
                 }
                });

            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
