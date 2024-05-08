using Application.Contract;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.Order
{
    public class CreateOrderCommandValidator 
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.PeopleId)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("{PropertyName} must bigger than 0");

            RuleFor(x => x.ProductId)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("{PropertyName} must bigger than 0");

            RuleFor(x => x.Qty)
                .NotNull();
        }

        protected override void RaiseValidationException
            (ValidationContext<CreateOrderCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
