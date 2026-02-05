using Hangfire;
using ProSphere.Domain.Constants;
using ProSphere.ExternalServices.Interfaces.Email;

namespace ProSphere.ExternalServices.Implementaions.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        public void SendEmailConfirmationMail(
            string email, string confirmationLink, string firstName, string lastName, string role)
        {

            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Confirm Your Email",
                    EmailBody.GetEmailConfirmationBody(email, confirmationLink, firstName, lastName, role)
                    )
                );
        }
    }
}
