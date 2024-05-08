using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.Order
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("{PropertyName} must bigger than 0");


            RuleFor(x => x.Qty)
                .NotNull();
        }

        protected override void RaiseValidationException
            (ValidationContext<UpdateOrderCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
