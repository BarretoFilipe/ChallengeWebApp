using ChallengeWebApp.Application;
using FluentValidation;

namespace ChallengeWebApp.Validators
{
    public class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicCommandValidator()
        {
            RuleFor(topic => topic.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(topic => topic.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}