using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Order
{
    public record UpdateOrderCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }

    public class UpdateOrderCommandHandler 
        : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle
            (UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.Orders) + " WHERE Id = @Id";
            var exists = _unitOfWork.OrderRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }

            exists.Qty = request.Qty;

            _unitOfWork.OrderRepository.Update(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
