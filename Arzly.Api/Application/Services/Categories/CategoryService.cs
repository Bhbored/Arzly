using Arzly.Api.Application.Contracts.Categories;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Application.Services.Categories
{
    public class CategoryService : BaseService<Category, CategoryResponse, CategoryAddRequest, CategoryUpdateRequest, Guid>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _categoryRepository = repository;
        }

        protected override CategoryResponse MapToDto(Category entity) => entity.ToResponse();
        protected override Category MapToEntity(CategoryAddRequest createDto) => createDto.ToEntity();
        protected override Category MapToEntity(CategoryUpdateRequest updateDto) => updateDto.ToEntity();

        public override async Task<CategoryResponse?> CreateAsync(CategoryAddRequest? createDto, Guid userId)
        {
            if (createDto is null) throw new ArgumentNullException(nameof(createDto));
            var name = createDto.Name.Trim();
            if (await _categoryRepository.NameExistsAsync(name))
                throw new ArgumentException("A category with this name already exists");
            var entity = createDto.ToEntity();
            entity.Name = name;
            await _categoryRepository.AddAsync(entity);
            return entity.ToResponse();
        }

        public override async Task<CategoryResponse?> UpdateAsync(CategoryUpdateRequest? updateDto, Guid userId)
        {
            if (updateDto is null) throw new ArgumentNullException(nameof(updateDto));
            var name = updateDto.Name.Trim();
            if (await _categoryRepository.NameExistsAsync(name, updateDto.Id))
                throw new ArgumentException("A category with this name already exists");
            var entity = updateDto.ToEntity();
            entity.Name = name;
            return (await _categoryRepository.Update(entity)).ToResponse();
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            if (await _categoryRepository.HasDependentsAsync(id))
                throw new ArgumentException("A category with subcategories or listings cannot be deleted");
            var entity = await _categoryRepository.GetByIdAsync(id)
                ?? throw new ArgumentException("Category not found");
            return await _categoryRepository.Delete(entity);
        }
    }
}
