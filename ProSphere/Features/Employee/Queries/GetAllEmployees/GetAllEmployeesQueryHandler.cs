using LinqKit;
using MediatR;
using ProSphere.Domain.Entities;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.RepositoryManager.Pagination;
using ProSphere.ResultResponse;
using System.Linq.Expressions;

namespace ProSphere.Features.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler :
        IRequestHandler<GetAllEmployeesQuery, Result<PageSourcePagination<GetAllEmployeesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PageSourcePagination<GetAllEmployeesResponse>>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Employee, bool>> filter = e => true;

            if(!string.IsNullOrEmpty(query.name))
                filter = filter.And(e => e.Name.Contains(query.name));

            if (!string.IsNullOrEmpty(query.country))
                filter = filter.And(e => e.Country.Contains(query.country));

            var result = await _unitOfWork.Employees.GetAllPaginatedEnhancedAsync(
                filter: filter,
                selector: e => new GetAllEmployeesResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Country = e.Country,
                    StartWorkAt = e.StartWorkAt,
                    EndWorkAt = e.EndWorkAt,
                    ModeratorAccountId = e.AssignedTo,
                    ModeratorAccountCode = e.Moderator == null ? null : e.Moderator.Code
                });

            return Result<PageSourcePagination<GetAllEmployeesResponse>>.Success(result, "Paginated Employees Retreived Successfully");
        }
    }
}
