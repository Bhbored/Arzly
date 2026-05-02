using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Azure;

namespace Arzly.Api.Application.Services
{
    public class ListingService : BaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>, IListingService
    {
        private readonly IListingRepository _listingRepo;
        private readonly IPickupLocationRepository _pickupLocationRepository;
        private readonly IUserService _userService;
        private readonly IListingOwnedRepository _listingOwnedRepository;
        public ListingService(IListingRepository repository, IPickupLocationRepository pickupLocationRepository, IUserService userService
            , IListingOwnedRepository listingOwnedRepository)
            : base(repository)
        {
            _listingRepo = repository;
            _pickupLocationRepository = pickupLocationRepository;
            _userService = userService;
            _listingOwnedRepository = listingOwnedRepository;

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
        public async Task<ListingResponse> AssignOneLocation_Details(Listing entitie, ListingResponse response)
        {
            PickupLocation pickupLocation = entitie.PickupLocation;

            response.PickupLocation = pickupLocation.ToResponse();

            response.ListingDetails = await _listingOwnedRepository
                .GetByListingId(response.Id);
            return response ?? new();
        }

        #endregion


        #region fetch

        public override async Task<List<ListingResponse>> GetAllAsync()
        {
            var entities = await _listingRepo.GetAllAsync();

            var responses = entities
                .Select(x => x.ToResponse())
                .ToList();
            return await AssignLocation_Details(entities, responses);

        }

        public override async Task<ListingResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            var entity = await _listingRepo.GetByIdAsync(id);
            if (entity is null)
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");

            return await AssignOneLocation_Details(entity, MapToDto(entity));
        }
        public Task<List<ListingResponse>> GetListingByCategoryId(Guid? categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString)
        {

            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchString))
                return new List<ListingResponse>();
            List<Listing> listings = new();
            listings = searchBy switch
            {
                nameof(ListingResponse.Title) => await _listingRepo.GetFilteredListing(l => l.Title.Contains(searchString)),
                //more cases to come
                _ => await _listingRepo.GetAllAsync()
            };
            var response = listings
                .Select(x => x.ToResponse())
                .ToList();
            return await AssignLocation_Details(listings, response);

        }

        public async Task<List<ListingResponse>> GetListingByUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(ExceptionMessages.MissingFirebaseId);

            await _userService
                          .GetByFireBaseIdAsync(userId);

            var entities = await _listingRepo.GetListingByUserId(userId);

            var responses = entities
                .Select(x => x.ToResponse())
                .ToList();

            return await AssignLocation_Details(entities, responses);

        }

        public async Task<List<ListingResponse>> GetIndexedListings(int pageSzie = 10, int currentPage = 0)
        {
            var entities = await _listingRepo.GetIndexedListings(pageSzie, currentPage);

            var responses = entities
                .Select(x => x.ToResponse())
                .ToList();

            return await AssignLocation_Details(entities, responses);
        }

        #endregion


        #region create

        public override async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto, string? userId)
        {
            if (createDto is null)
                throw new ArgumentException(ExceptionMessages.EmptyAddRequest);

            var dbuser = await _userService.GetByFireBaseIdAsync(userId);

            var requestLocation = await _pickupLocationRepository
                .GetByIdAsync(createDto.PickupLocationId);

            if (requestLocation is null)
                throw new ArgumentNullException(ExceptionMessages.MissingPickUpLocation);

            var entity = createDto.ToEntity();
            entity.Id = Guid.NewGuid();

            entity.OwnerId = dbuser!.Id;
            await _listingRepo.AddAsync(entity);

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
