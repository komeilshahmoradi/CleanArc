using Application.Contract;
using AutoMapper;
using MediatR;

namespace Application.Query.People
{
    public record GetAllPeople : IRequest<List<PeopleDto>>;

    public class GetPeopleQueryHandler : IRequestHandler<GetAllPeople, List<PeopleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPeopleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PeopleDto>> Handle(GetAllPeople request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM " + nameof(Domain.Entities.People);

            var dbResult = _unitOfWork.PeopleRepository.GetAll(query);

            var result = _mapper.Map<List<PeopleDto>>(dbResult);

            return result;
        }
    }
}
