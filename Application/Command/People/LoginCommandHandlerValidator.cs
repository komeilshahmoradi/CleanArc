using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.People
{
    public class LoginCommandHandlerValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandHandlerValidator()
        {
            RuleFor(x => x.MobileNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(11)
                .WithMessage("{PropertyName} length must be 11");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must be not null or empty")
                .WithErrorCode("Empty");
        }

        protected override void RaiseValidationException
            (ValidationContext<LoginCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
