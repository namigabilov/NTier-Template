using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;
using System.Text.RegularExpressions;

namespace Core.Helpers.Email
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailManager(IConfiguration config)
        {
            _config = config;
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new(pattern);
            return regex.IsMatch(email);
        }
        public async Task<IResult> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                string senderEmail = _config["SmtpSetting:Email"];
                string smtpServer = _config["SmtpSetting:Host"];
                int port = Convert.ToInt32(_config["SmtpSetting:Port"]);
                string senderPassword = _config["SmtpSetting:Password"];

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(senderEmail));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;



                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = body
                };

                using (MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtpClient.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(senderEmail, senderPassword);
                    await smtpClient.SendAsync(email);
                    await smtpClient.DisconnectAsync(true);
                    smtpClient.Dispose();
                }

                return new SuccessResult(message: "Email uğurla göndərildi", statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: "Gözlənilməz xəta baş verdi", statusCode: HttpStatusCode.BadRequest);
            }
        }

    }
}
