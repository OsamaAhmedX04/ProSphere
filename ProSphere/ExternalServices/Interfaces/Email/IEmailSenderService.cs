namespace ProSphere.ExternalServices.Interfaces.Email
{
    public interface IEmailSenderService
    {
        void SendEmailConfirmationMail(string email, string confirmationLink, string firstName, string lastName, string role);
        void SendResetPasswordMail(string email, string resettingLink);
        public void SendDeleteAccountOTPMail(string email, string otp);
        public void SendAcceptanceVerifiedIdentityMail(string email, string firstName, string lastName);
        public void SendRejectionVerifiedIdentityMail(string email, string firstName, string lastName, string rejectionReason);
        public void SendAcceptanceVerifiedFinancialMail(string email, string firstName, string lastName);
        public void SendRejectionVerifiedFinancialMail(string email, string firstName, string lastName, string rejectionReason);
        public void SendAcceptanceVerifiedProfessionalMail(string email, string firstName, string lastName);
        public void SendRejectionVerifiedProfessionalMail(string email, string firstName, string lastName, string rejectionReason);

    }
}
