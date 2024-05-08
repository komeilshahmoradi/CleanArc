using Application.Contract;
using Application.Query.People;
using AutoMapper;
using MediatR;

namespace Application.Query.Order
{
    public record GetAllOrderQuery : IRequest<List<OrderDto>>;

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM " + nameof(Domain.Entities.Orders);

            var dbResult = _unitOfWork.OrderRepository.GetAll(query);

            var result = _mapper.Map<List<OrderDto>>(dbResult);

            return result;
        }
    }
}
