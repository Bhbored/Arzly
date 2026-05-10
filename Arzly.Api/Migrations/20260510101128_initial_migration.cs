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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
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
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    { new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), "Apartments, villas, land and commercial properties", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/real-estate.png", 0, "Real Estate" },
                    { new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), "TVs, laptops, cameras, kitchen and home appliances", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/electronics%26appliances.png", 0, "Electronics & Appliances" },
                    { new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), "Toys, strollers, clothing and baby gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/kids%26babies.png", 0, "Kids & Babies" },
                    { new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), "Books, music, art, collectibles and musical instruments", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/hobbies.png", 0, "Hobbies" },
                    { new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), "Cars, motorcycles, boats, trucks and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/vehicles.png", 0, "Vehicles" },
                    { new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), "Home and office furniture, lighting, rugs and decor", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/furniture%26decor.png", 0, "Furniture & Decor" },
                    { new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), "Dogs, cats, birds, fish and pet supplies", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/pets.png", 0, "Pets" },
                    { new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), "Gym equipment, bicycles, camping and fitness gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/sports%26equipment.png", 0, "Sports & Equipment" },
                    { new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), "Home repair, cleaning, tutoring, moving and more", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/services.png", 0, "Services" },
                    { new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), "Clothing, shoes, bags, jewelry and cosmetics", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/fshion%26style.png", 0, "Fashion & Style" },
                    { new Guid("eaade39c-8743-4a1a-b763-48c4dd767603"), "Smartphones, tablets, watches and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/phones%26gadgets.png", 0, "Phones & Gadgets" }
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
                    { new Guid("03c40745-76fa-46cc-8018-21148fcdb071"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Dining Rooms" },
                    { new Guid("06297fda-45cd-49af-ac8b-ba63a59500ee"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Pet Services" },
                    { new Guid("08f7a769-4141-42c3-96d3-a9f74125c738"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Water Sports & Diving" },
                    { new Guid("0b148f0c-c2af-4b5f-9897-22961f906ed6"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Land For Sale" },
                    { new Guid("0c18e0a4-2750-41b1-99a4-e6ce350cfe1d"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("1097b341-05ed-4976-8a02-4fe13a3be64e"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("15825ab0-6b94-4a04-a1fa-2237bdb3c744"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Antiques & Collectibles" },
                    { new Guid("16ba03e2-d7da-4403-a79a-c19cf434177b"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Musical Instruments" },
                    { new Guid("17097065-488d-43d7-9539-ee728504f179"), new Guid("eaade39c-8743-4a1a-b763-48c4dd767603"), null, 0, "Smart Watches" },
                    { new Guid("1a0a7986-20cf-430b-87bb-70ff57807fcb"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("1a2a2cf7-6304-4316-a724-a0a87d64ea93"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Cameras" },
                    { new Guid("1e5f6852-b4d7-4f76-9e91-7cfcbd23ffb6"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Dogs" },
                    { new Guid("229e517e-c7f5-4efa-a169-39dd10e996e2"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Number Plates" },
                    { new Guid("22b4a858-3e88-4ce4-a8b0-624286f3d767"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Movies" },
                    { new Guid("2d528e7d-7ab1-46f9-b479-a567ac504140"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Games & Hobbies" },
                    { new Guid("30db5017-e153-4a1e-a8f4-e72333b36118"), new Guid("eaade39c-8743-4a1a-b763-48c4dd767603"), null, 0, "Mobile Numbers" },
                    { new Guid("350efd39-eea3-43fe-baf5-18f67dfcdc6a"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("35b09616-916e-44ed-9e4d-576ce4c90bf6"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Houses For Rent" },
                    { new Guid("35dac49c-b12f-4087-bbd4-cf8ecdedf91d"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Other Sports" },
                    { new Guid("3ce5b607-e9b5-445f-b692-83708e98a0e0"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("3f2aba22-1a0d-479a-ac0b-455a7d1eb991"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Boats" },
                    { new Guid("439d273d-c5a2-4ec2-8a5e-0b8dc55be645"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Toys" },
                    { new Guid("458d47b8-7b0f-433c-be5d-5d885bf2121e"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Cars For Sale" },
                    { new Guid("518b2c79-aac7-49e3-b5d5-4f3dbe789b71"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Other Furniture & Decor" },
                    { new Guid("52cf7d96-d585-46b5-b6be-2c36a104b52a"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("54690ce5-2309-4a20-bf1d-6c8c17f60cbb"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Home Services" },
                    { new Guid("5e83b11b-a368-4367-9254-aef1672e4dc1"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Trucks & Buses" },
                    { new Guid("60e9bd07-f2b7-4490-af9f-025318ed5e3d"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Toys For Kids" },
                    { new Guid("643b8a99-8d0d-4283-9faf-67e2395ece98"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Accessories For Women" },
                    { new Guid("67ae26e0-3ba3-4b55-b5a0-c474234faef7"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Outdoors & Camping" },
                    { new Guid("67f0a63e-7a9b-4da5-ab03-9da7f02da419"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Pet Grooming" },
                    { new Guid("6a34eb32-83a7-46be-9d17-e1c04c0e09e3"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Home Audio & Speakers" },
                    { new Guid("700abe2f-a9dc-4899-a608-aee91fdaccc6"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("7763b39a-08ea-4fb5-b1d1-ca5a14a69c03"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Garden & Outdoors" },
                    { new Guid("7a8e9faf-085c-4c5a-b9fb-9aaa5489a6ef"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Other Fashion & Style" },
                    { new Guid("7d2f2064-c7e4-40d5-936c-22e5af46e8a4"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Bicycles & Accessories" },
                    { new Guid("7f98ef64-4512-4b72-b812-1a8dd834a968"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("80dd3c41-d168-431a-9e41-0a379ac8e6ae"), new Guid("eaade39c-8743-4a1a-b763-48c4dd767603"), null, 0, "Mobile Accessories" },
                    { new Guid("819a5979-27e8-4e4b-b92f-dcaf867d5701"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Transport" },
                    { new Guid("8504d029-32ed-44c5-a8ff-c1f37f742765"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Pet Accessories" },
                    { new Guid("899687ac-1257-44f0-918e-3d386d1cbfb4"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Pet Food & Treats" },
                    { new Guid("8c4cf154-8107-4fcb-8b2f-94b1f7051297"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "TV & Video" },
                    { new Guid("8f02077b-36d9-42ee-8e96-19e3965d8f49"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Other Items" },
                    { new Guid("8f13e1b9-70d9-4104-bd3c-7bcff6c5e761"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("914cee69-e165-4a5b-b117-a978b3b70aea"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Land For Rent" },
                    { new Guid("94246f72-590d-4448-bff1-4bfeb892ea70"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Video Games" },
                    { new Guid("970e77cb-ad4d-40bb-8f08-01b2a5e295b3"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Feeding & Nursing" },
                    { new Guid("9940e9f9-68b8-4b9e-ab65-3db57a20d0dd"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Bathrooms" },
                    { new Guid("9cde56ab-31b7-43ad-a5eb-d7e56ba172f9"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("9f39594a-0b3a-4de3-8a5e-d33a4eaf010c"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Gym Fitness & Combat Sports" },
                    { new Guid("a24557e3-147c-4280-9117-81a6da74c011"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Cleaning Appliances" },
                    { new Guid("a785f829-019c-44a9-94ac-9afd54a0fdeb"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("a7c0cbb0-1706-4564-8fbb-765ecd463efe"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Clothing For Men" },
                    { new Guid("aa011fc0-01c5-4494-9706-42d9f0f7fba4"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "Other Home Appliances" },
                    { new Guid("aa1ac7a9-a943-4bdc-927b-9762eb3b1b63"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Events" },
                    { new Guid("ae8b75ef-30fe-454d-9026-8b3c4c687926"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Ball Sports" },
                    { new Guid("aeba5c93-ea5b-4606-9627-b51d5f2162bf"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Safety & Monitors" },
                    { new Guid("af7d80f6-6963-4ea5-8f3d-6fbcb75962ac"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("b27d41e2-d9e6-46c4-88a4-0105d5320361"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("b2e3736b-fdc5-41c9-b1a6-9fe04b13e599"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Living Room" },
                    { new Guid("b3646b69-c537-4029-834d-a1b42cd9cea8"), new Guid("68656ff6-0ae5-4ee3-8dd6-74d9057238c2"), null, 0, "Bedrooms" },
                    { new Guid("ba4c262f-0b98-4c21-80bb-4261897b4e30"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Birds" },
                    { new Guid("bde57b35-4e87-4a5a-853a-4dfb17b602cb"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("be45e09d-effb-438a-8a86-6aecbb587148"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Other for Kids & Babies" },
                    { new Guid("c0a0b6a1-0b74-4c41-aafb-79013bf16f65"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Personal Services" },
                    { new Guid("c161823e-c79d-4c02-901d-aca08b749fe6"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Ski & Winter Sports" },
                    { new Guid("c2b3344f-832e-4c3d-88e5-f9ea5f2a7415"), new Guid("2baadca7-8d5b-487a-b65d-31096eaff0df"), null, 0, "AC Cooling & Heating" },
                    { new Guid("c45be71b-47f5-4f52-a31c-c5c2d7946af7"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Other Animals" },
                    { new Guid("c6cadbeb-67b7-4279-b6c2-61f14c8c85b5"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Professional Services" },
                    { new Guid("c768a8be-aecc-4fba-a7c9-8fd8071b407a"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Supplements & Nutrition" },
                    { new Guid("ca122af2-4ab7-4008-8fb3-916ccb2efdb3"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("d211e286-47a0-4e98-a9a6-f0d38a3a5c29"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Watches" },
                    { new Guid("d22b841b-4e2c-4cf9-afcf-839a387ec4f1"), new Guid("7158d4b7-81aa-4758-98fc-ca9fb1d55456"), null, 0, "Cats" },
                    { new Guid("d6e8a949-148b-4129-a620-877bc9d4fb3f"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Houses For Sale" },
                    { new Guid("def3b53d-d330-409b-a121-b2e240b3566e"), new Guid("75b525d7-2931-4b24-9120-1d5461ae0b00"), null, 0, "Billiard & Similar Games" },
                    { new Guid("e4035aa7-821c-4a70-aa0f-7e186fd24060"), new Guid("abd1969e-6f78-4ca2-b37c-561d0430749c"), null, 0, "Other Services" },
                    { new Guid("e4a7560a-4322-4e6f-a83e-968ce620b250"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Bathing Accessories" },
                    { new Guid("e8ef9b12-a4a0-430e-94ed-330d363d6b47"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Accessories For Men" },
                    { new Guid("e9c34849-87b3-495f-a1d1-09fc8a0446d3"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Commercials For Sale" },
                    { new Guid("ec5425fc-cf59-4e26-aaa7-e3e04d827a86"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Vehicle Accessories" },
                    { new Guid("f0680de1-9416-49e8-b1da-d570f4367814"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Rooms For Rent" },
                    { new Guid("f2ac3007-3903-4826-a9b9-8a3a723ac78f"), new Guid("eaade39c-8743-4a1a-b763-48c4dd767603"), null, 0, "Mobile Phones" },
                    { new Guid("f4d35fe5-08f5-4dd2-a088-e221d75d9e4c"), new Guid("4fe0a9f2-14e4-4a59-89a0-6aabf34544ba"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("fb32751d-d4be-4a3f-a136-427c63a3abb4"), new Guid("c9bc0d2f-46d9-42ad-b203-05882beb4209"), null, 0, "Clothing For Women" },
                    { new Guid("fb4870af-9299-4459-a9e3-54936ae20411"), new Guid("0a967e69-5a7b-4bd9-b22d-54021829dceb"), null, 0, "Commercials For Rent" },
                    { new Guid("fe7d41b6-8270-4d9b-81be-6b93ad7d149e"), new Guid("2f039f23-c9d0-4325-902e-0677aeb218dd"), null, 0, "Strollers & Seats" },
                    { new Guid("ff9e9fc3-7963-4e8f-8563-f5bedf850527"), new Guid("337facd0-a09b-4b56-bd0b-545ff90ec395"), null, 0, "Books" }
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
