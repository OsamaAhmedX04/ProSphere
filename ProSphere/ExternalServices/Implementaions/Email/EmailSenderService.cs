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

        public void SendAcceptanceVerifiedIdentityMail(string email, string firstName, string lastName)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Identity Verified Successfully",
                    EmailBody.GetVerifiedIdentityAcceptanceBody(email, firstName, lastName)
                    )
                );
        }

        public void SendRejectionVerifiedIdentityMail(string email, string firstName, string lastName, string rejectionReason)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Identity Rejected",
                    EmailBody.GetVerifiedIdentityRejectionBody(email, firstName, lastName, rejectionReason)
                    )
                );
        }

        public void SendAcceptanceVerifiedFinancialMail(string email, string firstName, string lastName)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Financail Verification Accepted",
                    EmailBody.GetVerifiedFinancialAcceptanceBody(email, firstName, lastName)
                    )
                );
        }

        public void SendRejectionVerifiedFinancialMail(string email, string firstName, string lastName, string rejectionReason)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Financail Verification Rejected",
                    EmailBody.GetVerifiedFinancialRejectionBody(email, firstName, lastName, rejectionReason)
                    )
                );
        }

        public void SendAcceptanceVerifiedProfessionalMail(string email, string firstName, string lastName)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Professinal Verification Accepted",
                    EmailBody.GetVerifiedProfessionalAcceptanceBody(email, firstName, lastName)
                    )
                );
        }

        public void SendRejectionVerifiedProfessionalMail(string email, string firstName, string lastName, string rejectionReason)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Professinal Verification Rejected",
                    EmailBody.GetVerifiedProfessionalRejectionBody(email, firstName, lastName, rejectionReason)
                    )
                );
        }

        public void SendWelcomeEmployeeMail(string email, string name)
        {
            BackgroundJob.Enqueue<IEmailService>(
                service => service.SendEmailAsync(
                    email,
                    "Welcome To Our New Employee",
                    EmailBody.GetWelcomeNewEmployeeBody(email, name)
                    )
                );
        }
    }
}
