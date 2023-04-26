using System.Net;
using System.Net.Mail;

namespace BooksApp.MVC.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSsl;
        private string _userName;
        private string _password;

        public SmtpEmailSender(string host, int port, bool enableSsl, string userName, string password)
        {
            _host = host;
            _port = port;
            _enableSsl = enableSsl;
            _userName = userName;
            _password = password;
        }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSsl
            };
            return client.SendMailAsync(
                    new MailMessage(_userName, email, subject, body)
                    {
                        IsBodyHtml=true
                    });
        }

    }
}
