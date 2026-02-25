using FluentValidation;
using ProSphere.Domain.Enums;

namespace ProSphere.Features.Reports.Commands.SendReportOnUser
{
    public class SendReportOnUserValidator : AbstractValidator<SendReportOnUserRequest>
    {
        public SendReportOnUserValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Report Reason Is Required")
                .Must(reason => Enum.TryParse<ReportReason>(reason, true, out _)).WithMessage("Invalid Report Reason");
        }
    }
}
