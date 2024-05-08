using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.People
{
    public record DeletePeopleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeletePeopleCommandHandler : IRequestHandler<DeletePeopleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePeopleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeletePeopleCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.People) + " WHERE Id = @Id";
            var exists = _unitOfWork.PeopleRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }
            
            _unitOfWork.PeopleRepository.Delete(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
