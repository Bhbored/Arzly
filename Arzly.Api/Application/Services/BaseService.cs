using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using AutoMapper;

namespace Arzly.Api.Application.Services
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey>
    : IBaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey>
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    {
        protected readonly IBaseRepository<TEntity, TKey> _repository;

        protected BaseService(IBaseRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities
                .ConvertAll(x => MapToDto(x));

        }

        public virtual async Task<TDto?> GetByIdAsync(TKey? id)
        {
            if (id == null) return default;

            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? default : MapToDto(entity);
        }

        public virtual async Task<TDto?> CreateAsync(TCreateDto? createDto)
        {
            if (createDto == null) return default;

            var entity = MapToEntity(createDto);
            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public virtual async Task<TDto?> UpdateAsync(TUpdateDto? updateDto)
        {
            if (updateDto == null) return default;

            var entity = MapToEntity(updateDto);
            var updatedEntity = _repository.Update(entity);
            return MapToDto(updatedEntity);
        }

        public virtual async Task<bool> DeleteAsync(TKey? id)
        {
            if (id == null) return false;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return _repository.Delete(entity);
        }

        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TCreateDto createDto);
        protected abstract TEntity MapToEntity(TUpdateDto updateDto);
    }
}
