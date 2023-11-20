

using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AlBum.SendMail {
    public class MailSettings {
        public string Mail {set;get;} ="";
        public string DisplayName {set;get;} ="";
        public string Password {set;get;} ="";
        public string Host {set;get;} ="";

        public int Port {set;get;}


        // Dịch vụ gửi mail 
        public class SendMailService : IEmailSender
        {

            private readonly MailSettings mailSettings ;
            private readonly ILogger<SendMailService> logger;
            
            public SendMailService(IOptions<MailSettings> _mailSettings , ILogger<SendMailService> _logger)
            {
                mailSettings = _mailSettings.Value;
                logger = _logger;
                logger.LogInformation("Create Send Email Service ");
            }

            public async Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                MimeMessage message = new MimeMessage();
                message.Sender = new MailboxAddress(mailSettings.DisplayName ,mailSettings.Mail);
                message.From.Add(message.Sender);
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = htmlMessage;
                message.Body = builder.ToMessageBody();

                // Dùng Smtp của MailKit
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                try {
                    smtp.Connect (mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate (mailSettings.Mail, mailSettings.Password);
                    await smtp.SendAsync (message);
                } catch (Exception ex) {
                    Directory.CreateDirectory("mailssave");
                    var emailsavefile = string.Format (@"mailssave/{0}.eml", Guid.NewGuid ());
                    await message.WriteToAsync (emailsavefile);

                    logger.LogInformation ("Lỗi gửi mail, lưu tại - " + emailsavefile);
                    logger.LogError (ex.Message);
                }
                
                smtp.Disconnect(true);
                logger.LogInformation ("send mail to: " + email);
            }
        }
    }
}