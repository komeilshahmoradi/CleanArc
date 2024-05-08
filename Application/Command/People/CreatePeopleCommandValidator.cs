using Application.Contract;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Command.People
{
    public class CreatePeopleCommandValidator 
        : AbstractValidator<CreatePeopleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePeopleCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.MobileNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(11)
                .WithMessage("{PropertyName} length must be 11")
                .WithErrorCode("Unique")
                .Must(BeUniqueMobileNumber)
                .WithMessage("{PropertyName} must be unique")
                .WithErrorCode("Unique");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must be not null or empty")
                .WithErrorCode("Empty");
        }

        public bool BeUniqueMobileNumber(string mobileNumber)
        {
            var result = _unitOfWork.PeopleRepository
                .IsUniqueMobileNumber(mobileNumber);
            return result;
        }

        protected override void RaiseValidationException
            (ValidationContext<CreatePeopleCommand> context, ValidationResult result)
        {
            var error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList());

            var ex = new Exception(error);

            throw ex;
        }
    }
}
