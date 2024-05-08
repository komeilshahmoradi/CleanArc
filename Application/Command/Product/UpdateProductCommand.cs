using Application.Contract;
using Dapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Product
{
    public record UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler 
        : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle
            (UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            validator.ValidateAndThrow(request);

            var parameters = new DynamicParameters(request);

            var query = "SELECT * FROM " + nameof(Domain.Entities.Products) + " WHERE Id = @Id";
            var exists = _unitOfWork.ProductRepository.Get(query, parameters);

            if (exists is null)
            {
                return false;
            }

            exists.Price = request.Price;
            exists.Name = request.Name;

            _unitOfWork.ProductRepository.Update(exists);

            return _unitOfWork.SaveChanges();
        }
    }
}
