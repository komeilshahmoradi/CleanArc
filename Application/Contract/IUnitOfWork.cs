namespace Application.Contract
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IPeopleRepository PeopleRepository { get; }
        IProductRepository ProductRepository { get; }

        bool SaveChanges();
    }
}
