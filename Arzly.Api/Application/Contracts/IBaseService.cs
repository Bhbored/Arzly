namespace Arzly.Api.Application.Contracts
{
    public interface IBaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey>
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey? id);
        Task<TDto?> CreateAsync(TCreateDto? createDto);
        Task<TDto?> UpdateAsync(TUpdateDto? updateDto);
        Task<bool> DeleteAsync(TKey? id);
    }
}