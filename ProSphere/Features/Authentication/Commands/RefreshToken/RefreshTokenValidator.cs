using FluentValidation;

namespace ProSphere.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(rf => rf.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token Is Required");
        }
    }
}
