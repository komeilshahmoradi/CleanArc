using Dapper;

namespace Application.Contract
{
    public interface IRepositoryBase<T>
    {
        List<T> GetAll(string query);
        T Get(string query, DynamicParameters dynamicParameters);
        void Create(T entiry);
        void Update(T entiry);
        void Delete(T entiry);
    }
}
