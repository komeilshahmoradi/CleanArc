﻿using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.Product
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} max length must be 50");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("{PropertyName} must be not null or empty");
        }

        protected override void RaiseValidationException
            (ValidationContext<UpdateProductCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}