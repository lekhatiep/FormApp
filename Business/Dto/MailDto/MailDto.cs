namespace Business.Dtos.MailDto
{
    public class MailDto
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Recipient { get; set; }
        public string BodyText { get; set; }
        public string BodyHtml { get; set; }
        public string Subject { get; set; }
    }
}
