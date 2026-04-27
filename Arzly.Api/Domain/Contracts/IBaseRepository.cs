namespace Arzly.Api.Domain.Contracts
{
    public interface IBaseRepository<TEntity,TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey? id);
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
