using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Car_Rental_Backend_Application.Data
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; 
        private readonly int _smtpPort = 587;                 
        private readonly string _smtpUsername = "carrentalsdriveease@gmail.com"; 
        private readonly string _smtpPassword = "zigbcftlpozjnenc"; 


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();

          
            emailMessage.From.Add(new MailboxAddress("Car Rental Service", _smtpUsername));

           
            emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));

           
            emailMessage.Subject = subject;

         
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message 
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();

          
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
