using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.Helpers;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Result<CreateAdminResponse>>
    {
        private readonly IValidator<CreateAdminRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateAdminCommandHandler(
            IValidator<CreateAdminRequest> validator, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result<CreateAdminResponse>> Handle(CreateAdminCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result<CreateAdminResponse>.ValidationFailure(errors);
            }

            var newUser = new ApplicationUser
            {
                UserName = command.request.Email,
                Email = command.request.Email,
                FirstName = command.request.FirstName,
                LastName = command.request.LastName,
                IsVerified = true,
                EmailConfirmed = true,
                Gender = Gender.None
            };
            var response = new CreateAdminResponse { TempPassword = PasswordGenerator.Generate(PasswordDificulty.Low) };

            var creatingResult = await _userManager.CreateAsync(newUser, response.TempPassword);
            if (!creatingResult.Succeeded)
            {
                var errors = creatingResult.ConvertErrorsToDictionary();
                return Result<CreateAdminResponse>.ValidationFailure(errors);
            }

            await _userManager.AddToRoleAsync(newUser, Role.InActiveAdmin);

            var newAdmin = new Domain.Entities.Admin
            {
                Id = newUser.Id,
                IsSuperAdmin = false
            };

            await _unitOfWork.Admins.AddAsync(newAdmin);
            await _unitOfWork.CompleteAsync();


            return Result<CreateAdminResponse>.Success(response, "Admin Account Has Been Created Successfully");
        }
    }
}
