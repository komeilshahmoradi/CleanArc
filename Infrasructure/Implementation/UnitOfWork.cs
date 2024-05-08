using Application.Contract;

namespace Infrasructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GolrangDbContext _golrangDbContext;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IPeopleRepository _peopleRepository;

        public UnitOfWork(GolrangDbContext golrangDbContext)
        {
            _golrangDbContext = golrangDbContext;
        }

        public IOrderRepository OrderRepository
        { 
            get 
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_golrangDbContext);
                }
                return _orderRepository;
            }
        }

        public IPeopleRepository PeopleRepository
        {
            get
            {
                if (_peopleRepository is null)
                {
                    _peopleRepository = new PeopleRepository(_golrangDbContext);
                }
                return _peopleRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository is null)
                {
                    _productRepository = new ProductRepository(_golrangDbContext);
                }
                return _productRepository;
            }
        }

        public bool SaveChanges()
        {
            return _golrangDbContext.SaveChanges() > 0;
        }
    }
}
