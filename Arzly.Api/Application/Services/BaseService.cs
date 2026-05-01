using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Shared.Constants;

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

        //complete since we're gonna provide empty list in repo layer in case of none elements
        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities
                .ConvertAll(x => MapToDto(x));

        }
        //all validation can be done here
        public virtual async Task<TDto?> GetByIdAsync(TKey? id)
        {
            if (id == null)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                throw new ArgumentException($"No Object with this ID {id} Found");
            return MapToDto(entity);
        }
        //use it at the end as base each entity has it own case validation,don't repeated made validations
        public virtual async Task<TDto?> CreateAsync(TCreateDto? createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            var entity = MapToEntity(createDto);
            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        //same as create each entity update case differs
        public virtual async Task<TDto?> UpdateAsync(TUpdateDto? updateDto)
        {
            if (updateDto == null)
                throw new ArgumentNullException(ExceptionMessages.EmptyUpdateRequest);

            var entity = MapToEntity(updateDto);
            var updatedEntity = await _repository.Update(entity);
            return MapToDto(updatedEntity);
        }

        //this is enough
        public virtual async Task<bool> DeleteAsync(TKey? id)
        {
            if (id == null)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            if (id is not Guid)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            if (id is Guid guid)
            {
                if (guid == Guid.Empty)
                    throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.Delete(entity);
        }

        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TCreateDto createDto);
        protected abstract TEntity MapToEntity(TUpdateDto updateDto);
    }
}