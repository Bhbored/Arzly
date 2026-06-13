using Arzly.Api.Application.Contracts.Categories;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;
using SerilogTimings;

namespace Arzly.Api.Application.Services.Categories
{
    public class SubCategoryService : BaseService<SubCategory, SubCategoryResponse, SubCategoryAddRequest, SubCategoryUpdateRequest, Guid>,
        ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<SubCategoryService> _logger;

        public SubCategoryService(ISubCategoryRepository repository, ICategoryRepository categoryRepository, ILogger<SubCategoryService> logger) : base(repository)
        {
            _subCategoryRepository = repository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<List<SubCategoryResponse>> GetByCategoryIdAsync(Guid categoryId)
        {
            _logger.LogInformation($"{GetType().Name} - GetByCategoryIdAsync has been reached");

            if (categoryId == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - Empty categoryId provided in GetByCategoryIdAsync");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            if (await _categoryRepository.GetByIdAsync(categoryId) == null)
            {
                _logger.LogError($"{GetType().Name} - No category found with id {{CategoryId}} in GetByCategoryIdAsync", categoryId);
                throw new ArgumentException(ExceptionMessages.NoCategoryWithId);
            }

            List<SubCategoryResponse> responses;
            using (Operation.Time("Time for Fetched SubCategories by category id from Database"))
            {
                var entities = await _subCategoryRepository.GetByCategoryIdAsync(categoryId);
                responses = entities.ConvertAll(x => MapToDto(x));
            }

            _logger.LogInformation($"{GetType().Name} - GetByCategoryIdAsync successfully returned {responses.Count} subcategories");
            return responses;
        }

        public async Task<SubCategoryResponse?> GetByTitleAsync(string title)
        {
            _logger.LogInformation($"{GetType().Name} - GetByTitleAsync has been reached");

            if (string.IsNullOrWhiteSpace(title))
            {
                _logger.LogError($"{GetType().Name} - Empty title provided in GetByTitleAsync");
                throw new ArgumentNullException(ExceptionMessages.MissingCategoriesId);
            }

            SubCategoryResponse? response;
            using (Operation.Time("Time for Fetched SubCategory by title from Database"))
            {
                var entity = await _subCategoryRepository.GetByTitleAsync(title);
                response = entity == null ? null : MapToDto(entity);
            }

            _logger.LogInformation($"{GetType().Name} - GetByTitleAsync returned {{Found}}", response != null);
            return response;
        }

        protected override SubCategoryResponse MapToDto(SubCategory entity) => entity.ToResponse();
        protected override SubCategory MapToEntity(SubCategoryAddRequest createDto) => createDto.ToEntity();
        protected override SubCategory MapToEntity(SubCategoryUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
