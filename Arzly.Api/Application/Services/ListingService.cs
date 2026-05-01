using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
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

        public ListingService(IListingRepository repository, IPickupLocationRepository pickupLocationRepository) : base(repository)
        {
            _listingRepo = repository;
            _pickupLocationRepository = pickupLocationRepository;

        }



        public async Task<List<ListingResponse>> AssignPickupLocation(List<Listing> entities, List<ListingResponse> responses)
        {
            List<PickupLocation> pickupLocations = new();

            entities
                .ForEach(x => pickupLocations.Add(x.PickupLocation));
            for (int i = 0; i < pickupLocations.Count; i++)
            {
                responses[i].PickupLocation = pickupLocations[i].ToResponse();
            }
            return responses ?? [];
        }
        public async Task<ListingResponse> AssignOnePickupLocation(Listing entitie, ListingResponse response)
        {
            PickupLocation pickupLocation = entitie.PickupLocation;

            response.PickupLocation = pickupLocation.ToResponse();

            return response ?? new();
        }

        #region fetch

        public override async Task<List<ListingResponse>> GetAllAsync()
        {
            var entities = await _listingRepo.GetAllAsync();

            var responses = entities
                .Select(x => x.ToResponse())
                .ToList();
            return await AssignPickupLocation(entities, responses);

        }

        public override async Task<ListingResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            var entity = await _listingRepo.GetByIdAsync(id);
            if (entity is null)
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");

            return await AssignOnePickupLocation(entity,MapToDto(entity));
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
            return await AssignPickupLocation(listings, response);

        }

        public async Task<List<ListingResponse>> GetListingByUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException($"User id {nameof(userId)} wasn't provided");
            //later add to find user is found logic before u continue so user fetching
            var entities = await _listingRepo.GetListingByUserId(userId);

            var responses = entities
                .Select(x => x.ToResponse())
                .ToList();

            return await AssignPickupLocation(entities, responses);

        }

        #endregion




        public override async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto)
        {
            if (createDto is null)
                throw new ArgumentException(ExceptionMessages.EmptyAddRequest);

            var requestLocation = await _pickupLocationRepository
                .GetByIdAsync(createDto.PickupLocationId);

            if (requestLocation is null)
                throw new ArgumentNullException(ExceptionMessages.MissingPickUpLocation);

            var entity = createDto.ToEntity();
            entity.Id = Guid.NewGuid();
            entity.OwnerId = "user-2-id";//temp
            await _listingRepo.AddAsync(entity);

            return entity.ToResponse();
        }


        #region Mapping
        protected override ListingResponse MapToDto(Listing entity) =>
            entity.ToResponse();

        protected override Listing MapToEntity(ListingAddRequest createDto) =>
            createDto.ToEntity();

        protected override Listing MapToEntity(ListingUpdateRequest updateDto) => updateDto.ToEntity();



        #endregion

    }
}
