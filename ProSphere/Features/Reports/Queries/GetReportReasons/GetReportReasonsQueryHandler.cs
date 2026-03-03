using MediatR;
using ProSphere.Domain.Enums;

namespace ProSphere.Features.Reports.Queries.GetReportReasons
{
    public class GetReportReasonsQueryHandler : IRequestHandler<GetReportReasonsQuery, List<GetReportReasonsResponse>>
    {

        public GetReportReasonsQueryHandler()
        {
        }

        public Task<List<GetReportReasonsResponse>> Handle(GetReportReasonsQuery query, CancellationToken cancellationToken)
        {
            var result = Enum.GetValues(typeof(ReportReason))
                .Cast<ReportReason>()
                .Select(r => new GetReportReasonsResponse
                {
                    Id = (int)r,
                    Name = r.ToString(),
                    DisplayName = GetDisplayName(r),
                })
                .ToList();

            return Task.FromResult(result);
        }
        private static string GetDisplayName(ReportReason reason)
        {
            return reason switch
            {
                ReportReason.Spam => "Spam or repetitive content",
                ReportReason.InappropriateContent => "Inappropriate content",
                ReportReason.CopyrightViolation => "Copyright violation",
                ReportReason.Harassment => "Harassment or abuse",
                ReportReason.Scam => "Scam or fraud",
                ReportReason.Other => "Other",
                _ => reason.ToString()
            };
        }
    }
}
