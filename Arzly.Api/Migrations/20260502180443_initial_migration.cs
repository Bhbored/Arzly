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
                    { new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), "TVs, laptops, cameras, kitchen and home appliances", "/images/categories/electronics.svg", 0, "Electronics & Appliances" },
                    { new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), "Toys, strollers, clothing and baby gear", "/images/categories/kids.svg", 0, "Kids & Babies" },
                    { new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), "Apartments, villas, land and commercial properties", "/images/categories/properties.svg", 0, "Real Estate" },
                    { new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), "Books, music, art, collectibles and musical instruments", "/images/categories/hobbies.svg", 0, "Hobbies" },
                    { new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), "Dogs, cats, birds, fish and pet supplies", "/images/categories/pets.svg", 0, "Pets" },
                    { new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), "Clothing, shoes, bags, jewelry and cosmetics", "/images/categories/fashion.svg", 0, "Fashion & Style" },
                    { new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), "Cars, motorcycles, boats, trucks and accessories", "/images/categories/vehicles.svg", 0, "Vehicles" },
                    { new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), "Gym equipment, bicycles, camping and fitness gear", "/images/categories/sports.svg", 0, "Sports & Equipment" },
                    { new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), "Home and office furniture, lighting, rugs and decor", "/images/categories/furniture.svg", 0, "Furniture & Decor" },
                    { new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), "Smartphones, tablets, watches and accessories", "/images/categories/mobiles.svg", 0, "Phones & Gadgets" },
                    { new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), "Home repair, cleaning, tutoring, moving and more", "/images/categories/services.svg", 0, "Services" }
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
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, 23, 0, new DateTime(2026, 4, 22, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(2847), null, "We are looking for an experienced software engineer to join our growing team. Must have 5+ years of experience with .NET and React. Remote-friendly with flexible hours.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 22, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(3075), false, true, 21, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0201", new DateTime(2026, 5, 11, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4171), new DateTime(2026, 4, 27, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(3940), 1, 120000.0, 90000.0, 1, " FiveToTenYears Software Engineer", 0, 2, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, 10, 2, new DateTime(2026, 4, 27, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4839), null, "Part-time customer service role for retail store. Evening and weekend shifts available. Great opportunity for students.", 1, "bhbored2022@gmail.com", 1, 0, new DateTime(2026, 5, 27, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4840), false, false, 12, null, "Tripoli", "Bourhan Hassoun", "user-2-id", "+1-555-0202", null, null, null, 22.0, 18.0, 1, "Customer Service Representative", 1, 0, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, 23, 1, new DateTime(2026, 4, 17, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4845), null, "Dynamic marketing manager needed for fast-paced startup. Lead our digital marketing efforts, manage social media, and develop brand strategy.", 3, "john@example.com", 0, 1, new DateTime(2026, 5, 17, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4846), false, false, 30, null, "BintJbeilNabatieh", "John Doe", "user-1-id", "+1-555-0203", null, null, null, 90000.0, 70000.0, 1, "Marketing Manager", 1, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, 25, 0, new DateTime(2026, 4, 29, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4850), null, "Seeking talented graphic designer for ongoing project work. Must be proficient in Adobe Creative Suite. Portfolio required.", 3, "bhbored2022@gmail.com", 2, 0, new DateTime(2026, 5, 29, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4851), false, false, 13, null, "Hermel", "Bourhan Hassoun", "user-2-id", "+1-555-0204", null, null, null, 60.0, 40.0, 1, "Freelance Graphic Designer", 0, 1, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), null, 17, 2, new DateTime(2026, 4, 12, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4856), null, "ICU nurse position at major hospital. Competitive salary, excellent benefits. Night shift differential included.", 3, "john@example.com", 0, 2, new DateTime(2026, 5, 12, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(4857), false, false, 18, null, "Zahle", "John Doe", "user-1-id", "+1-555-0205", null, null, null, 105000.0, 85000.0, 0, "Registered Nurse - ICU", 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "PickupLocations",
                columns: new[] { "Id", "Address", "DeletedAt", "IsDefault", "IsDeleted", "Label", "Lat", "Lon", "Notes", "UserId" },
                values: new object[,]
                {
                    { new Guid("82c12b32-b4e8-4f29-8f2f-f699d95242f6"), "321 Shopping Mall, Queens, NY 11354", null, false, false, 2, 40.728200000000001, -73.794899999999998, "Near the food court entrance", "user-2-id" },
                    { new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), "789 Park Lane, Brooklyn, NY 11201", null, true, false, 0, 40.678199999999997, -73.944199999999995, null, "user-2-id" },
                    { new Guid("8dd0d435-c177-44d4-a2bd-9ede09ca6a8d"), "456 Business Ave, New York, NY 10002", null, false, false, 1, 40.758000000000003, -73.985500000000002, "Call before arriving", "user-1-id" },
                    { new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), "123 Main Street, New York, NY 10001", null, true, false, 0, 40.712800000000001, -74.006, "Ring the doorbell twice", "user-1-id" }
                });

            migrationBuilder.InsertData(
                table: "SearchQueries",
                columns: new[] { "Id", "Query", "SearchedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("225a23a5-3fa6-473f-934c-960dcbca4cdb"), "apartment for rent in Brooklyn", new DateTime(2026, 4, 29, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(2318), "user-1-id" },
                    { new Guid("24995ad7-db33-4f5a-b4c3-b902d2a6a588"), "BMW cars for sale", new DateTime(2026, 4, 27, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(2075), "user-1-id" },
                    { new Guid("4bc31fbe-e557-44d9-8bc8-428520ab7e2f"), "sofa set", new DateTime(2026, 4, 30, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(2329), "user-2-id" },
                    { new Guid("c3846245-61e6-46b8-9f24-a5eee88eefc7"), "gaming laptop", new DateTime(2026, 4, 25, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(2327), "user-2-id" },
                    { new Guid("d75c508e-c94b-43dc-bef6-88c94537850a"), "iPhone 15 Pro Max", new DateTime(2026, 5, 1, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(2325), "user-1-id" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Description", "ItemsCount", "Name" },
                values: new object[,]
                {
                    { new Guid("00605d53-5df9-42f1-b084-479e8bd8d087"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Safety & Monitors" },
                    { new Guid("02fdd4c9-896b-4bda-82e8-2b020d4b0cdd"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Pet Accessories" },
                    { new Guid("05758f26-2ff8-4110-b1af-76b15ad2ce2c"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Pet Grooming" },
                    { new Guid("07408a95-2014-44cd-8ec9-b89ee8a0714d"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("07ba771c-01dc-48f4-b265-35fa9e43c794"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Outdoors & Camping" },
                    { new Guid("0e04fd14-a679-4759-ae35-d7cb847aec98"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Water Sports & Diving" },
                    { new Guid("0f12200d-9906-447c-9e72-5654ad791334"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Home Audio & Speakers" },
                    { new Guid("0fc1fe6b-aa50-4990-95d2-f3277c26fe23"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Books" },
                    { new Guid("1036479c-30c1-4289-96fe-16268d9a5c73"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("13bc9dd8-b26a-434e-ae2b-c979361268a5"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Transport" },
                    { new Guid("14690cd4-603d-4aa6-8fa5-5b55d7771b11"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Personal Services" },
                    { new Guid("14dcea6c-75bf-4c47-9200-1409f8cb922a"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Houses For Rent" },
                    { new Guid("1738e952-35a5-4fd3-aa90-99209d3ee2fe"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Accessories For Men" },
                    { new Guid("18414afe-5af7-421b-91df-f829e243d566"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Ski & Winter Sports" },
                    { new Guid("248b123c-f13e-4ad1-b4ae-df159735c357"), new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), null, 0, "Smart Watches" },
                    { new Guid("24c8c5a6-0c39-471d-84e3-ef4e4295adb4"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("292b14e0-f7ba-47ae-bf3b-c4d55d3e1975"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Other Animals" },
                    { new Guid("29b181ec-ff07-4bf5-87ee-14a33a523225"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Pet Food & Treats" },
                    { new Guid("32015d00-405b-417e-a7a5-753b29d4dfcc"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Movies" },
                    { new Guid("3da8d1a9-9e77-4b05-826b-418a6014ab28"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Dogs" },
                    { new Guid("416813c1-d16f-45a0-98b3-296c87467fa0"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Vehicle Accessories" },
                    { new Guid("41f1d8bf-084d-4a4a-b66f-2a3ba795d833"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Antiques & Collectibles" },
                    { new Guid("4579e0c9-408d-4f2c-9556-320c7b0cd01c"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Land For Rent" },
                    { new Guid("46dd52d8-e94b-4745-bde7-3f744d378428"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Pet Services" },
                    { new Guid("48a793fe-3e18-41ce-a545-4102f019a7c8"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "TV & Video" },
                    { new Guid("4d13d1fd-5f36-433e-84e7-bc22ada36d46"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Land For Sale" },
                    { new Guid("4f2e70a0-a139-4026-994a-ef398b93ce00"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Bathrooms" },
                    { new Guid("50caf411-d4ec-4ecf-83d6-6156351af7b6"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Houses For Sale" },
                    { new Guid("51dc6610-86e4-40ce-b3da-02d1fab59cab"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Other Fashion & Style" },
                    { new Guid("573ec918-e903-4acf-9168-c527537d73ea"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Games & Hobbies" },
                    { new Guid("5755f8b8-2596-47b6-9208-3eecdd184e43"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Rooms For Rent" },
                    { new Guid("59d10393-db50-4378-8db0-4936f11a4000"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Living Room" },
                    { new Guid("5b7ad674-9a03-447b-affc-1f76578beaf3"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Bathing Accessories" },
                    { new Guid("5ba3fdc4-c37d-4cb7-92d3-d19e2c2ce8bf"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Clothing For Women" },
                    { new Guid("63703d83-9cba-4453-90a0-8701dfefef3f"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Cars For Sale" },
                    { new Guid("65b9904a-a829-46ab-9ae9-0a4fbfc32b97"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Trucks & Buses" },
                    { new Guid("6eac9933-8fe5-4f42-b786-c29770e26a4e"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Toys" },
                    { new Guid("704a6c17-0a15-4950-b168-ffbee1587e9d"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Clothing For Men" },
                    { new Guid("716ecdbb-085f-48a1-9f0a-4cba8e1d863a"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Cats" },
                    { new Guid("7367ea53-59e6-4396-b37f-c6954ea0d428"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Other Items" },
                    { new Guid("7837d867-26e8-44d0-91c8-85aea965f684"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Feeding & Nursing" },
                    { new Guid("7e1bdf5c-57bb-4818-b141-64be1d71ce16"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Cleaning Appliances" },
                    { new Guid("81695f40-6c5f-4ee8-91d5-1acc94e0ee19"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Garden & Outdoors" },
                    { new Guid("817bd695-954b-4ab8-816c-8a7d559fb8a8"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Home Services" },
                    { new Guid("8395b325-bc10-4d89-bc8e-16fcadc5ded7"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("863041bd-3d2b-48ad-9181-741fa9a6c5b5"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Cameras" },
                    { new Guid("8984145e-98b2-4876-ab60-3b2c6e9ba1a1"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Accessories For Women" },
                    { new Guid("8a9c2feb-49ae-48d8-9f3d-a50f105e5e63"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("8c3720d9-be2d-468c-8a5f-ceb50028ff47"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Other for Kids & Babies" },
                    { new Guid("8c52e1c4-3821-42c6-a465-73d96f2b506f"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("8d82c961-6a30-4d4f-94c8-ddad28d5052c"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Toys For Kids" },
                    { new Guid("8dd33f67-020a-4712-a721-25174743781b"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Other Services" },
                    { new Guid("8f700fff-2ec4-4751-8faa-238e58da7c63"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("91b00d0b-47ff-4e0d-9123-5359ee79be6f"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("93d77127-e505-41c1-87a7-9570e970d3e7"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Events" },
                    { new Guid("9658216f-01fe-44ee-8fac-a43a24757875"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("9a3bc8d7-64d2-4fdb-908d-9bc0727897d5"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Other Home Appliances" },
                    { new Guid("9ae180a7-45bb-4899-ae47-ac0ec9f829f9"), new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), null, 0, "Mobile Numbers" },
                    { new Guid("a220b6ba-10c4-459c-b9af-c2b5bc35bf7d"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "AC Cooling & Heating" },
                    { new Guid("a2fb01b5-807e-4479-902f-7feddf9bdd5f"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Bedrooms" },
                    { new Guid("a36cbdc4-793f-4605-acef-53985a83759d"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("a563398d-019a-432e-a422-b3d74b9805b1"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Gym Fitness & Combat Sports" },
                    { new Guid("ab4b9f6f-24f5-4f6a-92b5-d9fec3180f9a"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Commercials For Rent" },
                    { new Guid("ae04c25b-0369-400b-aac6-244f26c48a5e"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Ball Sports" },
                    { new Guid("b1b95f5f-7e0b-4535-a86c-defdbc95fc59"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), null, 0, "Strollers & Seats" },
                    { new Guid("bd5b1da4-ce54-4296-a7d2-f82056915c62"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("c2d81562-5396-40a6-a72c-439a0acd332c"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Dining Rooms" },
                    { new Guid("c351f3eb-2093-4559-b792-beaae1c63efe"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), null, 0, "Birds" },
                    { new Guid("c5df4bf6-b7f9-40f9-82a9-e09a0b3a4cf5"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Billiard & Similar Games" },
                    { new Guid("c607c7de-a20f-4fb1-aeb8-6dee9224c327"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Boats" },
                    { new Guid("cec1776e-0200-4a20-bcf3-fa1c8b7d40c7"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("d187a433-f02f-4122-8e5f-3ed383288a7b"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), null, 0, "Other Furniture & Decor" },
                    { new Guid("d7978e56-c6d2-4c9a-86b7-a66cb32964e7"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Video Games" },
                    { new Guid("d98525f3-5d21-4526-8171-c5e2d52d0e29"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), null, 0, "Watches" },
                    { new Guid("e4198007-e7e0-4776-ac64-abde1d4bf08e"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("e46010cd-b3f1-48f0-a9b7-dcc984f9bb3d"), new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), null, 0, "Mobile Accessories" },
                    { new Guid("e4837437-e57c-44e1-9e57-a0316c9e2a44"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Other Sports" },
                    { new Guid("eb0eb5a4-cd2b-4deb-8156-10a28effebda"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Bicycles & Accessories" },
                    { new Guid("eb64abf2-3999-4da5-b678-433ecebacdb2"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), null, 0, "Commercials For Sale" },
                    { new Guid("ec01b346-7988-4a4b-841b-2c4b2c8f4d53"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), null, 0, "Professional Services" },
                    { new Guid("ee844a5d-515a-4129-95e0-dedaa42bae5c"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), null, 0, "Supplements & Nutrition" },
                    { new Guid("f28319bf-b738-4f9e-a046-abad86e209a2"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("f4edc02a-2296-4017-a6f2-9d617ecf8c19"), new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), null, 0, "Mobile Phones" },
                    { new Guid("f572f69e-87d0-4c19-a526-ad32ca536c40"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("fb585fd4-e1ea-44f5-9d90-a71baa568998"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("fc4802c0-d04c-4319-8aeb-d4eb10e6201b"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), null, 0, "Number Plates" },
                    { new Guid("ffbc39d3-c175-4540-aa04-33fd26634182"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), null, 0, "Musical Instruments" }
                });

            migrationBuilder.InsertData(
                table: "UserActivityLogs",
                columns: new[] { "Id", "ActionType", "ActorId", "ActorRole", "Details", "DurationMs", "ErrorMessage", "IPAddress", "IsSuccess", "TargetId", "TargetType", "Timestamp", "UserAgent" },
                values: new object[,]
                {
                    { new Guid("22729f04-65ef-4eb3-8601-9889eda3ce5c"), 2, "user-2-id", "User", "User registered successfully", null, null, "192.168.1.105", true, "user-2-id", 1, new DateTime(2026, 4, 12, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(9058), "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)" },
                    { new Guid("4439bcc8-d06e-485c-b07c-4303fa150c1b"), 8, "user-1-id", "User", "Created listing: BMW 320i", null, null, "192.168.1.100", true, "listing-1-id", 2, new DateTime(2026, 4, 22, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(9055), null },
                    { new Guid("77613992-a79a-4817-914d-0e78dd05b2ff"), 2, "user-1-id", "User", "User registered successfully", null, null, "192.168.1.100", true, "user-1-id", 1, new DateTime(2026, 4, 2, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(8355), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("bc389765-54b6-4de8-b8ad-65006f3a6ab6"), 13, "user-1-id", "User", "Saved listing to favorites", null, null, "192.168.1.100", true, "listing-2-id", 2, new DateTime(2026, 5, 2, 16, 4, 42, 54, DateTimeKind.Utc).AddTicks(9083), null },
                    { new Guid("d40c52b1-8459-4ba4-abb7-237a45cc53ff"), 0, "user-1-id", "User", null, 250, null, "192.168.1.100", true, null, 0, new DateTime(2026, 5, 1, 18, 4, 42, 54, DateTimeKind.Utc).AddTicks(8812), "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
                    { new Guid("dca2bf14-7fee-4bae-b8ac-809231ff0e5e"), 16, "user-2-id", "User", "Sent message in chat", 120, null, "192.168.1.105", true, "chat-1-id", 3, new DateTime(2026, 5, 2, 13, 4, 42, 54, DateTimeKind.Utc).AddTicks(9061), null }
                });

            migrationBuilder.InsertData(
                table: "UserPreferences",
                columns: new[] { "UserId", "EmailNotifications", "Language", "PushNotifications", "Theme", "UpdatedAt" },
                values: new object[,]
                {
                    { "user-1-id", false, 0, true, 2, new DateTime(2026, 5, 2, 18, 4, 42, 55, DateTimeKind.Utc).AddTicks(1884) },
                    { "user-2-id", true, 0, true, 1, new DateTime(2026, 5, 2, 18, 4, 42, 55, DateTimeKind.Utc).AddTicks(2120) }
                });

            migrationBuilder.InsertData(
                table: "UserReports",
                columns: new[] { "Id", "ChatId", "CreatedAt", "IsResolved", "Notes", "Reason", "ReportedUserId", "ReporterId", "ResolvedAt", "ResolvedById" },
                values: new object[,]
                {
                    { new Guid("a104e243-7471-4d49-ab86-856131542959"), null, new DateTime(2026, 4, 30, 18, 4, 42, 55, DateTimeKind.Utc).AddTicks(6066), false, "Listing contains misleading information", 3, "user-1-id", "user-2-id", null, null },
                    { new Guid("c1d420d9-1b07-41de-b2c0-bff212ecf527"), null, new DateTime(2026, 4, 27, 18, 4, 42, 55, DateTimeKind.Utc).AddTicks(5362), true, "User posting duplicate listings", 0, "user-2-id", "user-1-id", new DateTime(2026, 4, 29, 18, 4, 42, 55, DateTimeKind.Utc).AddTicks(5803), null }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "CategoryId", "ContactMethod", "CreatedAt", "DeletedAt", "Description", "ImagesUrl", "IsDeleted", "IsPriceNegotiable", "IsPromoted", "Name", "OwnerId", "PhoneNumber", "PickupLocationId", "Price", "PrimaryImageUrl", "PromotionEndDate", "PromotionStartDate", "PromotionType", "Status", "SubcategoryId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), 1, new DateTime(2026, 4, 17, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(7621), null, "Well-maintained BMW 320i with full service history. Luxury package, navigation system, leather seats, and sunroof. Single owner, garage kept.", "[\"https://example.com/images/bmw-320i-2.jpg\",\"https://example.com/images/bmw-320i-3.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0101", new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), 45000.0, "https://example.com/images/bmw-320i-1.jpg", null, null, null, 1, new Guid("63703d83-9cba-4453-90a0-8701dfefef3f"), "BMW 320i Luxury Line 2022", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("bfaa12ee-071b-494b-85bd-26fe82e006f1"), 1, new DateTime(2026, 4, 25, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(7912), null, "Clean Toyota Camry, perfect for daily commute. Fuel-efficient, reliable, and spacious. Recent oil change and tire rotation.", "[\"https://example.com/images/toyota-camry-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0102", new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), 28000.0, "https://example.com/images/toyota-camry-1.jpg", null, null, null, 1, new Guid("63703d83-9cba-4453-90a0-8701dfefef3f"), "Toyota Camry 2020 - Excellent Condition", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("81f01cf2-2a11-4d01-a52d-a09a762590ab"), 1, new DateTime(2026, 4, 29, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(7919), null, "Stunning 2-bedroom apartment with city views. Fully equipped kitchen, in-unit laundry, gym and pool in building. Close to subway.", "[\"https://example.com/images/apt-manhattan-2.jpg\",\"https://example.com/images/apt-manhattan-3.jpg\",\"https://example.com/images/apt-manhattan-4.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0103", new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), 4500.0, "https://example.com/images/apt-manhattan-1.jpg", null, null, null, 1, new Guid("50caf411-d4ec-4ecf-83d6-6156351af7b6"), "Modern 2BR Apartment in Manhattan", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("0657ac01-221c-4b0c-bc1c-c4b8594c8dab"), 0, new DateTime(2026, 5, 1, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(7924), null, "Like-new MacBook Pro with M2 Max chip, 32GB RAM, 1TB SSD. Perfect for professionals. AppleCare+ until 2026.", "[\"https://example.com/images/macbook-pro-2.jpg\"]", false, false, true, "Bourhan Hassoun", "user-2-id", "+1-555-0104", new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), 2800.0, "https://example.com/images/macbook-pro-1.jpg", new DateTime(2026, 5, 8, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(8948), new DateTime(2026, 5, 1, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(8668), 0, 1, new Guid("07408a95-2014-44cd-8ec9-b89ee8a0714d"), "MacBook Pro 16\" M2 Max 2023", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("e0c1634c-5984-471f-b31d-fcf8089b8c29"), 0, new DateTime(2026, 4, 27, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9210), null, "Comfortable L-shaped sectional sofa in excellent condition. Pet-free, smoke-free home. Easy to clean fabric.", "[\"https://example.com/images/sofa-gray-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0105", new Guid("8dd0d435-c177-44d4-a2bd-9ede09ca6a8d"), 650.0, "https://example.com/images/sofa-gray-1.jpg", null, null, null, 1, new Guid("59d10393-db50-4378-8db0-4936f11a4000"), "IKEA Sectional Sofa - Gray", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("ed7496f6-e8fb-4f22-95a4-cc7859cdb9b0"), 1, new DateTime(2026, 4, 12, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9223), new DateTime(2026, 4, 22, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9473), "Brand new iPhone 15 Pro Max, unopened box. 256GB storage in Titanium Blue. Full Apple warranty.", "[\"https://example.com/images/iphone-15-pro-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0106", new Guid("82c12b32-b4e8-4f29-8f2f-f699d95242f6"), 1200.0, "https://example.com/images/iphone-15-pro-1.jpg", null, null, null, 2, new Guid("f4edc02a-2296-4017-a6f2-9d617ecf8c19"), "iPhone 15 Pro Max 256GB - Titanium Blue", new DateTime(2026, 4, 22, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9223) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("eda632a8-cf1c-4809-bd2a-900b0697bd32"), 0, new DateTime(2026, 4, 2, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9726), null, "Licensed plumber with 15+ years experience. Emergency services available. Free estimates for all jobs.", "[]", false, true, false, "John Doe", "user-1-id", "+1-555-0107", new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), 0.0, "https://example.com/images/plumbing-service-1.jpg", null, null, null, 1, new Guid("292b14e0-f7ba-47ae-bf3b-c4d55d3e1975"), "Professional Plumbing Services", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("932b3b87-4f94-4a26-8735-8e3ae3563309"), 0, new DateTime(2026, 4, 30, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9734), null, "Adorable Golden Retriever puppies looking for loving homes. Vaccinated, microchipped, and health checked. Parents on premises.", "[\"https://example.com/images/golden-retriever-2.jpg\",\"https://example.com/images/golden-retriever-3.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0108", new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), 1500.0, "https://example.com/images/golden-retriever-1.jpg", null, null, null, 1, new Guid("3da8d1a9-9e77-4b05-826b-418a6014ab28"), "Golden Retriever Puppies", null },
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), 1, new DateTime(2026, 4, 28, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9739), null, "Premium cotton oxford dress shirt, slim fit. Perfect for office or formal occasions. Never worn, tags attached.", "[\"https://example.com/images/oxford-shirt-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0109", new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), 45.0, "https://example.com/images/oxford-shirt-1.jpg", null, null, null, 1, new Guid("704a6c17-0a15-4950-b168-ffbee1587e9d"), "Classic Oxford Dress Shirt - Navy", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("afea5c7d-5d4c-40b1-b5f7-fa9f40212e62"), 1, new DateTime(2026, 4, 26, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9745), null, "Beautiful floral print summer dress, worn once for a photoshoot. Excellent condition, dry cleaned.", "[\"https://example.com/images/floral-dress-2.jpg\",\"https://example.com/images/floral-dress-3.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0110", new Guid("8dd0d435-c177-44d4-a2bd-9ede09ca6a8d"), 65.0, "https://example.com/images/floral-dress-1.jpg", null, null, null, 1, new Guid("5ba3fdc4-c37d-4cb7-92d3-d19e2c2ce8bf"), "Floral Summer Dress - Size M", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), 0, new DateTime(2026, 4, 24, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9750), null, "Complete newborn gift set with onesies, blankets, and bibs. Brand new, unopened. Gender neutral colors.", "[\"https://example.com/images/baby-gift-set-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0111", new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), 35.0, "https://example.com/images/baby-gift-set-1.jpg", null, null, null, 1, new Guid("8c3720d9-be2d-468c-8a5f-ceb50028ff47"), "Baby Gift Set - Newborn Essentials", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("0b746ecc-f0db-4eed-850f-de1e32b080aa"), 1, new DateTime(2026, 4, 29, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9755), null, "Lightweight foldable stroller, perfect for travel. Used for 6 months, in great condition. Includes rain cover.", "[\"https://example.com/images/yoyo-stroller-2.jpg\",\"https://example.com/images/yoyo-stroller-3.jpg\"]", false, true, false, "Bourhan Hassoun", "user-2-id", "+1-555-0112", new Guid("82c12b32-b4e8-4f29-8f2f-f699d95242f6"), 350.0, "https://example.com/images/yoyo-stroller-1.jpg", null, null, null, 1, new Guid("b1b95f5f-7e0b-4535-a86c-defdbc95fc59"), "Babyzen YOYO2 Compact Stroller", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), 0, new DateTime(2026, 4, 20, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9760), null, "Collection of 25 vintage silver coins from various countries, 1800s-1900s. Includes display case and certificates.", "[\"https://example.com/images/coin-collection-2.jpg\"]", false, false, false, "John Doe", "user-1-id", "+1-555-0113", new Guid("b8b26fdd-1cb0-4779-b90a-f876db635902"), 800.0, "https://example.com/images/coin-collection-1.jpg", null, null, null, 1, new Guid("41f1d8bf-084d-4a4a-b66f-2a3ba795d833"), "Vintage Silver Coin Collection", null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("87e3fe5a-8477-4e7d-a079-4f768232df1d"), 1, new DateTime(2026, 4, 23, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9804), null, "Fender CD-60S dreadnought acoustic guitar. Solid spruce top, walnut fingerboard. Comes with gig bag and tuner.", "[\"https://example.com/images/fender-guitar-2.jpg\",\"https://example.com/images/fender-guitar-3.jpg\"]", false, true, false, "Bourhan Hassoun", "user-2-id", "+1-555-0114", new Guid("8dd0d435-c177-44d4-a2bd-9ede09ca6a8d"), 180.0, "https://example.com/images/fender-guitar-1.jpg", null, null, null, 1, new Guid("ffbc39d3-c175-4540-aa04-33fd26634182"), "Fender Acoustic Guitar - Sunburst", null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), 0, new DateTime(2026, 4, 27, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9809), null, "Trek Marlin 7 hardtail mountain bike, 29er wheels. Hydraulic disc brakes, 1x10 drivetrain. Excellent trail condition.", "[\"https://example.com/images/trek-marlin-2.jpg\"]", false, true, false, "John Doe", "user-1-id", "+1-555-0115", new Guid("86d81967-56e8-465e-a60c-ffcc2567d41e"), 950.0, "https://example.com/images/trek-marlin-1.jpg", null, null, null, 1, new Guid("eb0eb5a4-cd2b-4deb-8156-10a28effebda"), "Trek Marlin 7 Mountain Bike 2023", null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("db614ee0-a2b6-488f-92ca-e972a3930e4e"), 1, new DateTime(2026, 4, 30, 18, 4, 42, 51, DateTimeKind.Utc).AddTicks(9814), null, "Bowflex SelectTech adjustable dumbbells, pair. Replaces 15 sets of weights. Like new, barely used.", "[\"https://example.com/images/dumbbells-2.jpg\"]", false, false, false, "Bourhan Hassoun", "user-2-id", "+1-555-0116", new Guid("82c12b32-b4e8-4f29-8f2f-f699d95242f6"), 300.0, "https://example.com/images/dumbbells-1.jpg", null, null, null, 1, new Guid("a563398d-019a-432e-a422-b3d74b9805b1"), "Adjustable Dumbbell Set 5-52.5 lbs", null }
                });

            migrationBuilder.InsertData(
                table: "BabyChildDetails",
                columns: new[] { "ListingId", "AgeRange", "Condition", "CribFurnitureType", "FeedingType", "Gender", "StrollerSeatType" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000003"), 0, 0, null, null, 0, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), null, 1, null, null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "ElectronicsDetails",
                columns: new[] { "ListingId", "AudioBrand", "CameraBrand", "CameraType", "CleaningApplianceType", "CompatibleConsole", "ComputerAccessoryType", "ComputerBrand", "ComputerColor", "ComputerScreenSize", "ComputerStorage", "ComputerType", "Condition", "CoolingHeatingType", "DisplayTechnology", "GameCondition", "GameGenre", "GamingBrand", "GamingType", "KitchenApplianceType", "Processor", "RamSize", "ScreenSize", "StorageType", "TVBrand", "TVType", "WashingMachineBrand" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), null, null, null, null, null, null, 0, 3, 6, 1024, 0, 0, null, null, null, null, null, null, null, 9, 32, null, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "FashionDetails",
                columns: new[] { "ListingId", "Condition", "CosmeticType", "JewelryMaterial", "JewelryType", "MensAccessoryType", "MensClothingType", "WatchGender", "WomensAccessoryType", "WomensClothingType" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 0, null, null, null, null, 1, null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 1, null, null, null, null, null, null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "FurnitureDetails",
                columns: new[] { "ListingId", "BathroomType", "BedroomType", "Condition", "DiningRoomType", "GardenType", "HomeDecorType", "KitchenwareType", "LivingRoomType" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), null, null, 1, null, null, null, null, 1 });

            migrationBuilder.InsertData(
                table: "HobbiesDetails",
                columns: new[] { "ListingId", "BookLanguage", "BookType", "CollectibleType", "Condition", "HobbyGameType", "InstrumentType", "MovieGenre" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000005"), null, null, 0, 1, null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), null, null, null, 0, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "PetsDetails",
                columns: new[] { "ListingId", "AnimalType", "BirdAgeGroup", "BirdSpecies", "CatAgeRange", "CatBreed", "DogAgeRange", "DogBreed", "Gender", "GroomingType", "IsVaccinated", "PetAccessoryType", "PetFoodType", "PetServiceType", "PetToyType" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), null, null, null, null, null, 0, 1, 0, null, true, null, null, null, null });

            migrationBuilder.InsertData(
                table: "PhonesDetails",
                columns: new[] { "ListingId", "AccessoryBrand", "AccessoryItemType", "BandColor", "BandMaterial", "Color", "MembershipType", "Operator", "PhoneBrand", "PhoneCondition", "Storage", "WatchBrand", "WatchStorage" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000006"), null, null, null, null, 6, null, null, 0, 0, 256, null, null });

            migrationBuilder.InsertData(
                table: "RealEstateDetails",
                columns: new[] { "ListingId", "Bathrooms", "Bedrooms", "ChaletFeatures", "ChaletType", "CommercialFeatures", "CommercialType", "Condition", "Equipped", "Features", "Floor", "Furnished", "LandType", "ListingType", "Ownership", "PropertyAge", "PropertyType", "ReferenceId", "RoomFeatures", "RoomFurnished", "Size" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), 2, 2, null, null, null, null, 0, null, "[8,14,11]", 6, 3, null, 1, 0, 0, 1, "RE-00003", null, null, 95.0 });

            migrationBuilder.InsertData(
                table: "SavedListings",
                columns: new[] { "Id", "DeletedAt", "ListingId", "SavedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 4, 30, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(8437), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2026, 5, 1, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(8687), "user-1-id" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 4, 27, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(8690), "user-2-id" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 5, 1, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(8693), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 4, 29, 18, 4, 42, 53, DateTimeKind.Utc).AddTicks(8693), "user-2-id" }
                });

            migrationBuilder.InsertData(
                table: "ServicesDetails",
                columns: new[] { "ListingId", "EventServiceType", "HomeServiceType", "OtherServiceType", "PersonalServiceType", "ProfessionalServiceType", "ServiceType", "TransportServiceType" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000007"), null, 2, null, null, null, 0, null });

            migrationBuilder.InsertData(
                table: "SportsDetails",
                columns: new[] { "ListingId", "BallSportType", "BicyclePowerType", "BicycleType", "Condition", "GameRoomType", "GymType", "OutdoorType", "RacketSportType", "SupplementBrand", "SupplementType", "WaterSportType", "WinterSportType" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000007"), null, 0, 1, 1, null, null, null, null, null, null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), null, null, null, 0, null, 0, null, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "VehiclesDetails",
                columns: new[] { "ListingId", "AccessoryType", "BoatType", "CarBrand", "CarFeatures", "Condition", "FuelType", "Kilometers", "MotorcycleType", "NumberOfDigits", "NumberOfDoors", "TransmissionType", "TruckBrand", "VehicleColor", "VehicleType", "Version", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, null, 5, "[20,25,17]", 1, 0, 25000, null, null, 4, 0, null, 0, null, "320i Luxury Line", 2022 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, null, 0, "[3,26]", 1, 0, 45000, null, null, 4, 0, null, 2, null, "Camry SE", 2020 }
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
