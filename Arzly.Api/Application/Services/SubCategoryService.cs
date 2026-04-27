using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Api.Application.Services
{
    public class SubCategoryService : BaseService<SubCategory, SubCategoryResponse, SubCategoryAddRequest, SubCategoryUpdateRequest, Guid>, ISubCategoryService
    {
        public SubCategoryService(ISubCategoryRepository repository) : base(repository)
        {
        }

        protected override SubCategoryResponse MapToDto(SubCategory entity) => entity.ToResponse();
        protected override SubCategory MapToEntity(SubCategoryAddRequest createDto) => createDto.ToEntity();
        protected override SubCategory MapToEntity(SubCategoryUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
