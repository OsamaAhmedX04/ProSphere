using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSphere.Domain.Entities;
using ProSphere.Extensions;
using ProSphere.ExternalServices.Interfaces.JWT;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;
using ProSphere.Shared.DTOs;

namespace ProSphere.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IJWTService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IValidator<LoginRequest> validator, IJWTService jwtService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _validator = validator;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(
            LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(command.request, cancellationToken);
            if (!result.IsValid)
            {
                var errors = result.ConvertErrorsToDictionary();
                return Result<LoginResponse>.ValidationFailure(errors);
            }

            var user = await _userManager.FindByEmailAsync(command.request.Email);

            if (user == null)
                return Result<LoginResponse>.Failure("User Not Found", 404);

            if (!user.EmailConfirmed)
                return Result<LoginResponse>.Failure("Email Not Confirmed Yet , Check Your Mail", 400);

            var loginResult = await _userManager.CheckPasswordAsync(user, command.request.Password);

            if (!loginResult)
                return Result<LoginResponse>.Failure("Wrong Email Or Password", 400);


            var userRole = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(new AuthenticatedUserDto
            {
                Id = user.Id,
                Role = userRole.First()
            });

            var refreshToken = Guid.NewGuid().ToString();

            var loginResponse = new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
            };

            var expirationRefreshTokenDate = command.request.RememberMe ? DateTime.UtcNow.AddMonths(3) : DateTime.UtcNow.AddHours(12);
            var tokenRow = await _unitOfWork.RefreshTokenAuths.GetAllAsIQueryable().FirstOrDefaultAsync(rt => rt.UserId == user.Id);
            if (tokenRow == null)
            {
                await _unitOfWork.RefreshTokenAuths.AddAsync(new RefreshTokenAuth
                {
                    UserId = user.Id,
                    Token = token,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = expirationRefreshTokenDate
                });
            }
            else
            {
                tokenRow.Token = token;
                tokenRow.RefreshToken = refreshToken;
                tokenRow.RefreshTokenExpiration = expirationRefreshTokenDate;
            }

            return Result<LoginResponse>.Success(loginResponse, "User Logged In Successfully");
        }
    }
}
