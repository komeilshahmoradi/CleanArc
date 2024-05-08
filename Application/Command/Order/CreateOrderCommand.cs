using Application.Contract;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Command.Order
{
    public record CreateOrderCommand
        : IRequest<bool>
    {
        public int PeopleId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateOrderCommand request
            , CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(request);

            var mapped = _mapper.Map<Domain.Entities.Orders>(request);

            _unitOfWork.OrderRepository.Create(mapped);

            return _unitOfWork.SaveChanges();
        }
    }
}
