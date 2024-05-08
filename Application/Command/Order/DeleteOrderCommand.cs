using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Order
{
    public record DeleteOrderCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteOrderCommandHandler 
        : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle
            (DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteOrderCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.Orders) + " WHERE Id = @Id";
            var exists = _unitOfWork.OrderRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }
            
            _unitOfWork.OrderRepository.Delete(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
