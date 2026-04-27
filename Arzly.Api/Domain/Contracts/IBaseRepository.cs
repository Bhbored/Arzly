namespace Arzly.Api.Domain.Contracts
{
    public interface IBaseRepository<TEntity,TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey? id);
        Task AddAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
    }
}
