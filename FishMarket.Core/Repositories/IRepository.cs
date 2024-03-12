using FishMarket.Domain;
using System.Linq.Expressions;

namespace FishMarket.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Detach(T entity);
    }
}
