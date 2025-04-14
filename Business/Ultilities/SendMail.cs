using Business.Dtos.MailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;

namespace Business.Ultilities
{
    public class SendMail : ISendMail
    {
        public async Task GmailSendAsync(MailDto mailDto, string usergmail = "", string passwordApp = "")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailDto.FromName, usergmail));
            message.To.Add(new MailboxAddress(mailDto.NameRecipient, mailDto.Recipient));
            message.Subject = mailDto.Subject;

            message.Body = new TextPart("html")
            {
                Text = mailDto.BodyHtml
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(usergmail, passwordApp);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                Console.WriteLine("Email sent successfully using MailKit!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        public async Task MailTrapSendAsync(MailDto mailDto, string apiKey = "9c17aa3c7e73e521e0619c8ace44f322")
        { 
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://send.api.mailtrap.io/api/send"),
                Headers =
                {
                    { "Accept", "application/json" },
                    { "Api-Token", apiKey},
                },
                Content = new StringContent("{\"to\":[{\"email\":\"popcap10121@gmail.com\",\"name\":\"JohnDoe\"}],\"from\":{\"email\":\"sales@example.com\",\"name\":\"ExampleSalesTeam\"},,\"custom_variables\":{\"user_id\":\"45982\",\"batch_id\":\"PSJ-12\"},\"headers\":{\"X-Message-Source\":\"dev.mydomain.com\"},\"subject\":\"YourExampleOrderConfirmation\",\"text\":\"Congratulationsonyourorderno.1234\",}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
