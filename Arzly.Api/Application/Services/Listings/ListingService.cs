using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.Activity;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Microsoft.AspNetCore.Http.HttpResults;
using SerilogTimings;
using System.Text.Json;
using Arzly.Api.Infrastructure.Storage;

namespace Arzly.Api.Application.Services.Listings
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepo;
        private readonly IPickupLocationRepository _pickupLocationRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IListingOwnedRepository _listingOwnedRepository;
        private readonly ILogger<ListingService> _logger;
        private readonly IUserActivityLogRepository _activityLogRepository;
        private readonly IImageUploader _imageUploader;


        public ListingService(IListingRepository repository, IPickupLocationRepository pickupLocationRepository
            , IListingOwnedRepository listingOwnedRepository, ILogger<ListingService> logger, ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository, JsonSerializerOptions jsonOptions,
            IUserActivityLogRepository activityLogRepository, IImageUploader imageUploader)
        {
            _listingRepo = repository;
            _pickupLocationRepository = pickupLocationRepository;
            _listingOwnedRepository = listingOwnedRepository;
            _logger = logger;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _jsonOptions = jsonOptions;
            _activityLogRepository = activityLogRepository;
            _imageUploader = imageUploader;
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

            var entity = updateDto.ToEntity();
            var updatedEntity = await _listingRepo
                .UpdateAdmin(entity);

            return updatedEntity.ToResponse();
        }

        public async Task<ListingResponse> GetByIdAdminAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(ExceptionMessages.MissingId);
            var entity = await _listingRepo.GetByIdAdminAsync(id)
                ?? throw new ArgumentException(ExceptionMessages.NoObjectWithId);
            return await AssignOneLocation_Details_Page(entity, entity.ToResponse());
        }

        public async Task<ListingResponse> SetStatusAdminAsync(Guid id, ListingStatus status, Guid actorId, string actorRole)
        {
            if (status == ListingStatus.Deleted)
                throw new ArgumentException("Use the delete operation for deleted listings");
            var entity = await _listingRepo.SetStatusAdminAsync(id, status)
                ?? throw new ArgumentException(ExceptionMessages.NoObjectWithId);
            await AddModerationAudit(entity.Id, actorId, actorRole,
                status == ListingStatus.Active ? ActivityActionType.ListingApproved : ActivityActionType.ListingUpdated,
                $"Status changed to {status}");
            return entity.ToResponse();
        }

        public async Task<ListingResponse> RejectAdminAsync(
            Guid id, string reason, Guid actorId, string actorRole)
        {
            if (string.IsNullOrWhiteSpace(reason) || reason.Trim().Length > 1000)
                throw new ArgumentException("A rejection reason between 1 and 1000 characters is required");
            var entity = await _listingRepo.RejectAdminAsync(id, reason.Trim())
                ?? throw new ArgumentException(ExceptionMessages.NoObjectWithId);
            await AddModerationAudit(entity.Id, actorId, actorRole,
                ActivityActionType.ListingRejected, reason.Trim());
            return entity.ToResponse();
        }

        public async Task DeleteAdminAsync(Guid id, Guid actorId, string actorRole)
        {
            if (!await _listingRepo.DeleteAdminAsync(id))
                throw new ArgumentException(ExceptionMessages.NoObjectWithId);
            await AddModerationAudit(id, actorId, actorRole,
                ActivityActionType.ListingModerationDeleted, "Listing deleted by moderation");
        }

        public async Task<ListingResponse> RestoreAdminAsync(Guid id, Guid actorId, string actorRole)
        {
            var entity = await _listingRepo.RestoreAdminAsync(id)
                ?? throw new ArgumentException(ExceptionMessages.NoObjectWithId);
            await AddModerationAudit(id, actorId, actorRole,
                ActivityActionType.ListingRestored, "Listing restored to pending review");
            return entity.ToResponse();
        }

        public async Task<List<UserActivityLogResponse>> GetModerationHistoryAsync(
            Guid id, int pageSize, int currentPage) =>
            (await _activityLogRepository.GetByTargetAsync(
                ActivityTargetType.Listing, id.ToString(), Math.Clamp(pageSize, 1, 100), Math.Max(currentPage, 0)))
            .Select(x => x.ToResponse()).ToList();

        private Task AddModerationAudit(
            Guid listingId, Guid actorId, string actorRole, ActivityActionType action, string details) =>
            _activityLogRepository.AddAsync(new UserActivityLog
            {
                ActorId = actorId, ActorRole = actorRole, ActionType = action,
                TargetType = ActivityTargetType.Listing, TargetId = listingId.ToString(),
                Details = details, Timestamp = DateTime.UtcNow, IsSuccess = true
            });

        public async Task<string?> GetTitleByIdAsync(Guid listingId)
        {
            if (listingId == Guid.Empty) return null;
            return await _listingRepo.GetTitleByIdAsync(listingId);
        }

        #endregion


        #region user 

        public async Task<ListingResponse?> GetByIdAsync(Guid id)
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

                return await AssignOneLocation_Details_Page(entity, entity.ToResponse());
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


        public async Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString, LocationPreset? preset, string order,
            string orderByPrice, double minPrice, double maxPrice, int pageSize, int currentPage)
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
                    nameof(ListingResponse.Title) => await _listingRepo.GetFilteredListing(
                        l => l.Title.Contains(searchString), pageSize, currentPage, preset, minPrice, maxPrice, order, orderByPrice),
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

        public async Task<List<string>> GetFilteredListingTitles(string searchString)
        {
            _logger.LogInformation($"{GetType().Name} - GetFilteredListingTitles Has been reached");

            if (string.IsNullOrWhiteSpace(searchString))
                return new List<string>();
            var listings = await _listingRepo
                .GetFilteredListingTitles(l => l.Title.Contains(searchString));
            return listings;
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

        public async Task<List<ListingResponse>> GetInitialListings(List<string> subcategoriesTitle, LocationPreset? location)
        {
            _logger.LogInformation("GetInitialListings started. Count: {Count}, Location: {Location}", subcategoriesTitle.Count, location);
            if (!subcategoriesTitle.Any())
            {
                _logger.LogError("GetInitialListings: empty subcategoriesTitle");
                throw new ArgumentNullException(ExceptionMessages.MissingCategoriesId);
            }

            List<Guid> subcategoryIds = [];
            foreach (string title in subcategoriesTitle)
            {
                var subcategory = await _subCategoryRepository.GetByTitleAsync(title);
                if (subcategory != null)
                {
                    subcategoryIds.Add(subcategory.Id);
                }
            }

            List<ListingResponse> responses = [];
            List<Listing> entities = [];
            using (Operation.Time("Fetched initial listings"))
            {
                foreach (Guid subcategoryId in subcategoryIds)
                {
                    _logger.LogDebug("Fetching listings for SubcategoryId: {SubcategoryId}, Location: {Location}", subcategoryId, location);
                    var items = await _listingRepo.GetInitialListings(subcategoryId, location);
                    entities.AddRange(items);
                }
                var response = entities
                    .Select(x => x.ToResponse())
                    .ToList();
                responses = await AssignLocation_Details(entities, response);
            }

            _logger.LogInformation("GetInitialListings completed. Count: {Count}", responses.Count);
            responses = responses.OrderByDescending(x => x.CreatedAt).ToList();

            return responses;
        }




        public async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto, Guid userId)
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

            if (requestLocation.UserId != userId)
                throw new UnauthorizedAccessException("The pickup location does not belong to the current user");


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



        public async Task<ListingResponse?> UpdateAsync(ListingUpdateRequest? updateDto, Guid userId)
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

            var existingListing = await _listingRepo.GetByIdAsync(updateDto.Id);
            if (existingListing is null)
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {updateDto.Id}");

            if (existingListing.OwnerId != userId)
                throw new UnauthorizedAccessException("The listing does not belong to the current user");

            var requestLocation = await _pickupLocationRepository
                .GetByIdAsync(updateDto.PickupLocationId);

            if (requestLocation is null)
            {
                _logger.LogError($"{GetType().Name} - Missing pickup location with id {{PickupLocationId}} in CreateAsync",
                    updateDto.PickupLocationId);
                throw new ArgumentNullException(ExceptionMessages.MissingPickUpLocation);
            }

            if (requestLocation.UserId != userId)
                throw new UnauthorizedAccessException("The pickup location does not belong to the current user");


            var entity = updateDto.ToEntity();

            var previousImageUrls = GetImageUrls(existingListing);
            var retainedImageUrls = GetImageUrls(entity);


            await _listingRepo.Update(entity);



            if (updateDto.ListingDetails.HasValue)
            {
                Type detailType = await GetDetailTypeFromCategoryId(updateDto.CategoryId);

                var details = updateDto
                    .ListingDetails
                    .Value.Deserialize(detailType, _jsonOptions);
                await _listingRepo.UpdateListingDetails(details!, entity.Id);
            }

            await DeleteRemovedImagesAsync(previousImageUrls.Except(retainedImageUrls), userId);


            return entity.ToResponse();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            var entity = await _listingRepo.GetByIdAsync(id);
            if (entity is null) return false;

            if (entity.OwnerId != userId)
                throw new UnauthorizedAccessException("The listing does not belong to the current user");

            return await _listingRepo.Delete(entity);
        }

        #endregion



        #region Mapping
        private static HashSet<string> GetImageUrls(Listing listing)
        {
            var urls = new HashSet<string>(StringComparer.Ordinal);
            if (!string.IsNullOrWhiteSpace(listing.PrimaryImageUrl))
                urls.Add(listing.PrimaryImageUrl);
            if (listing.ImagesUrl is not null)
                urls.UnionWith(listing.ImagesUrl.Where(url => !string.IsNullOrWhiteSpace(url)));
            return urls;
        }

        private async Task DeleteRemovedImagesAsync(IEnumerable<string> urls, Guid ownerId)
        {
            foreach (var url in urls)
            {
                try
                {
                    await _imageUploader.DeleteFile(ownerId.ToString(), url);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception,
                        "Listing update succeeded but removed image cleanup failed. ListingOwnerId: {OwnerId}",
                        ownerId);
                }
            }
        }




        #endregion

    }
}
