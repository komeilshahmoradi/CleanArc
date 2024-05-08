using Application.Contract;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrasructure.Implementation
{
    public class PeopleRepository 
        : RepositoryBase<Domain.Entities.People>, IPeopleRepository
    {
        public PeopleRepository(GolrangDbContext golrangDbContext) 
            : base(golrangDbContext)
        {
        }

        public bool IsUniqueMobileNumber(string mobileNumber)
        {
            return !_golrangDbContext.Peoples.Any(x => x.MobileNumber == mobileNumber);
        }
    }
}
