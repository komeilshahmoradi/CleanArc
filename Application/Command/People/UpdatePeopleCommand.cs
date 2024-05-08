using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.People
{
    public record UpdatePeopleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePeopleCommandHandler 
        : IRequestHandler<UpdatePeopleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePeopleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePeopleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePeopleCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.People) + " WHERE Id = @Id";
            var exists = _unitOfWork.PeopleRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }

            exists.Password = request.Password;

            _unitOfWork.PeopleRepository.Update(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
