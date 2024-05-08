using Application.Contract;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Infrasructure.Implementation
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly GolrangDbContext _golrangDbContext;
        protected readonly string ConnectionString;
        protected RepositoryBase(GolrangDbContext golrangDbContext)
        {
            _golrangDbContext = golrangDbContext;
            ConnectionString = _golrangDbContext?.Database?.GetConnectionString() ?? "";
        }
        public void Create(T entiry) => _golrangDbContext.Set<T>().Add(entiry);

        public void Delete(T entiry) => _golrangDbContext.Set<T>().Remove(entiry);

        public void Update(T entiry) => _golrangDbContext.Set<T>().Update(entiry);

        public List<T> GetAll(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Query<T>(query);
                return result.ToList();
            }
        }

        public T Get(string query,DynamicParameters dynamicParameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<T>(query, dynamicParameters);
                return result;
            }
        }
    }
}
