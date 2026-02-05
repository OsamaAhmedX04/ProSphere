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

        public void SendResetPasswordMail(string email, string resettingLink)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Reset Your Password",
                    EmailBody.GetResetPasswordBody(email, resettingLink)
                    )
                );
        }

        public void SendDeleteAccountOTPMail(string email, string otp)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Delete Your Account",
                    EmailBody.GetDeleteAccountBody(email, otp)
                    )
                );
        }
    }
}
