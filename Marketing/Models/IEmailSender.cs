namespace Marketing.Models
{
    public interface IEmailSender
    {
        public Task< bool> SendEmailAsync(string senderEmail, string email, string subject, string confirmlink);
        void SendEmail(string email, string v1, string v2);
    }
}
