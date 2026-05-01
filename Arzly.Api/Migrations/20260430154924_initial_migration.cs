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
                    PromotionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobListings_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    { new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), "Smartphones, tablets, watches and accessories", "/images/categories/mobiles.svg", 0, "Phones & Gadgets" },
                    { new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), "Clothing, shoes, bags, jewelry and cosmetics", "/images/categories/fashion.svg", 0, "Fashion & Style" },
                    { new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), "Cars, motorcycles, boats, trucks and accessories", "/images/categories/vehicles.svg", 0, "Vehicles" },
                    { new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), "Apartments, villas, land and commercial properties", "/images/categories/properties.svg", 0, "Real Estate" },
                    { new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), "Toys, strollers, clothing and baby gear", "/images/categories/kids.svg", 0, "Kids & Babies" },
                    { new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), "Gym equipment, bicycles, camping and fitness gear", "/images/categories/sports.svg", 0, "Sports & Equipment" },
                    { new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), "Home repair, cleaning, tutoring, moving and more", "/images/categories/services.svg", 0, "Services" },
                    { new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), "Books, music, art, collectibles and musical instruments", "/images/categories/hobbies.svg", 0, "Hobbies" },
                    { new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), "Dogs, cats, birds, fish and pet supplies", "/images/categories/pets.svg", 0, "Pets" },
                    { new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), "TVs, laptops, cameras, kitchen and home appliances", "/images/categories/electronics.svg", 0, "Electronics & Appliances" },
                    { new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), "Home and office furniture, lighting, rugs and decor", "/images/categories/furniture.svg", 0, "Furniture & Decor" }
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
                table: "JobListings",
                columns: new[] { "Id", "AppUserId", "BaseLocation", "ContactMethod", "CreatedAt", "DeletedAt", "Description", "EducationLevel", "Email", "EmploymentType", "ExperienceLevel", "ExpiresAt", "IsDeleted", "IsPromoted", "JobField", "Languages", "LocationTitle", "Name", "OwnerId", "PhoneNumber", "PromotionEndDate", "PromotionStartDate", "PromotionType", "SalaryMax", "SalaryMin", "Status", "Title", "Type", "WorkplaceType", "lat", "lon" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, 23, 0, new DateTime(2026, 4, 20, 15, 49, 22, 988, DateTimeKind.Utc).AddTicks(9664), null, "We are looking for an experienced software engineer to join our growing team. Must have 5+ years of experience with .NET and React. Remote-friendly with flexible hours.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 20, 15, 49, 22, 988, DateTimeKind.Utc).AddTicks(9887), false, true, 21, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0201", new DateTime(2026, 5, 9, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1008), new DateTime(2026, 4, 25, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(774), 1, 120000.0, 90000.0, 1, " FiveToTenYears Software Engineer", 0, 2, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, 10, 2, new DateTime(2026, 4, 25, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1677), null, "Part-time customer service role for retail store. Evening and weekend shifts available. Great opportunity for students.", 1, "bhbored2022@gmail.com", 1, 0, new DateTime(2026, 5, 25, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1678), false, false, 12, null, "Tripoli", "Bourhan Hassoun", "user-2-id", "+1-555-0202", null, null, null, 22.0, 18.0, 1, "Customer Service Representative", 1, 0, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, 23, 1, new DateTime(2026, 4, 15, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1708), null, "Dynamic marketing manager needed for fast-paced startup. Lead our digital marketing efforts, manage social media, and develop brand strategy.", 3, "john@example.com", 0, 1, new DateTime(2026, 5, 15, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1711), false, false, 30, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0203", null, null, null, 90000.0, 70000.0, 1, "Marketing Manager", 1, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, 25, 0, new DateTime(2026, 4, 27, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1715), null, "Seeking talented graphic designer for ongoing project work. Must be proficient in Adobe Creative Suite. Portfolio required.", 3, "bhbored2022@gmail.com", 2, 0, new DateTime(2026, 5, 27, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1716), false, false, 13, null, "Hermel", "Bourhan Hassoun", "user-2-id", "+1-555-0204", null, null, null, 60.0, 40.0, 1, "Freelance Graphic Designer", 0, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), null, 17, 2, new DateTime(2026, 4, 10, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1721), null, "ICU nurse position at major hospital. Competitive salary, excellent benefits. Night shift differential included.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 10, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(1721), false, false, 18, null, "Zahle", "John Doe", "user-1-id", "+1-555-0205", null, null, null, 105000.0, 85000.0, 0, "Registered Nurse - ICU", 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "PickupLocations",
                columns: new[] { "Id", "Address", "DeletedAt", "IsDefault", "IsDeleted", "Label", "Lat", "Lon", "Notes", "UserId" },
                values: new object[,]
                {
                    { new Guid("3e545987-07b4-4130-b88f-e1830ac22d9f"), "456 Business Ave, New York, NY 10002", null, false, false, 1, 40.758000000000003, -73.985500000000002, "Call before arriving", "user-1-id" },
                    { new Guid("6b5529c9-8299-47a5-b688-4fc75c2c8208"), "321 Shopping Mall, Queens, NY 11354", null, false, false, 2, 40.728200000000001, -73.794899999999998, "Near the food court entrance", "user-2-id" },
                    { new Guid("a3f47519-f19d-48b7-97bc-bfa9cfadf5a4"), "123 Main Street, New York, NY 10001", null, true, false, 0, 40.712800000000001, -74.006, "Ring the doorbell twice", "user-1-id" },
                    { new Guid("b37fce3b-3ae7-461b-a02e-63dd33d46d2b"), "789 Park Lane, Brooklyn, NY 11201", null, true, false, 0, 40.678199999999997, -73.944199999999995, null, "user-2-id" }
                });

            migrationBuilder.InsertData(
                table: "SearchQueries",
                columns: new[] { "Id", "Query", "SearchedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("44efa511-db28-4cca-a799-996d2ac5fa26"), "apartment for rent in Brooklyn", new DateTime(2026, 4, 27, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(9075), "user-1-id" },
                    { new Guid("570c5033-59fc-40e0-b641-c8890e8ece8b"), "gaming laptop", new DateTime(2026, 4, 23, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(9083), "user-2-id" },
                    { new Guid("81eda076-52bd-43df-b0b8-5f54fbb99ac3"), "sofa set", new DateTime(2026, 4, 28, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(9086), "user-2-id" },
                    { new Guid("b5792aab-2166-4c54-a3a9-0e988a103061"), "BMW cars for sale", new DateTime(2026, 4, 25, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(8825), "user-1-id" },
                    { new Guid("efdba549-9d2c-42f6-8d33-411eb894a7d8"), "iPhone 15 Pro Max", new DateTime(2026, 4, 29, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(9078), "user-1-id" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ItemsCount", "Name" },
                values: new object[,]
                {
                    { new Guid("00238b5e-63f0-49e1-bc68-8386e710bc72"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Trucks & Buses" },
                    { new Guid("031e087d-ee28-4bf5-af70-fa3eec8df73b"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Cats" },
                    { new Guid("071e8dce-9f1f-4922-bf51-ba2a0c31d7e9"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("0bd90c19-c289-41fa-8506-1b365a83a339"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Commercials For Rent" },
                    { new Guid("15dfebf5-0d8e-4bd0-bdc5-2334c79f91c3"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Feeding & Nursing" },
                    { new Guid("18e91ecb-e2db-482b-a5fc-37a0c3f3f80c"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Bathrooms" },
                    { new Guid("1fdb9daa-8fca-4eb8-b952-77bd0e3cdc93"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Boats" },
                    { new Guid("2837c85a-3219-4d79-a2e0-976776f1c417"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Safety & Monitors" },
                    { new Guid("2e514a44-0c91-40c1-b897-5593599f70a6"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Land For Sale" },
                    { new Guid("2ee9fc3e-961a-4b33-aca9-755b9af4651b"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Ball Sports" },
                    { new Guid("319a2218-cecf-47ed-b6a6-db57bcbcb90c"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Clothing For Men" },
                    { new Guid("32f6d569-34a3-4ca3-a54e-a6cf1c8ef4c7"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Other Home Appliances" },
                    { new Guid("362d080e-0e72-4e63-b534-334e27facdb1"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Events" },
                    { new Guid("39390af1-517d-4c66-a417-65c4869b299d"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Bathing Accessories" },
                    { new Guid("495a6d3d-ef85-4903-80d4-d5e6724e303c"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Land For Rent" },
                    { new Guid("4a3ed01b-6122-4b36-8565-dd95af02ff98"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Antiques & Collectibles" },
                    { new Guid("4aa4fea5-ad88-4120-a787-fc932c3972ee"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("4b0a5ac9-9851-4752-aba5-ab3d066c8354"), new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), null, 0, "Smart Watches" },
                    { new Guid("4b80e331-26c3-4f13-806d-c9e5d4741477"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Accessories For Women" },
                    { new Guid("4c388c37-f3d2-4925-91a0-c8ea66c87772"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("4f2be330-41b0-4a5b-b40e-efd9347581fd"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Cameras" },
                    { new Guid("5089f851-4b39-48e4-b5f7-db9563586b5d"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Other Sports" },
                    { new Guid("5c4f4259-9208-475c-b20f-2a3653f6f124"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Musical Instruments" },
                    { new Guid("5f720311-25db-4b4e-b5fd-3ec1324cbec4"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("60c10f2c-03c8-4c90-9676-d3f55032d6f6"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Garden & Outdoors" },
                    { new Guid("6bf9e8d2-bc8c-4780-8dbb-902f566f2c14"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Pet Grooming" },
                    { new Guid("6fcb30eb-70a7-4ae5-b4d3-8c4f2b3a35dc"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Home Audio & Speakers" },
                    { new Guid("73534b97-33d1-4252-86c4-63dac75c5287"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Houses For Rent" },
                    { new Guid("7588e04e-e09d-4473-9087-035a8fef0d56"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "AC Cooling & Heating" },
                    { new Guid("75b9b734-b0e2-47bd-a096-0717a028d19e"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Personal Services" },
                    { new Guid("7ac28f95-a956-4fe3-8ddd-0c329dd3e314"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("7ad1fe9b-de48-4613-9538-7ea1e16e402f"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Pet Food & Treats" },
                    { new Guid("7bfde93e-ba48-4c18-9e6d-55aea9f629ac"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Bicycles & Accessories" },
                    { new Guid("7d3234bc-4ca6-4b8f-bc66-e52d9cbfc388"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Other Furniture & Decor" },
                    { new Guid("7fc23e86-5a00-4169-ac0e-93969a6ded60"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Cars For Sale" },
                    { new Guid("852752e6-ecea-48a4-9d04-a8c1304bf63f"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Vehicle Accessories" },
                    { new Guid("85af7117-5f8c-45bf-b2fe-93677082c5b6"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("85ea40d8-591f-4346-9adb-2bc79cc07034"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Bedrooms" },
                    { new Guid("8846cbb8-abd4-4f38-8edb-1f7cb653d9f7"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Books" },
                    { new Guid("8f4b5b81-ff99-49c4-b15e-09ef80929ccd"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Other Animals" },
                    { new Guid("922c4bf9-8a35-4044-bb1e-c03d37dc86ca"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Professional Services" },
                    { new Guid("9551fa8a-0f23-4d5b-bc4e-de1ebff4e2eb"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("9631be99-7b93-4eaa-976c-28c1aac945af"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Watches" },
                    { new Guid("96eddc63-f986-405f-a063-ef27d7fd0477"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("9bbf3534-8ed0-4bae-bf9f-8bbd1d8863a3"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Commercials For Sale" },
                    { new Guid("9c60da2c-cc89-4a69-b0d3-5102ec5f2926"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("9cc49a9e-3441-4384-a9d1-bd58db7e2d1d"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Birds" },
                    { new Guid("9ce7df0c-e37a-42ca-8f99-13171ffb1b4c"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("a1a9a4ca-a7e6-4e17-9f32-beb6bd8d7b25"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Pet Services" },
                    { new Guid("a58b56ff-ec62-4b11-a3f9-9dbd5171cd74"), new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), null, 0, "Mobile Numbers" },
                    { new Guid("a5f2c0a0-ea69-451b-b857-002accb82f53"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Billiard & Similar Games" },
                    { new Guid("a91bfeeb-ec77-41d2-859b-8e5cb90a9c66"), new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), null, 0, "Mobile Phones" },
                    { new Guid("ada7c0b3-9d3e-4266-b362-ef4fd31b8644"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Accessories For Men" },
                    { new Guid("ae4580b3-5da8-4040-b3d4-009ce10fa3f1"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Home Services" },
                    { new Guid("b126b97b-7965-44fd-8e3a-14221666d1f3"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Dining Rooms" },
                    { new Guid("b26b733b-2485-4f7e-924d-b50454a5ad37"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Toys" },
                    { new Guid("b30e7d52-f535-4488-84ff-f3cabff04eaa"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Video Games" },
                    { new Guid("b3a184a0-01cb-40fa-b479-b466bc1d6bca"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Outdoors & Camping" },
                    { new Guid("c20022b0-51c8-4a46-95de-84a238968394"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Gym Fitness & Combat Sports" },
                    { new Guid("c21b32e2-a476-4fb1-9ae5-4e808ca31a63"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("c3508062-875d-4289-a8a7-8f0df219f257"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), null, 0, "Living Room" },
                    { new Guid("c3eeadbf-2a23-4498-a87b-a4fd5303e305"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("c6867a59-3ae2-4c7d-957c-79fb83e7fc38"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Pet Accessories" },
                    { new Guid("cdf64170-f5be-423d-ae0c-05cb24a9d282"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Water Sports & Diving" },
                    { new Guid("d0c2c61b-9e58-4ded-8a30-dba2e2372181"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), null, 0, "Dogs" },
                    { new Guid("d2ab7e6b-11f7-4e10-a871-209c5c6636c8"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Cleaning Appliances" },
                    { new Guid("d2b3a5ea-c00a-4588-883b-a4b08e9eb231"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Supplements & Nutrition" },
                    { new Guid("d728fb8c-ffb2-473b-8a92-751e19549460"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Other for Kids & Babies" },
                    { new Guid("d838bd7f-7a46-4f57-b9aa-31d9d6b7c708"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("d91a18e4-a59a-469c-8151-14cfe9af88f5"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Other Fashion & Style" },
                    { new Guid("db0d3ab9-d5ba-4f73-a256-02f1e07f4b22"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Transport" },
                    { new Guid("db55d931-6854-4604-9f54-b535ca73ebb2"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("df4b487f-8bbf-48f9-b01c-a0123129c869"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Rooms For Rent" },
                    { new Guid("e206520b-60db-4cc3-872c-ef754a3807f9"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Movies" },
                    { new Guid("e7102a85-36cd-4417-a12b-ff3447cbb3cf"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), null, 0, "TV & Video" },
                    { new Guid("e727a29c-a2a7-4421-b7ab-073cdaea12bd"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("e7454e3e-f314-4be2-a9c5-51ce4f3be794"), new Guid("24784d09-4881-4a11-8c68-2a67f4d32f87"), null, 0, "Clothing For Women" },
                    { new Guid("e890d8d2-c600-4961-9766-49b3f2f4cf3d"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Strollers & Seats" },
                    { new Guid("e8d8e8db-cf42-4788-a987-eacd7e8d5b63"), new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), null, 0, "Mobile Accessories" },
                    { new Guid("e8e84db6-2a96-415c-bcf1-383f5095733c"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("e966c458-3745-4c40-adc2-122cdeaddc22"), new Guid("9d855155-09ed-40dc-9ba0-4b31833f9ea0"), null, 0, "Toys For Kids" },
                    { new Guid("ef1ec32d-378d-42ac-9354-2a706392594b"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), null, 0, "Other Services" },
                    { new Guid("f17e9a6e-2e3e-46be-b83f-edd46a0ecf23"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Other Items" },
                    { new Guid("f46c58d4-e5f9-4cb5-bf28-024eaf0369d7"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), null, 0, "Number Plates" },
                    { new Guid("fb1feeb7-25ad-4041-a1a3-54653cbae8c4"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), null, 0, "Houses For Sale" },
                    { new Guid("fca5634e-81e1-4dc2-bcb9-82921a38a428"), new Guid("be6549b8-588f-406f-bf91-a6f50e66f97c"), null, 0, "Games & Hobbies" },
                    { new Guid("ffd944ff-c384-4caa-94f9-5d2e1a8652ba"), new Guid("aa24a6f6-fd36-4cb1-a7ee-5d8d2c52355f"), null, 0, "Ski & Winter Sports" }
                });

            migrationBuilder.InsertData(
                table: "UserActivityLogs",
                columns: new[] { "Id", "ActionType", "ActorId", "ActorRole", "Details", "DurationMs", "ErrorMessage", "IPAddress", "IsSuccess", "TargetId", "TargetType", "Timestamp", "UserAgent" },
                values: new object[,]
                {
                    { new Guid("2640698f-15e5-4e36-a366-f0ba4df9d1ba"), 0, "user-1-id", "User", null, 250, null, "192.168.1.100", true, null, 0, new DateTime(2026, 4, 29, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(5374), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("424c1994-209a-4ed8-9661-10d4b541261c"), 2, "user-1-id", "User", "User registered successfully", null, null, "192.168.1.100", true, "user-1-id", 1, new DateTime(2026, 3, 31, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(4922), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("44ac02df-ad7a-4673-b4a6-d8a17e70b6c8"), 2, "user-2-id", "User", "User registered successfully", null, null, "192.168.1.105", true, "user-2-id", 1, new DateTime(2026, 4, 10, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(5630), "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)" },
                    { new Guid("52c8a3b4-3377-4e27-9f0b-19f40671de61"), 16, "user-2-id", "User", "Sent message in chat", 120, null, "192.168.1.105", true, "chat-1-id", 3, new DateTime(2026, 4, 30, 10, 49, 22, 990, DateTimeKind.Utc).AddTicks(5633), null },
                    { new Guid("7d30b4c8-6e69-4307-9ad9-cd0be37823aa"), 13, "user-1-id", "User", "Saved listing to favorites", null, null, "192.168.1.100", true, "listing-2-id", 2, new DateTime(2026, 4, 30, 13, 49, 22, 990, DateTimeKind.Utc).AddTicks(5651), null },
                    { new Guid("b829dbb8-3608-4a41-82d1-84ee8b5cebb8"), 8, "user-1-id", "User", "Created listing: BMW 320i", null, null, "192.168.1.100", true, "listing-1-id", 2, new DateTime(2026, 4, 20, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(5627), null }
                });

            migrationBuilder.InsertData(
                table: "UserPreferences",
                columns: new[] { "UserId", "EmailNotifications", "Language", "PushNotifications", "Theme", "UpdatedAt" },
                values: new object[,]
                {
                    { "user-1-id", false, 0, true, 2, new DateTime(2026, 4, 30, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(8397) },
                    { "user-2-id", true, 0, true, 1, new DateTime(2026, 4, 30, 15, 49, 22, 990, DateTimeKind.Utc).AddTicks(8634) }
                });

            migrationBuilder.InsertData(
                table: "UserReports",
                columns: new[] { "Id", "ChatId", "CreatedAt", "IsResolved", "Notes", "Reason", "ReportedUserId", "ReporterId", "ResolvedAt", "ResolvedById" },
                values: new object[,]
                {
                    { new Guid("7f9a8fd6-a6c8-4818-8732-6a07eedeca8e"), null, new DateTime(2026, 4, 25, 15, 49, 22, 991, DateTimeKind.Utc).AddTicks(1833), true, "User posting duplicate listings", 0, "user-2-id", "user-1-id", new DateTime(2026, 4, 27, 15, 49, 22, 991, DateTimeKind.Utc).AddTicks(2315), null },
                    { new Guid("e7cb56e0-89b9-4fb0-9130-4f6216823b7c"), null, new DateTime(2026, 4, 28, 15, 49, 22, 991, DateTimeKind.Utc).AddTicks(2582), false, "Listing contains misleading information", 3, "user-1-id", "user-2-id", null, null }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "CategoryId", "ContactMethod", "CreatedAt", "DeletedAt", "Description", "ImagesUrl", "IsDeleted", "IsPriceNegotiable", "IsPromoted", "Name", "OwnerId", "PhoneNumber", "PickupLocationId", "Price", "PrimaryImageUrl", "PromotionEndDate", "PromotionStartDate", "PromotionType", "Status", "SubcategoryId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), 1, new DateTime(2026, 4, 15, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(5314), null, "Well-maintained BMW 320i with full service history. Luxury package, navigation system, leather seats, and sunroof. Single owner, garage kept.", "[\"https://example.com/images/bmw-320i-2.jpg\",\"https://example.com/images/bmw-320i-3.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0101", new Guid("a3f47519-f19d-48b7-97bc-bfa9cfadf5a4"), 45000.0, "https://example.com/images/bmw-320i-1.jpg", null, null, null, 1, new Guid("7fc23e86-5a00-4169-ac0e-93969a6ded60"), "BMW 320i Luxury Line 2022", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("3cad2798-23de-4196-8dd7-952155fd1ab4"), 1, new DateTime(2026, 4, 23, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(5599), null, "Clean Toyota Camry, perfect for daily commute. Fuel-efficient, reliable, and spacious. Recent oil change and tire rotation.", "[\"https://example.com/images/toyota-camry-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0102", new Guid("b37fce3b-3ae7-461b-a02e-63dd33d46d2b"), 28000.0, "https://example.com/images/toyota-camry-1.jpg", null, null, null, 1, new Guid("7fc23e86-5a00-4169-ac0e-93969a6ded60"), "Toyota Camry 2020 - Excellent Condition", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("59733c2c-27c7-40ac-8cf5-ed9f89b14b0a"), 1, new DateTime(2026, 4, 27, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(5605), null, "Stunning 2-bedroom apartment with city views. Fully equipped kitchen, in-unit laundry, gym and pool in building. Close to subway.", "[\"https://example.com/images/apt-manhattan-2.jpg\",\"https://example.com/images/apt-manhattan-3.jpg\",\"https://example.com/images/apt-manhattan-4.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0103", new Guid("a3f47519-f19d-48b7-97bc-bfa9cfadf5a4"), 4500.0, "https://example.com/images/apt-manhattan-1.jpg", null, null, null, 1, new Guid("fb1feeb7-25ad-4041-a1a3-54653cbae8c4"), "Modern 2BR Apartment in Manhattan", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("cba614a7-eb8c-45d8-9f8e-dd0c5803d91d"), 0, new DateTime(2026, 4, 29, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(5611), null, "Like-new MacBook Pro with M2 Max chip, 32GB RAM, 1TB SSD. Perfect for professionals. AppleCare+ until 2026.", "[\"https://example.com/images/macbook-pro-2.jpg\"]", false, false, true, "Bourhan Hassoun", "user-2-id", "+1-555-0104", new Guid("b37fce3b-3ae7-461b-a02e-63dd33d46d2b"), 2800.0, "https://example.com/images/macbook-pro-1.jpg", new DateTime(2026, 5, 6, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(6601), new DateTime(2026, 4, 29, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(6344), 0, 1, new Guid("c21b32e2-a476-4fb1-9ae5-4e808ca31a63"), "MacBook Pro 16\" M2 Max 2023", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("d1c9e429-21fc-4164-8fc0-2f64585a7026"), 0, new DateTime(2026, 4, 25, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(6858), null, "Comfortable L-shaped sectional sofa in excellent condition. Pet-free, smoke-free home. Easy to clean fabric.", "[\"https://example.com/images/sofa-gray-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0105", new Guid("3e545987-07b4-4130-b88f-e1830ac22d9f"), 650.0, "https://example.com/images/sofa-gray-1.jpg", null, null, null, 1, new Guid("c3508062-875d-4289-a8a7-8f0df219f257"), "IKEA Sectional Sofa - Gray", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("0bb2ac3e-e98b-48f6-b5c6-c0cc705fc825"), 1, new DateTime(2026, 4, 10, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(6866), new DateTime(2026, 4, 20, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(7114), "Brand new iPhone 15 Pro Max, unopened box. 256GB storage in Titanium Blue. Full Apple warranty.", "[\"https://example.com/images/iphone-15-pro-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0106", new Guid("6b5529c9-8299-47a5-b688-4fc75c2c8208"), 1200.0, "https://example.com/images/iphone-15-pro-1.jpg", null, null, null, 2, new Guid("a91bfeeb-ec77-41d2-859b-8e5cb90a9c66"), "iPhone 15 Pro Max 256GB - Titanium Blue", new DateTime(2026, 4, 20, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(6867) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("ab2a6892-b17d-41a7-b344-251426ea00df"), 0, new DateTime(2026, 3, 31, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(7352), null, "Licensed plumber with 15+ years experience. Emergency services available. Free estimates for all jobs.", "[]", false, true, false, "John Doe", "user-1-id", "+1-555-0107", new Guid("a3f47519-f19d-48b7-97bc-bfa9cfadf5a4"), 0.0, "https://example.com/images/plumbing-service-1.jpg", null, null, null, 1, new Guid("8f4b5b81-ff99-49c4-b15e-09ef80929ccd"), "Professional Plumbing Services", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("c7e33f89-5f59-4384-9f32-5062d53a8e56"), 0, new DateTime(2026, 4, 28, 15, 49, 22, 987, DateTimeKind.Utc).AddTicks(7365), null, "Adorable Golden Retriever puppies looking for loving homes. Vaccinated, microchipped, and health checked. Parents on premises.", "[\"https://example.com/images/golden-retriever-2.jpg\",\"https://example.com/images/golden-retriever-3.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0108", new Guid("b37fce3b-3ae7-461b-a02e-63dd33d46d2b"), 1500.0, "https://example.com/images/golden-retriever-1.jpg", null, null, null, 1, new Guid("18e91ecb-e2db-482b-a5fc-37a0c3f3f80c"), "Golden Retriever Puppies", null }
                });

            migrationBuilder.InsertData(
                table: "SavedListings",
                columns: new[] { "Id", "DeletedAt", "ListingId", "SavedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 4, 28, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(5034), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2026, 4, 29, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(5342), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 4, 25, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(5349), "user-2-id" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 4, 29, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(5352), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 4, 27, 15, 49, 22, 989, DateTimeKind.Utc).AddTicks(5351), "user-2-id" }
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
                name: "IX_JobListings_AppUserId",
                table: "JobListings",
                column: "AppUserId");

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
