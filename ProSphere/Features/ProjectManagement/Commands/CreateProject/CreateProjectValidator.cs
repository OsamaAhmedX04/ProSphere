using FluentValidation;
using ProSphere.Domain.Constants.FileConstants;

namespace ProSphere.Features.ProjectManagement.Commands.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Short description is required.")
                .MaximumLength(500).WithMessage("Short description must not exceed 500 characters.");

            RuleFor(x => x.Problem)
                .NotEmpty().WithMessage("Problem description is required.")
                .MaximumLength(2000).WithMessage("Problem must not exceed 2000 characters.");

            RuleFor(x => x.SolutionSummary)
                .NotEmpty().WithMessage("Solution summary is required.")
                .MaximumLength(2000).WithMessage("Solution summary must not exceed 2000 characters.");

            RuleFor(x => x.Market)
                .NotEmpty().WithMessage("Market information is required.")
                .MaximumLength(1000).WithMessage("Market must not exceed 1000 characters.");

            RuleFor(x => x.NeededInvestment)
                .GreaterThan(0).WithMessage("Needed investment must be greater than 0.");

            RuleFor(x => x.EquityPercentage)
                .InclusiveBetween(0.1, 100)
                .WithMessage("Equity percentage must be between 0.1 and 100.");

            RuleFor(x => x.ExecutionPlan)
                .NotEmpty().WithMessage("Execution plan is required.")
                .MaximumLength(3000).WithMessage("Execution plan must not exceed 3000 characters.");

            RuleFor(x => x.FinancialDetails)
                .NotEmpty().WithMessage("Financial details are required.")
                .MaximumLength(3000).WithMessage("Financial details must not exceed 3000 characters.");

            RuleFor(x => x.BusinessModel)
                .NotEmpty().WithMessage("Business model is required.")
                .MaximumLength(2000).WithMessage("Business model must not exceed 2000 characters.");

            RuleFor(x => x.MarketingStrategy)
                .NotEmpty().WithMessage("Marketing strategy is required.")
                .MaximumLength(2000).WithMessage("Marketing strategy must not exceed 2000 characters.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .WithMessage("Notes must not exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));

            RuleFor(x => x.Images)
                .Must(images => images == null || images.Count <= 4)
                .WithMessage("You can upload a maximum of 4 images.");

            RuleForEach(x => x.Images)
                .NotNull().WithMessage("Image file cannot be null.")
                .Must(file => file.Length > 0)
                .WithMessage("Image file cannot be empty.")
                .When(x => x.Images != null);

            RuleForEach(x => x.Images)
                .Must(file => FileRestriction.AllowableImageExtensions.Contains(Path.GetExtension(file.FileName)))
                .WithMessage($"Invalid image format. Allowed formats are: {string.Join(',', FileRestriction.AllowableImageExtensions)}")
                .When(x => x.Images != null);

            RuleForEach(x => x.Images)
                .Must(file => file.Length <= FileRestriction.AllowableImageFileSize)
                .WithMessage($"Each image must not exceed 1MB.")
                .When(x => x.Images != null);
        }
    }
}
