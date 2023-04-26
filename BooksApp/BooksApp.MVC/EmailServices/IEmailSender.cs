namespace BooksApp.MVC.EmailServices
{
    public interface IEmailSender
    {
        /* Burası farklı mail serverlara göre mail gönderme metotlarımızı yapılandırabilmek için tasarladığımız bir interface */
        Task SendEmailAsync(string email, string subject, string body);
    }
}
