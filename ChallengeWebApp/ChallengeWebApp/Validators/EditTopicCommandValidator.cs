using ChallengeWebApp.Application;
using FluentValidation;

namespace ChallengeWebApp.Validators
{
    public class EditTopicCommandValidator : AbstractValidator<EditTopicCommand>
    {
        public EditTopicCommandValidator()
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