using Application.Contract;

namespace Infrasructure.Implementation
{
    public class OrderRepository 
        : RepositoryBase<Domain.Entities.Orders>, IOrderRepository
    {
        public OrderRepository(GolrangDbContext golrangDbContext) 
            : base(golrangDbContext)
        {
        }
    }
}
