using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Azure.Core;
using SerilogTimings;
using System.Reflection;
using System.Text.Json;

namespace Arzly.Api.Application.Services
{
    public class ListingService : BaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>, IListingService
    {
        private readonly IListingRepository _listingRepo;
        private readonly IPickupLocationRepository _pickupLocationRepository;
        private readonly IUserService _userService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IListingOwnedRepository _listingOwnedRepository;
        private readonly ILogger<ListingService> _logger;


        public ListingService(IListingRepository repository, IPickupLocationRepository pickupLocationRepository, IUserService userService
            , IListingOwnedRepository listingOwnedRepository, ILogger<ListingService> logger, ICategoryRepository categoryRepository,
            JsonSerializerOptions jsonOptions)
            : base(repository)
        {
            _listingRepo = repository;
            _pickupLocationRepository = pickupLocationRepository;
            _userService = userService;
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


        #region fetch

        public override async Task<List<ListingResponse>> GetAllAsync()
        {
            _logger.LogInformation($"{GetType().Name} - GetAllAsync Has been reached");

            List<ListingResponse> responses = new();
            using (Operation.Time("Time for Fetched All Listings with location & details from Database"))
            {
                var entities = await _listingRepo.GetAllAsync();

                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            return responses;

        }

        public override async Task<ListingResponse?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation($"{GetType().Name} - GetByIdAsync Has been reached");

            if (id == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - Empty id provided in GetByIdAsync");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _listingRepo.GetByIdAsync(id);
            if (entity is null)
            {
                _logger.LogError($"{GetType().Name} - No Listing found with id {{Id}} in GetByIdAsync", id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            return await AssignOneLocation_Details_Page(entity, MapToDto(entity));
        }

        public async Task<List<ListingResponse>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingByCategoryId Has been reached");
            if (categoryId == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - GetListingByCategoryId No id was provided  {nameof(categoryId)}");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            List<ListingResponse> responses = new();
            var entities = await _listingRepo.GetListingByCategoryId(categoryId, pageSize, currentPage);
            var response = entities
                .Select(x => x.ToResponse())
                .ToList();
            responses = await AssignLocation_Details(entities, responses);
            return responses;
        }

        public async Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetFilteredListing Has been reached");

            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchString))
                return new List<ListingResponse>();
            List<Listing> listings = new();
            List<ListingResponse> responses = new();
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

        public async Task<List<ListingResponse>> GetListingByUserId(string? userId, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingByUserId Has been reached");

            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogError($"{GetType().Name} - Empty userId provided in GetListingByUserId");
                throw new ArgumentNullException(ExceptionMessages.MissingFirebaseId);
            }
            //this will change when identity is integrated
            var user = await _userService
                            .GetByFireBaseIdAsync(userId);
            List<ListingResponse> responses = new();
            using (Operation.Time("Time for Fetched Listings ByUserId with location & details from Database"))
            {
                var entities = await _listingRepo.GetListingByUserId(user.Id, pageSize, currentPage);

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

            List<ListingResponse> responses = new();
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

        public async Task<List<ListingResponse>> GetInitialListings(List<Guid> categoriesId)
        {
            _logger.LogInformation($"{GetType().Name} - GetInitialListings Has been reached");
            if (!categoriesId.Any())
            {
                _logger.LogError($"{GetType().Name} - Empty categoryNames provided in GetInitialListings");
                throw new ArgumentNullException(ExceptionMessages.MissingCategoriesId);
            }
            List<ListingResponse> responses = new();
            List<Listing> entities = new();
            using (Operation.Time("Time for Fetched initial Listings with location & details from Database"))
            {

                foreach (Guid categoryId in categoriesId)
                {
                    var items = await _listingRepo.GetInitialListings(categoryId);
                    entities.AddRange(items);
                }
                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            return responses;

        }


        #endregion


        #region create

        public override async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto, string? userId)
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
            //identity affected later

            var dbuser = await _userService.GetByFireBaseIdAsync(userId);
            //if it's null it will already throw error no need for repeat logic

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

            entity.OwnerId = dbuser!.Id;
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
