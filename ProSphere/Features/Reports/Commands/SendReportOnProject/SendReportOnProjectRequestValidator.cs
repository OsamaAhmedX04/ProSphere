using FluentValidation;
using ProSphere.Domain.Enums;

namespace ProSphere.Features.Reports.Commands.SendReportOnProject
{
    public class SendReportOnProjectRequestValidator : AbstractValidator<SendReportOnProjectRequest>
    {
        public SendReportOnProjectRequestValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Report Reason Is Required")
                .Must(reason => Enum.TryParse<ReportReason>(reason, true, out _)).WithMessage("Invalid Report Reason");
        }
    }
}
