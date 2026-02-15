using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProSphere.Domain.Constants.RoleConstants;
using ProSphere.Domain.Entities;
using ProSphere.Domain.Enums;
using ProSphere.Extensions;
using ProSphere.Helpers.Generators;
using ProSphere.RepositoryManager.Interfaces;
using ProSphere.ResultResponse;

namespace ProSphere.Features.Moderator.Commands.CreateModerator
{
    public class CreateModeratorCommandHandler : IRequestHandler<CreateModeratorCommand, Result<CreateModeratorResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateModeratorRequest> _validator;

        public CreateModeratorCommandHandler(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork, IValidator<CreateModeratorRequest> validator)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<CreateModeratorResponse>> Handle(CreateModeratorCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ConvertErrorsToDictionary();
                return Result<CreateModeratorResponse>.ValidationFailure(errors);
            }

            var newUser = new ApplicationUser
            {
                FirstName = "Moderator",
                LastName = "ProSphere",
                Email = command.request.Email,
                UserName = command.request.Email,
                EmailConfirmed = true,
                Gender = Gender.None
            };

            var response = new CreateModeratorResponse { TempPassword = PasswordGenerator.Generate(PasswordDificulty.Low) };

            var creatingResult = await _userManager.CreateAsync(newUser, response.TempPassword);

            if (!creatingResult.Succeeded)
            {
                var errors = creatingResult.ConvertErrorsToDictionary();
                return Result<CreateModeratorResponse>.ValidationFailure(errors);
            }

            var assigningRoleResult = await _userManager.AddToRoleAsync(newUser, Role.InActiveModerator);

            if (!assigningRoleResult.Succeeded)
            {
                var errors = creatingResult.ConvertErrorsToDictionary();
                return Result<CreateModeratorResponse>.ValidationFailure(errors);
            }

            var numberOfModerators = await _unitOfWork.Moderators.Count();

            var moderatorCode = $"{AccountCodeGenerator.Generate()}MOD{numberOfModerators + 1}";

            var newModerator = new Domain.Entities.Moderator { Id = newUser.Id, Code = moderatorCode };
            await _unitOfWork.Moderators.AddAsync(newModerator);
            await _unitOfWork.CompleteAsync();

            return Result<CreateModeratorResponse>.Success(response, "Moderator Account Has Been Created Successfully");
        }
    }
}
