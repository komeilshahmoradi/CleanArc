using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.Product
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => x > 0)
                .WithMessage("{PropertyName} must bigger than 0")
                .WithErrorCode("Range");
        }

        protected override void RaiseValidationException
            (ValidationContext<DeleteProductCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
