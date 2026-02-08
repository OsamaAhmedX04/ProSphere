using FluentValidation;

namespace ProSphere.Features.Verification.Commands.RejectIdentityVerification
{
    public class RejectIdentityVerificationValidator : AbstractValidator<RejectIdentityVerificationRequest>
    {
        public RejectIdentityVerificationValidator()
        {
            RuleFor(v => v.RejectReason)
                .NotEmpty().WithMessage("Rejection Reason Is Required")
                .MaximumLength(500).WithMessage("Max Length Of Rejection Reason Is 500");
        }
    }
}
