using Application.Contract;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Product
{
    public record CreateProductCommand
        : IRequest<bool>
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateProductCommand request
            , CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            validator.ValidateAndThrow(request);

            var mapped = _mapper.Map<Domain.Entities.Products>(request);

            _unitOfWork.ProductRepository.Create(mapped);

            return _unitOfWork.SaveChanges();
        }
    }
}
