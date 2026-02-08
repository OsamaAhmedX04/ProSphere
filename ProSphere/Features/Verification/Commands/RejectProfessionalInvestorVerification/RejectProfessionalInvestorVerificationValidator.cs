using FluentValidation;

namespace ProSphere.Features.Verification.Commands.RejectProfessionalInvestorVerification
{
    public class RejectProfessionalInvestorVerificationValidator : AbstractValidator<RejectProfessionalInvestorVerificationRequest>
    {
        public RejectProfessionalInvestorVerificationValidator()
        {
            RuleFor(v => v.RejectReason)
                .NotEmpty().WithMessage("Rejection Reason Is Required")
                .MaximumLength(500).WithMessage("Max Length Of Rejection Reason Is 500");
        }
    }
}
