using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Arzly.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ItemsCount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthMethod = table.Column<int>(type: "int", nullable: false),
                    FirebaseUid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PublicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublicLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActiveAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    BanReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BanExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ItemsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BaseLocation = table.Column<int>(type: "int", nullable: false),
                    lon = table.Column<double>(type: "float", nullable: true),
                    lat = table.Column<double>(type: "float", nullable: true),
                    LocationTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactMethod = table.Column<int>(type: "int", nullable: true),
                    JobField = table.Column<int>(type: "int", nullable: true),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: true),
                    EducationLevel = table.Column<int>(type: "int", nullable: true),
                    EmploymentType = table.Column<int>(type: "int", nullable: true),
                    WorkplaceType = table.Column<int>(type: "int", nullable: true),
                    SalaryMin = table.Column<double>(type: "float", nullable: true),
                    SalaryMax = table.Column<double>(type: "float", nullable: true),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPromoted = table.Column<bool>(type: "bit", nullable: false),
                    PromotionType = table.Column<int>(type: "int", nullable: true),
                    PromotionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromotionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobListings_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PickupLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Label = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupLocations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SearchedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchQueries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    TargetType = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationMs = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivityLogs_Users_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Theme = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    PushNotifications = table.Column<bool>(type: "bit", nullable: false),
                    EmailNotifications = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PrimaryImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsPriceNegotiable = table.Column<bool>(type: "bit", nullable: false),
                    IsPromoted = table.Column<bool>(type: "bit", nullable: false),
                    PromotionType = table.Column<int>(type: "int", nullable: true),
                    PromotionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromotionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactMethod = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_PickupLocations_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "PickupLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_SubCategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyChildDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgeRange = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    StrollerSeatType = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    CribFurnitureType = table.Column<int>(type: "int", nullable: true),
                    FeedingType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyChildDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_BabyChildDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContextRole = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDiscontinued = table.Column<bool>(type: "bit", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InitiatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_JobListings_JobListingId",
                        column: x => x.JobListingId,
                        principalTable: "JobListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicsDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TVBrand = table.Column<int>(type: "int", nullable: true),
                    TVType = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    ScreenSize = table.Column<int>(type: "int", nullable: true),
                    DisplayTechnology = table.Column<int>(type: "int", nullable: true),
                    AudioBrand = table.Column<int>(type: "int", nullable: true),
                    KitchenApplianceType = table.Column<int>(type: "int", nullable: true),
                    CoolingHeatingType = table.Column<int>(type: "int", nullable: true),
                    CleaningApplianceType = table.Column<int>(type: "int", nullable: true),
                    WashingMachineBrand = table.Column<int>(type: "int", nullable: true),
                    ComputerBrand = table.Column<int>(type: "int", nullable: true),
                    ComputerType = table.Column<int>(type: "int", nullable: true),
                    Processor = table.Column<int>(type: "int", nullable: true),
                    RamSize = table.Column<int>(type: "int", nullable: true),
                    ComputerScreenSize = table.Column<int>(type: "int", nullable: true),
                    ComputerStorage = table.Column<int>(type: "int", nullable: true),
                    StorageType = table.Column<int>(type: "int", nullable: true),
                    ComputerColor = table.Column<int>(type: "int", nullable: true),
                    ComputerAccessoryType = table.Column<int>(type: "int", nullable: true),
                    CameraBrand = table.Column<int>(type: "int", nullable: true),
                    CameraType = table.Column<int>(type: "int", nullable: true),
                    GamingBrand = table.Column<int>(type: "int", nullable: true),
                    GamingType = table.Column<int>(type: "int", nullable: true),
                    CompatibleConsole = table.Column<int>(type: "int", nullable: true),
                    GameCondition = table.Column<int>(type: "int", nullable: true),
                    GameGenre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicsDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_ElectronicsDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FashionDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MensClothingType = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    MensAccessoryType = table.Column<int>(type: "int", nullable: true),
                    WomensClothingType = table.Column<int>(type: "int", nullable: true),
                    WomensAccessoryType = table.Column<int>(type: "int", nullable: true),
                    CosmeticType = table.Column<int>(type: "int", nullable: true),
                    JewelryType = table.Column<int>(type: "int", nullable: true),
                    JewelryMaterial = table.Column<int>(type: "int", nullable: true),
                    WatchGender = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FashionDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_FashionDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LivingRoomType = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    BedroomType = table.Column<int>(type: "int", nullable: true),
                    DiningRoomType = table.Column<int>(type: "int", nullable: true),
                    KitchenwareType = table.Column<int>(type: "int", nullable: true),
                    BathroomType = table.Column<int>(type: "int", nullable: true),
                    HomeDecorType = table.Column<int>(type: "int", nullable: true),
                    GardenType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_FurnitureDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HobbiesDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectibleType = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    InstrumentType = table.Column<int>(type: "int", nullable: true),
                    BookType = table.Column<int>(type: "int", nullable: true),
                    BookLanguage = table.Column<int>(type: "int", nullable: true),
                    MovieGenre = table.Column<int>(type: "int", nullable: true),
                    HobbyGameType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbiesDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_HobbiesDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetsDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetFoodType = table.Column<int>(type: "int", nullable: true),
                    PetToyType = table.Column<int>(type: "int", nullable: true),
                    GroomingType = table.Column<int>(type: "int", nullable: true),
                    PetAccessoryType = table.Column<int>(type: "int", nullable: true),
                    DogBreed = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DogAgeRange = table.Column<int>(type: "int", nullable: true),
                    IsVaccinated = table.Column<bool>(type: "bit", nullable: true),
                    CatBreed = table.Column<int>(type: "int", nullable: true),
                    CatAgeRange = table.Column<int>(type: "int", nullable: true),
                    BirdSpecies = table.Column<int>(type: "int", nullable: true),
                    BirdAgeGroup = table.Column<int>(type: "int", nullable: true),
                    AnimalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetServiceType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetsDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_PetsDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhonesDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneBrand = table.Column<int>(type: "int", nullable: true),
                    PhoneCondition = table.Column<int>(type: "int", nullable: true),
                    Storage = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: true),
                    AccessoryBrand = table.Column<int>(type: "int", nullable: true),
                    AccessoryItemType = table.Column<int>(type: "int", nullable: true),
                    Operator = table.Column<int>(type: "int", nullable: true),
                    MembershipType = table.Column<int>(type: "int", nullable: true),
                    WatchBrand = table.Column<int>(type: "int", nullable: true),
                    WatchStorage = table.Column<int>(type: "int", nullable: true),
                    BandMaterial = table.Column<int>(type: "int", nullable: true),
                    BandColor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhonesDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_PhonesDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingType = table.Column<int>(type: "int", nullable: true),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: true),
                    Ownership = table.Column<int>(type: "int", nullable: true),
                    Bedrooms = table.Column<int>(type: "int", nullable: true),
                    Bathrooms = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Furnished = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyAge = table.Column<int>(type: "int", nullable: true),
                    CommercialType = table.Column<int>(type: "int", nullable: true),
                    Equipped = table.Column<bool>(type: "bit", nullable: true),
                    CommercialFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandType = table.Column<int>(type: "int", nullable: true),
                    ChaletType = table.Column<int>(type: "int", nullable: true),
                    ChaletFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomFurnished = table.Column<bool>(type: "bit", nullable: true),
                    RoomFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_RealEstateDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedListings_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedListings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: true),
                    HomeServiceType = table.Column<int>(type: "int", nullable: true),
                    PersonalServiceType = table.Column<int>(type: "int", nullable: true),
                    ProfessionalServiceType = table.Column<int>(type: "int", nullable: true),
                    EventServiceType = table.Column<int>(type: "int", nullable: true),
                    TransportServiceType = table.Column<int>(type: "int", nullable: true),
                    OtherServiceType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_ServicesDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BicycleType = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    BicyclePowerType = table.Column<int>(type: "int", nullable: true),
                    OutdoorType = table.Column<int>(type: "int", nullable: true),
                    GymType = table.Column<int>(type: "int", nullable: true),
                    BallSportType = table.Column<int>(type: "int", nullable: true),
                    SupplementType = table.Column<int>(type: "int", nullable: true),
                    SupplementBrand = table.Column<int>(type: "int", nullable: true),
                    GameRoomType = table.Column<int>(type: "int", nullable: true),
                    WinterSportType = table.Column<int>(type: "int", nullable: true),
                    WaterSportType = table.Column<int>(type: "int", nullable: true),
                    RacketSportType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_SportsDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesDetails",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarBrand = table.Column<int>(type: "int", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Kilometers = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: true),
                    VehicleColor = table.Column<int>(type: "int", nullable: true),
                    NumberOfDoors = table.Column<int>(type: "int", nullable: true),
                    TransmissionType = table.Column<int>(type: "int", nullable: true),
                    CarFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessoryType = table.Column<int>(type: "int", nullable: true),
                    MotorcycleType = table.Column<int>(type: "int", nullable: true),
                    VehicleType = table.Column<int>(type: "int", nullable: true),
                    NumberOfDigits = table.Column<int>(type: "int", nullable: true),
                    TruckBrand = table.Column<int>(type: "int", nullable: true),
                    BoatType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesDetails", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_VehiclesDetails_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReports_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReports_Users_ReportedUserId",
                        column: x => x.ReportedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserReports_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_Users_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsInternalNote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImageUrl", "ItemsCount", "Name" },
                values: new object[,]
                {
                    { new Guid("031f53da-215e-4361-b068-f8b960515a21"), "Books, music, art, collectibles and musical instruments", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/hobbies.png", 0, "Hobbies" },
                    { new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), "Clothing, shoes, bags, jewelry and cosmetics", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/fshion%26style.png", 0, "Fashion & Style" },
                    { new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), "Cars, motorcycles, boats, trucks and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/vehicles.png", 0, "Vehicles" },
                    { new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), "Home repair, cleaning, tutoring, moving and more", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/services.png", 0, "Services" },
                    { new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), "TVs, laptops, cameras, kitchen and home appliances", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/electronics%26appliances.png", 0, "Electronics & Appliances" },
                    { new Guid("936fa3de-1369-44fd-8fca-a15aa46dc254"), "Smartphones, tablets, watches and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/phones%26gadgets.png", 0, "Phones & Gadgets" },
                    { new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), "Dogs, cats, birds, fish and pet supplies", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/pets.png", 0, "Pets" },
                    { new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), "Home and office furniture, lighting, rugs and decor", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/furniture%26decor.png", 0, "Furniture & Decor" },
                    { new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), "Apartments, villas, land and commercial properties", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/real-estate.png", 0, "Real Estate" },
                    { new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), "Gym equipment, bicycles, camping and fitness gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/sports%26equipment.png", 0, "Sports & Equipment" },
                    { new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), "Toys, strollers, clothing and baby gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/kids%26babies.png", 0, "Kids & Babies" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthMethod", "BanExpiresAt", "BanReason", "CreatedAt", "DeletedAt", "Email", "FirebaseUid", "IsBanned", "IsDeleted", "IsVerified", "LastActiveAt", "ProfileImageUrl", "PublicLocation", "PublicName", "UserName" },
                values: new object[,]
                {
                    { "user-1-id", 0, null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), null, "john@example.com", "firebase-uid-123", false, false, false, new DateTime(2025, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://example.com/profiles/john.jpg", "New York, USA", "John Doe", "john_doe" },
                    { "user-2-id", 0, null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), null, "bhbored2022@gmail.com", "firebase-uid-124", false, false, false, new DateTime(2025, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc), "https://example.com/profiles/john.jpg", "New York, USA", "John Doe", "bourhan-hassoun" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ItemsCount", "Name" },
                values: new object[,]
                {
                    { new Guid("01315faf-3c3b-451c-b91b-078badd79134"), new Guid("936fa3de-1369-44fd-8fca-a15aa46dc254"), null, 0, "Smart Watches" },
                    { new Guid("0b33477b-27fb-4472-8296-373a99cf9cae"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("0e3e3e67-0c8d-4765-b376-12927c9073bd"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("11361ac0-6f45-4257-b287-27808831518e"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Accessories For Women" },
                    { new Guid("12656cb4-803d-408f-ad68-3d9afd2be3ed"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Musical Instruments" },
                    { new Guid("12a6d04a-c8f6-4759-a3d6-319ede14fd01"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Number Plates" },
                    { new Guid("134cf286-4217-4cbf-bd12-7a8813c69b61"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Houses For Rent" },
                    { new Guid("184d500f-106e-471b-9b52-928f8033d2d3"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Games & Hobbies" },
                    { new Guid("18d4fa52-37b9-4ec5-b66b-43263b549552"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Clothing For Women" },
                    { new Guid("1f2ac968-e79c-40e7-8b4d-a806035ee9c7"), new Guid("936fa3de-1369-44fd-8fca-a15aa46dc254"), null, 0, "Mobile Numbers" },
                    { new Guid("2161b541-1769-4d47-b3ad-db44d5f97509"), new Guid("936fa3de-1369-44fd-8fca-a15aa46dc254"), null, 0, "Mobile Phones" },
                    { new Guid("22b2a1c3-782b-41f7-9694-d8ce99ce372e"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Clothing For Men" },
                    { new Guid("241c8daa-af44-4ee9-ac92-53d3a87f33a9"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Other Sports" },
                    { new Guid("2857fdf3-43e3-419a-a6e2-bb95b6d04c56"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "AC Cooling & Heating" },
                    { new Guid("2fae8e1e-dbac-4364-a47a-04255c3d4630"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Rooms For Rent" },
                    { new Guid("314b0210-2ef4-45bd-a1bf-77a486ec2c56"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Commercials For Sale" },
                    { new Guid("34d135a1-2439-4ea2-986c-836d6515ee6c"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Toys For Kids" },
                    { new Guid("40cad012-65fd-46b9-9213-1a5874e20078"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Houses For Sale" },
                    { new Guid("4166d6d2-36d9-4a7c-8b0d-0ee227639fc5"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Gym Fitness & Combat Sports" },
                    { new Guid("41ff7d91-e135-4bd5-af8d-0b007a7a747d"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Other Home Appliances" },
                    { new Guid("421787e3-b8f0-4aed-a2ab-db124007ffb3"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Billiard & Similar Games" },
                    { new Guid("44fa270a-a0e2-4d41-bea5-efc9f60d05f0"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Trucks & Buses" },
                    { new Guid("48a19589-ea3c-476d-b8a9-9c8171cd7d93"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Dogs" },
                    { new Guid("4d79b502-48a4-413e-879d-5f2c40d7b98a"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Ski & Winter Sports" },
                    { new Guid("53cdfb03-cc9e-48b9-8427-6a29d8aa0d80"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Pet Services" },
                    { new Guid("55c873be-0627-4baf-bae4-2fcf41c40f4a"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("55c8a352-84c9-4754-8234-10fd72e1e892"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Home Audio & Speakers" },
                    { new Guid("5d5f6a1a-3303-43f5-ad80-2194edef4fe3"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Other Animals" },
                    { new Guid("5d8fff4e-dd21-4c6d-8b68-2a03f6a142bb"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Bicycles & Accessories" },
                    { new Guid("5f4c75d0-8559-43ce-a892-1dd242f08eee"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("5f5ca06e-6c77-4d9e-a19c-ace81b687a94"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("659d0d21-1051-401f-b276-da30b4fa931a"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Living Room" },
                    { new Guid("70c6feda-0315-4dc1-9f01-44c1059e60c6"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Other for Kids & Babies" },
                    { new Guid("71005e0c-6a51-4470-878f-93e995588173"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Ball Sports" },
                    { new Guid("75d13824-8cd7-406f-8f9f-4e53655a00c3"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("780a5e40-aefc-45d6-8f3e-d29010592a3b"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Pet Food & Treats" },
                    { new Guid("790e2b65-d1fc-48b5-a2e6-3d49691b3461"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Vehicle Accessories" },
                    { new Guid("7abe46ba-ca9e-475f-9df8-c365c8aadf14"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Books" },
                    { new Guid("7f9d79b8-1dd2-4f44-84a9-c57c34928655"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("82089091-2ae8-42ad-af2d-6a01ee9cc067"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Pet Grooming" },
                    { new Guid("8253b1a7-2cd0-4ad6-bd6f-dbacc4decce6"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Bathrooms" },
                    { new Guid("8308ac6a-54b4-4332-a313-6cf958147624"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Video Games" },
                    { new Guid("832bf55b-a3fb-4c59-8aea-7df57036880d"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Professional Services" },
                    { new Guid("85d86bb6-2192-494a-8d7e-72f0bf8002b3"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Bedrooms" },
                    { new Guid("865a8ef2-44f6-4233-8ff2-993b88c6f18e"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("877e1687-de61-4efb-aaae-d63a894e3c65"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Strollers & Seats" },
                    { new Guid("8c3df1ef-8193-416f-bd17-5b4b42b4b874"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Safety & Monitors" },
                    { new Guid("9009cc3e-cc19-4ed8-a09c-b58cfcee0266"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("948a6b3d-1e61-4e1c-ae22-eb3acf053f18"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Cameras" },
                    { new Guid("9562fd7a-7939-48a0-a717-5428387e6394"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Personal Services" },
                    { new Guid("96fa1762-ca6e-4355-9e93-018d2a2cc7de"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("976b6691-2db7-4dbd-99b3-e2b945827614"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Other Fashion & Style" },
                    { new Guid("97ba7c2e-37d7-4e39-927b-337ec402fe06"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Other Furniture & Decor" },
                    { new Guid("97f066b2-94d8-4af2-9ac8-794eec89a8f7"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Commercials For Rent" },
                    { new Guid("98692ba4-991a-4aa8-a947-e93de58c8eb2"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Accessories For Men" },
                    { new Guid("a2e33db7-6642-474f-bb75-3d584a9517db"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Pet Accessories" },
                    { new Guid("a3b9c546-d318-4299-b5e7-18a8708d9496"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Antiques & Collectibles" },
                    { new Guid("a48f12e6-5837-44f8-81a7-f6f6cdbe6c73"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Transport" },
                    { new Guid("a52fcb5b-0a84-43a6-b4f6-f2fe500cd876"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Home Services" },
                    { new Guid("a58ddd8f-bdfa-46ed-9a73-7e707b520d76"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Land For Sale" },
                    { new Guid("a5b4f367-03f2-4f6b-86c0-dd6fcb86143e"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("a7f80fa0-1660-4bd3-a83b-a0d4d1c6d622"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("a9fbfede-aae2-40ff-add6-1f08c4337769"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Events" },
                    { new Guid("aab85e2a-57be-44ee-9c4c-77c991e05844"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Outdoors & Camping" },
                    { new Guid("b0b3a7d0-6c1f-4717-bfe1-43f17db6ffb0"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Garden & Outdoors" },
                    { new Guid("b9809e70-4120-480f-a499-5c8f71b8a939"), new Guid("50cf00ba-c75b-486b-95a2-509adf8da948"), null, 0, "Other Services" },
                    { new Guid("bac0b255-a8dd-4589-8d73-d2c63640a912"), new Guid("09c657b6-fd03-4c1c-af71-7dc61e773a7c"), null, 0, "Watches" },
                    { new Guid("c47b2e8b-17c8-406e-856b-cdc3e4ae74cd"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Cats" },
                    { new Guid("c521a539-bf59-4604-b4a2-d9c6a14aadae"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Toys" },
                    { new Guid("c94fd2d6-c095-4260-85e2-b3bc04a50437"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Movies" },
                    { new Guid("d0b1affe-d4ac-46e7-af3c-bbb576f01fc9"), new Guid("031f53da-215e-4361-b068-f8b960515a21"), null, 0, "Other Items" },
                    { new Guid("d1a2f171-9c22-4bf0-9756-a2cd3429dda8"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("d6125b0b-b2f4-4395-bd1c-62b9d430fc0c"), new Guid("bb8a13b3-8f9a-4ede-a792-d090e694eaa4"), null, 0, "Land For Rent" },
                    { new Guid("d6ab97da-baf2-4575-ace3-320f9e47dc43"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("da05f2bc-212c-4df6-bd86-fd575217824d"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Water Sports & Diving" },
                    { new Guid("e6cbd2ba-719e-4632-9769-c1e3cd052efc"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("e6f074e5-e064-42a5-9a01-e92238082002"), new Guid("936fa3de-1369-44fd-8fca-a15aa46dc254"), null, 0, "Mobile Accessories" },
                    { new Guid("e8b55c3e-6b11-4780-bd7e-e902b75a4d22"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("e8df1f2b-e31a-4323-8cc2-8c6adb04adf9"), new Guid("c3e63217-b8df-4cdd-9141-55202aaeaddb"), null, 0, "Supplements & Nutrition" },
                    { new Guid("e980f1c2-a15e-4112-83e8-5822277a882d"), new Guid("ab062d4a-2e67-46c2-88c1-b53094b5a724"), null, 0, "Dining Rooms" },
                    { new Guid("ec2202d5-da23-4ab6-a29e-77bd26df202f"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Bathing Accessories" },
                    { new Guid("ecbaec45-4943-4a7e-b8fe-b751cd463fae"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Boats" },
                    { new Guid("f0e6649e-34b2-40c1-a6d4-b361f12730e7"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "Cleaning Appliances" },
                    { new Guid("f11d4846-b486-4edc-b4ba-83cb4be90d05"), new Guid("18dfe493-66a6-4ada-a9e4-05fae6b685b6"), null, 0, "Cars For Sale" },
                    { new Guid("f940c6a3-e8ed-485b-acf5-32eb8a04ba8f"), new Guid("a3dd50b3-f30a-4f25-9b21-9ba577e2ea1c"), null, 0, "Birds" },
                    { new Guid("f9ef7034-6209-4f5e-9891-e70ba562a282"), new Guid("fb2fa159-cb23-4dea-b995-3ac1394f25bc"), null, 0, "Feeding & Nursing" },
                    { new Guid("ff1495b9-840e-45b8-9041-2b74db8dc416"), new Guid("8436c326-6f4e-4e08-b4e4-af61e8a7bf64"), null, 0, "TV & Video" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SentAt",
                table: "ChatMessages",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_InitiatorId",
                table: "Chats",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IsArchived",
                table: "Chats",
                column: "IsArchived");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_JobListingId",
                table: "Chats",
                column: "JobListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_LastActivity",
                table: "Chats",
                column: "LastActivity");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ListingId",
                table: "Chats",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_BaseLocation",
                table: "JobListings",
                column: "BaseLocation");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_CreatedAt",
                table: "JobListings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_ExpiresAt",
                table: "JobListings",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_LocationTitle",
                table: "JobListings",
                column: "LocationTitle");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_OwnerId",
                table: "JobListings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_Status",
                table: "JobListings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryId",
                table: "Listings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CreatedAt",
                table: "Listings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_OwnerId",
                table: "Listings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_PickupLocationId",
                table: "Listings",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_Status",
                table: "Listings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_SubcategoryId",
                table: "Listings",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_UserId",
                table: "PickupLocations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedListings_ListingId",
                table: "SavedListings",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedListings_UserId_ListingId",
                table: "SavedListings",
                columns: new[] { "UserId", "ListingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_SearchedAt",
                table: "SearchQueries",
                column: "SearchedAt");

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_UserId",
                table: "SearchQueries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_TicketId",
                table: "TicketAttachments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_UploadedAt",
                table: "TicketAttachments",
                column: "UploadedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_UploaderId",
                table: "TicketAttachments",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_ReceiverId",
                table: "TicketMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_SenderId",
                table: "TicketMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_SentAt",
                table: "TicketMessages",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessages",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId",
                table: "Tickets",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ListingId",
                table: "Tickets",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_ActorId",
                table: "UserActivityLogs",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_Timestamp",
                table: "UserActivityLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ChatId",
                table: "UserReports",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_CreatedAt",
                table: "UserReports",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_IsResolved",
                table: "UserReports",
                column: "IsResolved");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReportedUserId",
                table: "UserReports",
                column: "ReportedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReporterId",
                table: "UserReports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirebaseUid",
                table: "Users",
                column: "FirebaseUid",
                unique: true,
                filter: "[FirebaseUid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PublicName",
                table: "Users",
                column: "PublicName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyChildDetails");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ElectronicsDetails");

            migrationBuilder.DropTable(
                name: "FashionDetails");

            migrationBuilder.DropTable(
                name: "FurnitureDetails");

            migrationBuilder.DropTable(
                name: "HobbiesDetails");

            migrationBuilder.DropTable(
                name: "PetsDetails");

            migrationBuilder.DropTable(
                name: "PhonesDetails");

            migrationBuilder.DropTable(
                name: "RealEstateDetails");

            migrationBuilder.DropTable(
                name: "SavedListings");

            migrationBuilder.DropTable(
                name: "SearchQueries");

            migrationBuilder.DropTable(
                name: "ServicesDetails");

            migrationBuilder.DropTable(
                name: "SportsDetails");

            migrationBuilder.DropTable(
                name: "TicketAttachments");

            migrationBuilder.DropTable(
                name: "TicketMessages");

            migrationBuilder.DropTable(
                name: "UserActivityLogs");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserReports");

            migrationBuilder.DropTable(
                name: "VehiclesDetails");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "JobListings");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "PickupLocations");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
