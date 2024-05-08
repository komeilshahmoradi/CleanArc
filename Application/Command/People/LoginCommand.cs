using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.People
{
    public record LoginCommand : IRequest<string>
    {
        public string MobileNumber { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IJwtUtils jwtUtils, IUnitOfWork unitOfWork)
        {
            _jwtUtils = jwtUtils;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandHandlerValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.People)
                + " WHERE MobileNumber = @MobileNumber AND Password = @Password";

            var exists = _unitOfWork.PeopleRepository.Get(query,parameters);

            if (exists is null)
            {
                return "MobileNumber or password is wrong!";
            }

            var token = _jwtUtils.GenerateJwtToken(exists);

            return token;
        }
    }
}
