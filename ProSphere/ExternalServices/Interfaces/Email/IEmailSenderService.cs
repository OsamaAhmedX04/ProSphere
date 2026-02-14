namespace ProSphere.ExternalServices.Interfaces.Email
{
    public interface IEmailSenderService
    {
        void SendEmailConfirmationMail(string email, string confirmationLink, string firstName, string lastName, string role);
        void SendResetPasswordMail(string email, string resettingLink);
        void SendDeleteAccountOTPMail(string email, string otp);
        void SendAcceptanceVerifiedIdentityMail(string email, string firstName, string lastName);
        void SendRejectionVerifiedIdentityMail(string email, string firstName, string lastName, string rejectionReason);
        void SendAcceptanceVerifiedFinancialMail(string email, string firstName, string lastName);
        void SendRejectionVerifiedFinancialMail(string email, string firstName, string lastName, string rejectionReason);
        void SendAcceptanceVerifiedProfessionalMail(string email, string firstName, string lastName);
        void SendRejectionVerifiedProfessionalMail(string email, string firstName, string lastName, string rejectionReason);
        void SendWelcomeEmployeeMail(string email, string name);

    }
}
