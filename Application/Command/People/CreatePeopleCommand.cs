using Application.Contract;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Command.People
{
    public record CreatePeopleCommand(string MobileNumber,string Password) 
        : IRequest<bool>;

    public class CreatePeopleCommandHandled
        : IRequestHandler<CreatePeopleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePeopleCommandHandled(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreatePeopleCommand request
            , CancellationToken cancellationToken)
        {
            var validator = new CreatePeopleCommandValidator(_unitOfWork);
            validator.ValidateAndThrow(request);

            var mapped = _mapper.Map<Domain.Entities.People>(request);

            _unitOfWork.PeopleRepository.Create(mapped);

            return _unitOfWork.SaveChanges();
        }
    }
}
