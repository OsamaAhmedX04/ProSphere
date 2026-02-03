namespace ProSphere.ExternalServices.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
