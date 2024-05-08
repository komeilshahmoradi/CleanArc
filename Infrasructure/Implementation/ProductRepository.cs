using Application.Contract;

namespace Infrasructure.Implementation
{
    public class ProductRepository 
        : RepositoryBase<Domain.Entities.Products>, IProductRepository
    {
        public ProductRepository(GolrangDbContext golrangDbContext) 
            : base(golrangDbContext)
        {
        }
    }
}
