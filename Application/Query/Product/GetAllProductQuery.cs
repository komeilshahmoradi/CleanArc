using Application.Contract;
using AutoMapper;
using MediatR;

namespace Application.Query.Product
{
    public record GetAllProductQuery : IRequest<List<ProductDto>>;

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM " + nameof(Domain.Entities.Products);
            var dbResult = _unitOfWork.ProductRepository.GetAll(query);

            var result = _mapper.Map<List<ProductDto>>(dbResult);

            return result;
        }
    }
}
