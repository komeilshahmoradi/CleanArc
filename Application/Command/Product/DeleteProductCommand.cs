using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Product
{
    public record DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler 
        : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle
            (DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.Products) + " WHERE Id = @Id";
            var exists = _unitOfWork.ProductRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }
            
            _unitOfWork.ProductRepository.Delete(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
