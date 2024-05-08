using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.People
{
    public class UpdatePeopleCommandValidator : AbstractValidator<UpdatePeopleCommand>
    {
        public UpdatePeopleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("{PropertyName} must bigger than 0")
                .WithErrorCode("Range");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} can not be empty")
                .WithErrorCode("Empty");
        }

        protected override void RaiseValidationException
            (ValidationContext<UpdatePeopleCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
