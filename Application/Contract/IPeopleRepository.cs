using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Contract
{
    public interface IPeopleRepository : IRepositoryBase<People>
    {
        bool IsUniqueMobileNumber(string mobileNumber);
    }
}
