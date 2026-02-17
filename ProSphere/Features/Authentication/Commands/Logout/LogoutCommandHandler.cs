using MediatR;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using System.Security.Claims;

namespace ProSphere.Features.Authentication.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Result.Failure("User Not Authenticated", StatusCodes.Status401Unauthorized);

            var tokenRow = await _unitOfWork.RefreshTokenAuths.FirstOrDefaultAsync(rt => rt.UserId == userId);

            if(tokenRow == null)
                return Result.Failure("User Already Logged Out", StatusCodes.Status401Unauthorized);

            _unitOfWork.RefreshTokenAuths.Delete(tokenRow.UserId);
            await _unitOfWork.CompleteAsync();
            
            return Result.Success("Logged Out Successfully");
        }
    }
}
