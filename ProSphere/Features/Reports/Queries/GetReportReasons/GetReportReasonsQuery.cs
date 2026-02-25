using MediatR;

namespace ProSphere.Features.Reports.Queries.GetReportReasons
{
    public record GetReportReasonsQuery : IRequest<List<GetReportReasonsResponse>>
    {
    }
}
