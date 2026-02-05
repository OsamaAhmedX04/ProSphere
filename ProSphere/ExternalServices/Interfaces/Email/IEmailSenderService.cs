namespace ProSphere.ExternalServices.Interfaces.Email
{
    public interface IEmailSenderService
    {
        void SendEmailConfirmationMail(string email, string confirmationLink, string firstName, string lastName, string role);
        void SendResetPasswordMail(string email, string resettingLink);
    }
}
