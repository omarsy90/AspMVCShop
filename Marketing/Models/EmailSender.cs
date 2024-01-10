using System.Net.Mail;
using System.Net;

namespace Marketing.Models
{
    public class EmailSender : IEmailSender
    {

        public async Task< bool> SendEmailAsync(string senderEmail, string email, string subject, string confirmlink)
        {


          return  await Task.Run(() =>
             {
                 try
                 {

                     var sender = new MailAddress(senderEmail);
                     var receiverEmail = new MailAddress(email);
                     var password = "weheosbesvgtteip";
                     var sub = subject;
                     var body = confirmlink;
                     var smtp = new SmtpClient
                     {
                         Host = "smtp.gmail.com",
                         Port = 587,
                         EnableSsl = true,
                         DeliveryMethod = SmtpDeliveryMethod.Network,
                         UseDefaultCredentials = false,
                         Credentials = new NetworkCredential(sender.Address, password)
                     };
                     using (var mess = new MailMessage(sender, receiverEmail)
                     {
                         Subject = subject,
                         Body = body
                     })
                     {
                         smtp.Send(mess);
                     }
                     return true;

                 }
                 catch(Exception e)
                 {
                     return false;
                 }
                 


             });
                    
                
            
           

           

        }

        public void SendEmail(string email, string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
