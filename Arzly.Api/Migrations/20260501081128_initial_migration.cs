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
                    { new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), "TVs, laptops, cameras, kitchen and home appliances", "/images/categories/electronics.svg", 0, "Electronics & Appliances" },
                    { new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), "Clothing, shoes, bags, jewelry and cosmetics", "/images/categories/fashion.svg", 0, "Fashion & Style" },
                    { new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), "Books, music, art, collectibles and musical instruments", "/images/categories/hobbies.svg", 0, "Hobbies" },
                    { new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), "Smartphones, tablets, watches and accessories", "/images/categories/mobiles.svg", 0, "Phones & Gadgets" },
                    { new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), "Dogs, cats, birds, fish and pet supplies", "/images/categories/pets.svg", 0, "Pets" },
                    { new Guid("7e917c79-782e-480d-8d76-052194601264"), "Home repair, cleaning, tutoring, moving and more", "/images/categories/services.svg", 0, "Services" },
                    { new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), "Toys, strollers, clothing and baby gear", "/images/categories/kids.svg", 0, "Kids & Babies" },
                    { new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), "Apartments, villas, land and commercial properties", "/images/categories/properties.svg", 0, "Real Estate" },
                    { new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), "Cars, motorcycles, boats, trucks and accessories", "/images/categories/vehicles.svg", 0, "Vehicles" },
                    { new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), "Home and office furniture, lighting, rugs and decor", "/images/categories/furniture.svg", 0, "Furniture & Decor" },
                    { new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), "Gym equipment, bicycles, camping and fitness gear", "/images/categories/sports.svg", 0, "Sports & Equipment" }
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
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, 23, 0, new DateTime(2026, 4, 21, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(4802), null, "We are looking for an experienced software engineer to join our growing team. Must have 5+ years of experience with .NET and React. Remote-friendly with flexible hours.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 21, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(5029), false, true, 21, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0201", new DateTime(2026, 5, 10, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6161), new DateTime(2026, 4, 26, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(5921), 1, 120000.0, 90000.0, 1, " FiveToTenYears Software Engineer", 0, 2, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, 10, 2, new DateTime(2026, 4, 26, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6848), null, "Part-time customer service role for retail store. Evening and weekend shifts available. Great opportunity for students.", 1, "bhbored2022@gmail.com", 1, 0, new DateTime(2026, 5, 26, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6849), false, false, 12, null, "Tripoli", "Bourhan Hassoun", "user-2-id", "+1-555-0202", null, null, null, 22.0, 18.0, 1, "Customer Service Representative", 1, 0, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, 23, 1, new DateTime(2026, 4, 16, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6860), null, "Dynamic marketing manager needed for fast-paced startup. Lead our digital marketing efforts, manage social media, and develop brand strategy.", 3, "john@example.com", 0, 1, new DateTime(2026, 5, 16, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6860), false, false, 30, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0203", null, null, null, 90000.0, 70000.0, 1, "Marketing Manager", 1, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, 25, 0, new DateTime(2026, 4, 28, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6865), null, "Seeking talented graphic designer for ongoing project work. Must be proficient in Adobe Creative Suite. Portfolio required.", 3, "bhbored2022@gmail.com", 2, 0, new DateTime(2026, 5, 28, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6865), false, false, 13, null, "Hermel", "Bourhan Hassoun", "user-2-id", "+1-555-0204", null, null, null, 60.0, 40.0, 1, "Freelance Graphic Designer", 0, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), null, 17, 2, new DateTime(2026, 4, 11, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6903), null, "ICU nurse position at major hospital. Competitive salary, excellent benefits. Night shift differential included.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 11, 8, 11, 27, 539, DateTimeKind.Utc).AddTicks(6905), false, false, 18, null, "Zahle", "John Doe", "user-1-id", "+1-555-0205", null, null, null, 105000.0, 85000.0, 0, "Registered Nurse - ICU", 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "PickupLocations",
                columns: new[] { "Id", "Address", "DeletedAt", "IsDefault", "IsDeleted", "Label", "Lat", "Lon", "Notes", "UserId" },
                values: new object[,]
                {
                    { new Guid("113dfbb4-8768-4be8-aa7c-cca69e86a3eb"), "321 Shopping Mall, Queens, NY 11354", null, false, false, 2, 40.728200000000001, -73.794899999999998, "Near the food court entrance", "user-2-id" },
                    { new Guid("5edd95c5-e737-47eb-b483-9724ff71b5da"), "123 Main Street, New York, NY 10001", null, true, false, 0, 40.712800000000001, -74.006, "Ring the doorbell twice", "user-1-id" },
                    { new Guid("c87e626d-1b25-48d7-a822-6edbc700e9a0"), "789 Park Lane, Brooklyn, NY 11201", null, true, false, 0, 40.678199999999997, -73.944199999999995, null, "user-2-id" },
                    { new Guid("ea79e836-0db7-4fc9-9aaa-bf953a2e8606"), "456 Business Ave, New York, NY 10002", null, false, false, 1, 40.758000000000003, -73.985500000000002, "Call before arriving", "user-1-id" }
                });

            migrationBuilder.InsertData(
                table: "SearchQueries",
                columns: new[] { "Id", "Query", "SearchedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("a62f6cc5-2ec4-4ac7-9196-835e9cab22fb"), "BMW cars for sale", new DateTime(2026, 4, 26, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(4684), "user-1-id" },
                    { new Guid("ac6a718d-b905-419e-91bd-c54027c6ba54"), "iPhone 15 Pro Max", new DateTime(2026, 4, 30, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(4934), "user-1-id" },
                    { new Guid("b4329168-02ca-40b8-944c-2a4a716b6810"), "gaming laptop", new DateTime(2026, 4, 24, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(4936), "user-2-id" },
                    { new Guid("f8aa7ea2-7772-4003-afc4-c8047633dc26"), "apartment for rent in Brooklyn", new DateTime(2026, 4, 28, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(4931), "user-1-id" },
                    { new Guid("fc51de9d-5c0e-45f9-acf8-5606d6673577"), "sofa set", new DateTime(2026, 4, 29, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(4940), "user-2-id" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ItemsCount", "Name" },
                values: new object[,]
                {
                    { new Guid("00fb3880-a947-487f-ab84-e38efc218edf"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("08105009-2cb5-4c01-84f5-f4e24a7b43b2"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Professional Services" },
                    { new Guid("12ac5c22-f8f5-445e-89dc-6eaaf92d4619"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Other Fashion & Style" },
                    { new Guid("1335c4b4-a88b-4c89-a80b-3c6d0fc39537"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Transport" },
                    { new Guid("151d5505-56da-462c-b148-bb217db22c70"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Clothing For Women" },
                    { new Guid("19a08e85-ca91-4955-a85a-429cc7aef7e3"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Other Home Appliances" },
                    { new Guid("1cb43cde-4c52-4173-b97f-9fee6cfad727"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "AC Cooling & Heating" },
                    { new Guid("21d4347b-6972-4dee-ba6e-d6c6fb2eff17"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Other Items" },
                    { new Guid("235402cd-4f0d-4e95-90e6-568a3864878f"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Other Animals" },
                    { new Guid("236c4330-55a6-4fc9-89a2-0e628d8e6f63"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("241796a5-83b7-4146-be93-567aecd0a1c8"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Pet Grooming" },
                    { new Guid("25b6af6b-2a62-4286-abf5-817c7ae47dcc"), new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), null, 0, "Mobile Numbers" },
                    { new Guid("29dc99c3-0c1e-4c77-a0a2-508f7ae719a6"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Number Plates" },
                    { new Guid("2b01ae62-7cad-4f11-8a86-05eb3f1bf047"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Land For Rent" },
                    { new Guid("2eb2a5f6-650d-4395-926d-e1a30fb12cfb"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Houses For Rent" },
                    { new Guid("2f66de2d-10a0-4e34-9fe5-78473349d1ac"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("34757548-8a15-4d78-a963-6a4936cd06b2"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Vehicle Accessories" },
                    { new Guid("36ec0d1c-63a3-46ab-a8ee-33ad2512fd3c"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("374f37f8-af59-4db4-8d3b-45225c693ba8"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Bedrooms" },
                    { new Guid("385c0136-314c-4ae6-8c81-7047d506ba76"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Video Games" },
                    { new Guid("38eb77fe-5951-4102-878d-99c87a62a67b"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("39538da6-ffa2-4c5d-8423-ad5e1370893e"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Antiques & Collectibles" },
                    { new Guid("3b97b761-d217-464d-a1c9-16f758b2c6f9"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Water Sports & Diving" },
                    { new Guid("3cf0eb26-e881-4d11-a54a-e89b14a37cc1"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Bicycles & Accessories" },
                    { new Guid("3d6bb602-296c-4a0a-96b3-a2975e34c7e8"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Birds" },
                    { new Guid("40b6c98f-ba38-4ff0-b456-ff12dee6d386"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Home Services" },
                    { new Guid("4560c1d8-6618-40ef-864a-badfbe7e58e4"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("4bb2d7f0-4f0e-4ed7-a4b0-b42bf0cba5e7"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Other Furniture & Decor" },
                    { new Guid("4f479bc0-082b-44ec-b3a9-208e5f4c637a"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Toys" },
                    { new Guid("5097eb3a-aeba-4e51-afd5-40ec51446482"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Toys For Kids" },
                    { new Guid("51b4ade7-8e5b-4dd9-bd19-1c63418fd87c"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Dogs" },
                    { new Guid("53da8798-488e-42b6-a3b6-8031824e35d8"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Gym Fitness & Combat Sports" },
                    { new Guid("55699fd9-232e-4181-9e5e-f16750baa0ed"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Pet Food & Treats" },
                    { new Guid("5881f5f4-eafa-4638-8925-9a2df0909c29"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Strollers & Seats" },
                    { new Guid("64ce7848-684e-4b38-b2eb-2ef465ba805f"), new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), null, 0, "Smart Watches" },
                    { new Guid("6b71f3b2-5d9f-4c86-81e3-af66ab303b6e"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Rooms For Rent" },
                    { new Guid("6dff1bc9-a5de-439c-97f3-8d37535fb967"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("6f75458f-eb98-4672-930e-d5c2780ab9a6"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Bathrooms" },
                    { new Guid("6fe09d4c-1c8d-4b50-937d-5ac4112c4eb9"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Outdoors & Camping" },
                    { new Guid("70419c8a-9db2-4fc3-97dc-e88055b4c864"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "TV & Video" },
                    { new Guid("74931c5f-c1a0-4919-aabf-b75b6503a7c7"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("81613381-28f5-435f-8829-749af4c3fdb8"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Feeding & Nursing" },
                    { new Guid("817dbf5d-f308-4adf-9a4f-b54954a98c01"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Commercials For Sale" },
                    { new Guid("890d5ed0-bd23-4d33-9aa0-da88e25cf5f0"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("8edaf020-fd15-48d8-98ed-422d92eebeb5"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Musical Instruments" },
                    { new Guid("92455108-4c6b-4e11-b63e-0dce6512d6a5"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("95b2e9ec-c318-4635-a4e8-7603caa4f6f2"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Events" },
                    { new Guid("96effa59-ff3c-4f24-927c-864e6da7b42e"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Clothing For Men" },
                    { new Guid("a29f0104-16b9-4696-89c9-75c8d6f2ceee"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("a4812b82-9520-44fa-89f9-3cd962782b45"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Cats" },
                    { new Guid("a4d4334a-a1c7-4a3b-8585-fd6dc042df34"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Accessories For Women" },
                    { new Guid("a6d32505-e5b7-4a7b-8365-ac39bca74cdf"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Home Audio & Speakers" },
                    { new Guid("a741630b-309e-4566-8a48-0c1aee5ffe9d"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Ski & Winter Sports" },
                    { new Guid("ac4a9bec-e866-4bdd-aee4-43292364c16a"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Other Services" },
                    { new Guid("acbbfb07-91a0-47eb-a551-d7450be7b49a"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("add1d4c5-8fed-4eaf-9509-7a6202667f86"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Bathing Accessories" },
                    { new Guid("b058e559-2452-4706-bff6-e9da40e0176a"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Movies" },
                    { new Guid("b2b8f010-70df-4c2a-8e8d-954d201bd663"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("b46a892d-8587-4d47-aa5c-5444473d96fa"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Boats" },
                    { new Guid("b791419a-5048-48f1-9e11-58e3350a2e51"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Dining Rooms" },
                    { new Guid("b841c5b7-c30b-4a0d-9266-ea4fe2b29381"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Billiard & Similar Games" },
                    { new Guid("ba0677a3-5a9e-4225-ac73-d36621c97381"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Ball Sports" },
                    { new Guid("ba0be832-29bd-4531-a310-437a28eea30d"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Land For Sale" },
                    { new Guid("bbdf8074-42b8-4ac7-ab99-4639fb552b26"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Other for Kids & Babies" },
                    { new Guid("bc56f065-4d67-4924-b806-15009df2c7b5"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Houses For Sale" },
                    { new Guid("bdc7c272-826c-4b50-b6da-d59da7805e47"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("bea824b3-4144-4353-83cd-b6c4dc384922"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Cars For Sale" },
                    { new Guid("c5a8886f-5dea-4710-8eab-1b6f33d22bd2"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Living Room" },
                    { new Guid("c6f0260d-90ba-4676-a0d9-48d5ebff0fd7"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), null, 0, "Garden & Outdoors" },
                    { new Guid("c9c563ad-f3b8-408c-aa75-c872b064c2bd"), new Guid("7e917c79-782e-480d-8d76-052194601264"), null, 0, "Personal Services" },
                    { new Guid("d08e41e5-6fda-4b91-bafe-6f4f3c018591"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), null, 0, "Trucks & Buses" },
                    { new Guid("d324586b-667a-45d0-a034-5a9ec0cd21c2"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), null, 0, "Commercials For Rent" },
                    { new Guid("d3f71c37-4bb3-4d82-bbaf-c1783c386768"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Supplements & Nutrition" },
                    { new Guid("d7e3d471-2101-4b0f-8b8f-451f5cbb3043"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Pet Accessories" },
                    { new Guid("dc8fe277-1b1f-4466-a106-66710fedf8ca"), new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), null, 0, "Mobile Accessories" },
                    { new Guid("de3e269f-a74c-4f75-a20f-ec0aa8d26c40"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Games & Hobbies" },
                    { new Guid("dffef0bb-1a4c-4fa8-819b-fd81e22a8707"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Accessories For Men" },
                    { new Guid("e2f15b20-1b4e-47bc-a384-9c96f50f0d33"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Watches" },
                    { new Guid("e347d8ee-de95-4be7-ad6a-8dea91841692"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("e50e6b43-81f0-4cb5-8138-5da998b781d0"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), null, 0, "Pet Services" },
                    { new Guid("f3c09c52-0ade-4d78-9026-8d0a4b48b172"), new Guid("993ad18f-fe57-4d1c-af6b-f3e8633de96b"), null, 0, "Safety & Monitors" },
                    { new Guid("f5c9241a-9303-47a5-b73f-54228ddad823"), new Guid("e68b2c7a-3f47-464a-b5ad-315e66de10cf"), null, 0, "Other Sports" },
                    { new Guid("faab6c60-347a-494f-aa7a-10ed36871f18"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Cameras" },
                    { new Guid("fd460b43-d1ea-45ce-92c4-1746fcee7d1d"), new Guid("0abbf2d4-55e8-42d3-8d85-687f8cf62b7a"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("fd731e9d-0347-46d9-85a1-ead8fdbdc5ed"), new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), null, 0, "Mobile Phones" },
                    { new Guid("fd74c002-53ef-4c6b-9279-4121fe30157d"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), null, 0, "Cleaning Appliances" },
                    { new Guid("ff9ced80-ffcc-4694-9b36-93735b7f8419"), new Guid("0b6c8f48-6028-40ac-a576-e63d62ff04a6"), null, 0, "Books" }
                });

            migrationBuilder.InsertData(
                table: "UserActivityLogs",
                columns: new[] { "Id", "ActionType", "ActorId", "ActorRole", "Details", "DurationMs", "ErrorMessage", "IPAddress", "IsSuccess", "TargetId", "TargetType", "Timestamp", "UserAgent" },
                values: new object[,]
                {
                    { new Guid("15eec01c-8c66-4d4b-9cf6-960806526bfb"), 13, "user-1-id", "User", "Saved listing to favorites", null, null, "192.168.1.100", true, "listing-2-id", 2, new DateTime(2026, 5, 1, 6, 11, 27, 541, DateTimeKind.Utc).AddTicks(1475), null },
                    { new Guid("82f24ee2-44e4-48a7-9b76-80129de632f0"), 0, "user-1-id", "User", null, 250, null, "192.168.1.100", true, null, 0, new DateTime(2026, 4, 30, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(1207), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("a880bee5-aa17-44e2-b34f-dbde523de79c"), 16, "user-2-id", "User", "Sent message in chat", 120, null, "192.168.1.105", true, "chat-1-id", 3, new DateTime(2026, 5, 1, 3, 11, 27, 541, DateTimeKind.Utc).AddTicks(1455), null },
                    { new Guid("b0f28087-6303-49df-9dd3-901bfcc57a3c"), 8, "user-1-id", "User", "Created listing: BMW 320i", null, null, "192.168.1.100", true, "listing-1-id", 2, new DateTime(2026, 4, 21, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(1445), null },
                    { new Guid("ba307295-14d5-4970-946a-c4aca433a7f1"), 2, "user-1-id", "User", "User registered successfully", null, null, "192.168.1.100", true, "user-1-id", 1, new DateTime(2026, 4, 1, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(757), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("f5cc52ce-c9f9-421c-8039-21de87b80d31"), 2, "user-2-id", "User", "User registered successfully", null, null, "192.168.1.105", true, "user-2-id", 1, new DateTime(2026, 4, 11, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(1452), "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)" }
                });

            migrationBuilder.InsertData(
                table: "UserPreferences",
                columns: new[] { "UserId", "EmailNotifications", "Language", "PushNotifications", "Theme", "UpdatedAt" },
                values: new object[,]
                {
                    { "user-1-id", false, 0, true, 2, new DateTime(2026, 5, 1, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(4211) },
                    { "user-2-id", true, 0, true, 1, new DateTime(2026, 5, 1, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(4441) }
                });

            migrationBuilder.InsertData(
                table: "UserReports",
                columns: new[] { "Id", "ChatId", "CreatedAt", "IsResolved", "Notes", "Reason", "ReportedUserId", "ReporterId", "ResolvedAt", "ResolvedById" },
                values: new object[,]
                {
                    { new Guid("500b729f-dccd-4010-baee-762471f9426f"), null, new DateTime(2026, 4, 26, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(7779), true, "User posting duplicate listings", 0, "user-2-id", "user-1-id", new DateTime(2026, 4, 28, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(8216), null },
                    { new Guid("5cb2fa6d-c996-46b6-8b93-a502dceb335f"), null, new DateTime(2026, 4, 29, 8, 11, 27, 541, DateTimeKind.Utc).AddTicks(8484), false, "Listing contains misleading information", 3, "user-1-id", "user-2-id", null, null }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "CategoryId", "ContactMethod", "CreatedAt", "DeletedAt", "Description", "ImagesUrl", "IsDeleted", "IsPriceNegotiable", "IsPromoted", "Name", "OwnerId", "PhoneNumber", "PickupLocationId", "Price", "PrimaryImageUrl", "PromotionEndDate", "PromotionStartDate", "PromotionType", "Status", "SubcategoryId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), 1, new DateTime(2026, 4, 16, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(332), null, "Well-maintained BMW 320i with full service history. Luxury package, navigation system, leather seats, and sunroof. Single owner, garage kept.", "[\"https://example.com/images/bmw-320i-2.jpg\",\"https://example.com/images/bmw-320i-3.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0101", new Guid("5edd95c5-e737-47eb-b483-9724ff71b5da"), 45000.0, "https://example.com/images/bmw-320i-1.jpg", null, null, null, 1, new Guid("bea824b3-4144-4353-83cd-b6c4dc384922"), "BMW 320i Luxury Line 2022", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("c480a5f6-14b9-46f2-a4ce-c09e754ca477"), 1, new DateTime(2026, 4, 24, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(624), null, "Clean Toyota Camry, perfect for daily commute. Fuel-efficient, reliable, and spacious. Recent oil change and tire rotation.", "[\"https://example.com/images/toyota-camry-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0102", new Guid("c87e626d-1b25-48d7-a822-6edbc700e9a0"), 28000.0, "https://example.com/images/toyota-camry-1.jpg", null, null, null, 1, new Guid("bea824b3-4144-4353-83cd-b6c4dc384922"), "Toyota Camry 2020 - Excellent Condition", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("c4212950-0a5e-401b-9310-365f717e8f76"), 1, new DateTime(2026, 4, 28, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(649), null, "Stunning 2-bedroom apartment with city views. Fully equipped kitchen, in-unit laundry, gym and pool in building. Close to subway.", "[\"https://example.com/images/apt-manhattan-2.jpg\",\"https://example.com/images/apt-manhattan-3.jpg\",\"https://example.com/images/apt-manhattan-4.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0103", new Guid("5edd95c5-e737-47eb-b483-9724ff71b5da"), 4500.0, "https://example.com/images/apt-manhattan-1.jpg", null, null, null, 1, new Guid("bc56f065-4d67-4924-b806-15009df2c7b5"), "Modern 2BR Apartment in Manhattan", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("087008f8-4ced-47ff-a688-5f40d9593ae7"), 0, new DateTime(2026, 4, 30, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(656), null, "Like-new MacBook Pro with M2 Max chip, 32GB RAM, 1TB SSD. Perfect for professionals. AppleCare+ until 2026.", "[\"https://example.com/images/macbook-pro-2.jpg\"]", false, false, true, "Bourhan Hassoun", "user-2-id", "+1-555-0104", new Guid("c87e626d-1b25-48d7-a822-6edbc700e9a0"), 2800.0, "https://example.com/images/macbook-pro-1.jpg", new DateTime(2026, 5, 7, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(1686), new DateTime(2026, 4, 30, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(1427), 0, 1, new Guid("36ec0d1c-63a3-46ab-a8ee-33ad2512fd3c"), "MacBook Pro 16\" M2 Max 2023", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("dda98df4-74e8-4a7d-aecc-344f43f19846"), 0, new DateTime(2026, 4, 26, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(1948), null, "Comfortable L-shaped sectional sofa in excellent condition. Pet-free, smoke-free home. Easy to clean fabric.", "[\"https://example.com/images/sofa-gray-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0105", new Guid("ea79e836-0db7-4fc9-9aaa-bf953a2e8606"), 650.0, "https://example.com/images/sofa-gray-1.jpg", null, null, null, 1, new Guid("c5a8886f-5dea-4710-8eab-1b6f33d22bd2"), "IKEA Sectional Sofa - Gray", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("29773f2c-04c4-4256-8b0c-e1159945d596"), 1, new DateTime(2026, 4, 11, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(1957), new DateTime(2026, 4, 21, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(2218), "Brand new iPhone 15 Pro Max, unopened box. 256GB storage in Titanium Blue. Full Apple warranty.", "[\"https://example.com/images/iphone-15-pro-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0106", new Guid("113dfbb4-8768-4be8-aa7c-cca69e86a3eb"), 1200.0, "https://example.com/images/iphone-15-pro-1.jpg", null, null, null, 2, new Guid("fd731e9d-0347-46d9-85a1-ead8fdbdc5ed"), "iPhone 15 Pro Max 256GB - Titanium Blue", new DateTime(2026, 4, 21, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(1957) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("7e917c79-782e-480d-8d76-052194601264"), 0, new DateTime(2026, 4, 1, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(2467), null, "Licensed plumber with 15+ years experience. Emergency services available. Free estimates for all jobs.", "[]", false, true, false, "John Doe", "user-1-id", "+1-555-0107", new Guid("5edd95c5-e737-47eb-b483-9724ff71b5da"), 0.0, "https://example.com/images/plumbing-service-1.jpg", null, null, null, 1, new Guid("235402cd-4f0d-4e95-90e6-568a3864878f"), "Professional Plumbing Services", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("7165621d-ddce-458c-a34f-d3bd7aeed570"), 0, new DateTime(2026, 4, 29, 8, 11, 27, 538, DateTimeKind.Utc).AddTicks(2475), null, "Adorable Golden Retriever puppies looking for loving homes. Vaccinated, microchipped, and health checked. Parents on premises.", "[\"https://example.com/images/golden-retriever-2.jpg\",\"https://example.com/images/golden-retriever-3.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0108", new Guid("c87e626d-1b25-48d7-a822-6edbc700e9a0"), 1500.0, "https://example.com/images/golden-retriever-1.jpg", null, null, null, 1, new Guid("6f75458f-eb98-4672-930e-d5c2780ab9a6"), "Golden Retriever Puppies", null }
                });

            migrationBuilder.InsertData(
                table: "SavedListings",
                columns: new[] { "Id", "DeletedAt", "ListingId", "SavedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 4, 29, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(968), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2026, 4, 30, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(1224), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 4, 26, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(1227), "user-2-id" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 4, 30, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(1230), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 4, 28, 8, 11, 27, 540, DateTimeKind.Utc).AddTicks(1229), "user-2-id" }
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
