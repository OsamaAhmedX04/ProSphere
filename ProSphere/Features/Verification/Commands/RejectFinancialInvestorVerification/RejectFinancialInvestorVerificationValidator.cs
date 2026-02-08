using FluentValidation;

namespace ProSphere.Features.Verification.Commands.RejectFinancialInvestorVerification
{
    public class RejectFinancialInvestorVerificationValidator : AbstractValidator<RejectFinancialInvestorVerificationRequest>
    {
        public RejectFinancialInvestorVerificationValidator()
        {
            RuleFor(v => v.RejectReason)
                .NotEmpty().WithMessage("Rejection Reason Is Required")
                .MaximumLength(500).WithMessage("Max Length Of Rejection Reason Is 500");
        }
    }
}
