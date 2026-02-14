using FluentValidation;

namespace ProSphere.Features.SocialMediaAccounts.Commands.SetSocialMediaAccounts
{
    public class SetSocialMediaAccountsValidator : AbstractValidator<SetSocialMediaAccountsRequest>
    {
        public SetSocialMediaAccountsValidator()
        {
            RuleFor(u => u.FacebookURL)
                .MaximumLength(500).WithMessage("Max Length Of Facebook URL Is 500")
                .When(u => !string.IsNullOrEmpty(u.FacebookURL));

            RuleFor(u => u.LinkedInURL)
                .MaximumLength(500).WithMessage("Max Length Of LinkedIn URL Is 500")
                .When(u => !string.IsNullOrEmpty(u.LinkedInURL));

            RuleFor(u => u.GitHubURL)
                .MaximumLength(500).WithMessage("Max Length Of GitHub URL Is 500")
                .When(u => !string.IsNullOrEmpty(u.GitHubURL));



            RuleFor(u => u.FirstPlatformName)
                .MaximumLength(500).WithMessage("Max Length Of First Platform Name Is 500")
                .When(u => !string.IsNullOrEmpty(u.FirstPlatformName));
            RuleFor(u => u.FirstPlatformURL)
                .MaximumLength(500).WithMessage("Max Length Of First Platform URL Is 500")
                .When(u => !string.IsNullOrEmpty(u.FirstPlatformURL));

            RuleFor(u => u.FirstPlatformName)
                .NotEmpty().WithMessage("First Platform Name Is Required")
                .When(u => !string.IsNullOrEmpty(u.FirstPlatformURL));
            RuleFor(u => u.FirstPlatformURL)
                .NotEmpty().WithMessage("First Platform URL Is Required")
                .When(u => !string.IsNullOrEmpty(u.FirstPlatformName));




            RuleFor(u => u.SecondPlatformName)
                .MaximumLength(500).WithMessage("Max Length Of Second Platform Name Is 500")
                .When(u => !string.IsNullOrEmpty(u.SecondPlatformName));
            RuleFor(u => u.SecondPlatformURL)
                .MaximumLength(500).WithMessage("Max Length Of Second Platform URL Is 500")
                .When(u => !string.IsNullOrEmpty(u.SecondPlatformURL));

            RuleFor(u => u.SecondPlatformName)
                .NotEmpty().WithMessage("Second Platform Name Is Required")
                .When(u => !string.IsNullOrEmpty(u.SecondPlatformURL));
            RuleFor(u => u.SecondPlatformURL)
                .NotEmpty().WithMessage("Second Platform URL Is Required")
                .When(u => !string.IsNullOrEmpty(u.SecondPlatformName));




            RuleFor(u => u.ThirdPlatformName)
                .MaximumLength(500).WithMessage("Max Length Of Third Platform Name Is 500")
                .When(u => !string.IsNullOrEmpty(u.ThirdPlatformName));
            RuleFor(u => u.ThirdPlatformURL)
                .MaximumLength(500).WithMessage("Max Length Of Third Platform Name Is 500")
                .When(u => !string.IsNullOrEmpty(u.ThirdPlatformURL));

            RuleFor(u => u.ThirdPlatformName)
                .NotEmpty().WithMessage("Third Platform Name Is Required")
                .When(u => !string.IsNullOrEmpty(u.ThirdPlatformURL));
            RuleFor(u => u.ThirdPlatformURL)
                .NotEmpty().WithMessage("Third Platform URL Is Required")
                .When(u => !string.IsNullOrEmpty(u.ThirdPlatformName));
        }
    }
}
