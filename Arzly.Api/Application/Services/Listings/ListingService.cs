using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using SerilogTimings;
using System.Text.Json;

namespace Arzly.Api.Application.Services.Listings
{
    public class ListingService : BaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>, IListingService
    {
        private readonly IListingRepository _listingRepo;
        private readonly IPickupLocationRepository _pickupLocationRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IListingOwnedRepository _listingOwnedRepository;
        private readonly ILogger<ListingService> _logger;


        public ListingService(IListingRepository repository, IPickupLocationRepository pickupLocationRepository
            , IListingOwnedRepository listingOwnedRepository, ILogger<ListingService> logger, ICategoryRepository categoryRepository,
            JsonSerializerOptions jsonOptions)
            : base(repository)
        {
            _listingRepo = repository;
            _pickupLocationRepository = pickupLocationRepository;
            _listingOwnedRepository = listingOwnedRepository;
            _logger = logger;
            _categoryRepository = categoryRepository;
            _jsonOptions = jsonOptions;
        }


        #region helpers
        public async Task<List<ListingResponse>> AssignLocation_Details(List<Listing> entities, List<ListingResponse> responses)
        {

            var listingIds = entities.Select(x => x.Id).ToList();
            var details = await _listingOwnedRepository.GetByListingIds(listingIds);

            for (int i = 0; i < responses.Count; i++)
            {
                responses[i].PickupLocation = entities[i].PickupLocation.ToResponse();

                var listingId = responses[i].Id;
                if (details.TryGetValue(listingId, out var detail))
                    responses[i].ListingDetails = detail;
            }

            return responses;

        }
        public async Task<ListingResponse> AssignOneLocation_Details_Page(Listing entitie, ListingResponse response)
        {


            PickupLocation pickupLocation = entitie.PickupLocation;

            response.PickupLocation = pickupLocation.ToResponse();

            response.ListingDetails = await _listingOwnedRepository
                .GetByListingId(response.Id);
            return response ?? new();
        }


        public async Task<Type> GetDetailTypeFromCategoryId(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                _logger.LogError($"Failed to fetched category with id : {categoryId}");
                throw new ArgumentException($"{ExceptionMessages.NoCategoryWithId} - {categoryId}");

            }

            return category.Name switch
            {
                "Vehicles" => typeof(VehiclesDetails),
                "Real Estate" => typeof(RealEstateDetails),
                "Phones & Gadgets" => typeof(PhonesDetails),
                "Electronics & Appliances" => typeof(ElectronicsDetails),
                "Furniture & Decor" => typeof(FurnitureDetails),
                "Pets" => typeof(PetsDetails),
                "Kids & Babies" => typeof(BabyChildDetails),
                "Sports & Equipment" => typeof(SportsDetails),
                "Hobbies" => typeof(HobbiesDetails),
                "Fashion & Style" => typeof(FashionDetails),
                "Services" => typeof(ServicesDetails),
                _ => throw new ArgumentException($"No detail type for category: {category.Name}")
            };
        }

        #endregion


        #region admin & support



        public async Task<List<ListingResponse>> GetAllListingAdmin(int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetAllAsync Has been reached");

            List<ListingResponse> responses = [];
            using (Operation.Time("Time for Fetched All Listings with location & details from Database"))
            {
                var entities = await _listingRepo.GetAllListingAdmin(pageSize, currentPage);

                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            return responses;
        }

        public async Task<ListingResponse?> UpdateAsyncAdmin(ListingUpdateRequest? updateDto)
        {
            if (updateDto == null)
                throw new ArgumentNullException(ExceptionMessages.EmptyUpdateRequest);

            var entity = MapToEntity(updateDto);
            var updatedEntity = await _listingRepo
                .UpdateAdmin(entity);

            return MapToDto(updatedEntity);
        }

        #endregion


        #region user 

        public override async Task<ListingResponse?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation($"{GetType().Name} - GetByIdAsync Has been reached");

            if (id == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - Empty id provided in GetByIdAsync");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            using (Operation.Time("Time for Fetched Listings ById with location & details from Database"))
            {
                var entity = await _listingRepo.GetByIdAsync(id);
                if (entity is null)
                {
                    _logger.LogError($"{GetType().Name} - No Listing found with id {{Id}} in GetByIdAsync", id);
                    throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
                }

                return await AssignOneLocation_Details_Page(entity, MapToDto(entity));
            }

        }


        public async Task<List<ListingResponse>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, string order, string orderByPrice, double minPrice, double maxPrice)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingByCategoryId Has been reached");
            if (categoryId == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - GetListingByCategoryId No id was provided  {nameof(categoryId)}");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            using (Operation.Time("Time for Fetched Listings ByCategoryId with location & details from Database"))
            {
                List<ListingResponse> responses = [];
                var entities = await _listingRepo.GetListingByCategoryId(categoryId, pageSize, currentPage, searchString, preset,
                    minPrice, maxPrice, order, orderByPrice);
                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);

                return responses;
            }

        }


        public async Task<List<ListingResponse>> GetListingBySubCategoryId(Guid subcategoryId, Guid categoryId, int pageSize, int currentPage,
           string? searchString, LocationPreset? preset, object? details, string order, string orderByPrice, double minPrice, double maxPrice)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingBySubCategoryId Has been reached");
            if (categoryId == Guid.Empty || subcategoryId == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - GetListingBySubCategoryId No id was provided  {nameof(categoryId)}");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            using (Operation.Time("Time for Fetched Listings BySubcategoryId with location & details from Database"))
            {
                List<ListingResponse> responses = [];
                List<Listing>? entities = [];
                if (details != null)
                {
                    Type detailType = await GetDetailTypeFromCategoryId(categoryId);

                    var jsonString = JsonSerializer.Serialize(details, _jsonOptions);

                    var serializedDetails = JsonSerializer.
                        Deserialize(jsonString, detailType, _jsonOptions);

                    entities = await _listingRepo.GetListingBySubCategoryId(subcategoryId, pageSize, currentPage, searchString, preset,
                        serializedDetails, minPrice, maxPrice, order, orderByPrice);

                }
                else
                {
                    entities = await _listingRepo.GetListingBySubCategoryId(subcategoryId, pageSize, currentPage,
                        searchString, preset, null, minPrice, maxPrice, order, orderByPrice);

                }

                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);

                return responses;
            }
        }


        public async Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetFilteredListing Has been reached");

            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchString))
                return new List<ListingResponse>();
            List<Listing> listings = [];
            List<ListingResponse> responses = [];
            using (Operation.Time("Time for Fetched filtered Listings with location & details from Database"))
            {

                listings = searchBy switch
                {
                    nameof(ListingResponse.Title) => await _listingRepo.GetFilteredListing(l => l.Title.Contains(searchString), pageSize, currentPage),
                    //more cases to come
                    _ => await _listingRepo.GetIndexedListings(pageSize, currentPage)
                };
                var response = listings
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(listings, response);
            }

            return responses;

        }

        public async Task<List<ListingResponse>> GetListingByUserId(Guid? userId, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingByUserId Has been reached");

            List<ListingResponse> responses = [];
            using (Operation.Time("Time for Fetched Listings ByUserId with location & details from Database"))
            {
                var entities = await _listingRepo.GetListingByUserId(userId ?? Guid.Empty, pageSize, currentPage);

                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            return responses;

        }

        public async Task<List<ListingResponse>> GetIndexedListings(int pageSzie, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetIndexedListings Has been reached");

            List<ListingResponse> responses = [];
            using (Operation.Time("Time for Fetched indexed Listings with location & details from Database"))
            {
                var entities = await _listingRepo.GetIndexedListings(pageSzie, currentPage);

                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }
            return responses;
        }

        public async Task<List<ListingResponse>> GetInitialListings(List<Guid> subcategoryIds)
        {
            _logger.LogInformation($"{GetType().Name} - GetInitialListings Has been reached");
            if (!subcategoryIds.Any())
            {
                _logger.LogError($"{GetType().Name} - Empty categoryNames provided in GetInitialListings");
                throw new ArgumentNullException(ExceptionMessages.MissingCategoriesId);
            }
            List<ListingResponse> responses = [];
            List<Listing> entities = [];
            using (Operation.Time("Time for Fetched initial Listings with location & details from Database"))
            {

                foreach (Guid subcategoryId in subcategoryIds)
                {
                    var items = await _listingRepo.GetInitialListings(subcategoryId);
                    entities.AddRange(items);
                }
                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            responses = responses.OrderByDescending(x => x.CreatedAt).ToList();

            return responses;

        }




        public override async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto, Guid userId)
        {
            _logger.LogInformation($"{GetType().Name} - CreateAsync Has been reached");

            if (createDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty createDto provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            if (createDto.ListingDetails is null)
            {
                _logger.LogError($"{GetType().Name} - Empty Listing Details provided in CreateAsync");
                throw new ArgumentException(ExceptionMessages.NoAttachedDetails);
            }

            var requestLocation = await _pickupLocationRepository
                .GetByIdAsync(createDto.PickupLocationId);

            if (requestLocation is null)
            {
                _logger.LogError($"{GetType().Name} - Missing pickup location with id {{PickupLocationId}} in CreateAsync",
                    createDto.PickupLocationId);
                throw new ArgumentNullException(ExceptionMessages.MissingPickUpLocation);
            }


            var entity = createDto.ToEntity();
            entity.Id = Guid.NewGuid();

            entity.OwnerId = userId;
           
            await _listingRepo.AddAsync(entity);



            if (createDto.ListingDetails.HasValue)
            {
                Type detailType = await GetDetailTypeFromCategoryId(createDto.CategoryId);

                var details = createDto
                    .ListingDetails
                    .Value.Deserialize(detailType, _jsonOptions);
                await _listingRepo.AddListingDetails(details!, entity.Id);
            }


            return entity.ToResponse();
        }



        public async override Task<ListingResponse?> UpdateAsync(ListingUpdateRequest? updateDto, Guid userId)
        {
            _logger.LogInformation($"{GetType().Name} - UpdateAsync Has been reached");

            if (updateDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty UpdateAsync provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            if (updateDto.ListingDetails is null)
            {
                _logger.LogError($"{GetType().Name} - Empty Listing Details provided in CreateAsync");
                throw new ArgumentException(ExceptionMessages.NoAttachedDetails);
            }

            var requestLocation = await _pickupLocationRepository
                .GetByIdAsync(updateDto.PickupLocationId);

            if (requestLocation is null)
            {
                _logger.LogError($"{GetType().Name} - Missing pickup location with id {{PickupLocationId}} in CreateAsync",
                    updateDto.PickupLocationId);
                throw new ArgumentNullException(ExceptionMessages.MissingPickUpLocation);
            }


            var entity = updateDto.ToEntity();


            await _listingRepo.Update(entity);



            if (updateDto.ListingDetails.HasValue)
            {
                Type detailType = await GetDetailTypeFromCategoryId(updateDto.CategoryId);

                var details = updateDto
                    .ListingDetails
                    .Value.Deserialize(detailType, _jsonOptions);
                await _listingRepo.UpdateListingDetails(details!, entity.Id);
            }


            return entity.ToResponse();
        }

        public async override Task<bool> DeleteAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            if (id is Guid guid && guid == Guid.Empty)
            {
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            var entity = await _listingRepo.GetByIdAsync(id);
            if (entity == null) return false;

            return await _listingRepo.Delete(entity);
        }

        #endregion



        #region Mapping
        protected override ListingResponse MapToDto(Listing entity) =>
            entity.ToResponse();

        protected override Listing MapToEntity(ListingAddRequest createDto) =>
            createDto.ToEntity();

        protected override Listing MapToEntity(ListingUpdateRequest updateDto) => updateDto.ToEntity();




        #endregion

    }
}
