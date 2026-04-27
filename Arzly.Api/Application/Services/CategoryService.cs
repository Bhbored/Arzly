using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Application.Services
{
    public class CategoryService : BaseService<Category, CategoryResponse, CategoryAddRequest, CategoryUpdateRequest, Guid>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository) : base(repository)
        {
        }

        protected override CategoryResponse MapToDto(Category entity) => entity.ToResponse();
        protected override Category MapToEntity(CategoryAddRequest createDto) => createDto.ToEntity();
        protected override Category MapToEntity(CategoryUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
