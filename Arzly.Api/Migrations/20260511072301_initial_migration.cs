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
                    NumberOfOwners = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    VehicleInterior = table.Column<int>(type: "int", nullable: true),
                    AirConditioning = table.Column<int>(type: "int", nullable: true),
                    Kilometers = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    FuelConsumptionLPer100Km = table.Column<double>(type: "float", nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: true),
                    VehicleColor = table.Column<int>(type: "int", nullable: true),
                    HorsePower = table.Column<int>(type: "int", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: true),
                    NumberOfDoors = table.Column<int>(type: "int", nullable: true),
                    CarType = table.Column<int>(type: "int", nullable: true),
                    TransmissionType = table.Column<int>(type: "int", nullable: true),
                    CarFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessoryType = table.Column<int>(type: "int", nullable: true),
                    MotorcycleType = table.Column<int>(type: "int", nullable: true),
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
                    { new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), "Dogs, cats, birds, fish and pet supplies", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/pets.png", 0, "Pets" },
                    { new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), "Apartments, villas, land and commercial properties", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/real-estate.png", 0, "Real Estate" },
                    { new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), "Toys, strollers, clothing and baby gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/kids%26babies.png", 0, "Kids & Babies" },
                    { new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), "TVs, laptops, cameras, kitchen and home appliances", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/electronics%26appliances.png", 0, "Electronics & Appliances" },
                    { new Guid("60389343-b109-47f6-a93a-8f9155162a1b"), "Smartphones, tablets, watches and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/phones%26gadgets.png", 0, "Phones & Gadgets" },
                    { new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), "Gym equipment, bicycles, camping and fitness gear", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/sports%26equipment.png", 0, "Sports & Equipment" },
                    { new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), "Books, music, art, collectibles and musical instruments", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/hobbies.png", 0, "Hobbies" },
                    { new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), "Home repair, cleaning, tutoring, moving and more", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/services.png", 0, "Services" },
                    { new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), "Home and office furniture, lighting, rugs and decor", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/furniture%26decor.png", 0, "Furniture & Decor" },
                    { new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), "Clothing, shoes, bags, jewelry and cosmetics", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/fshion%26style.png", 0, "Fashion & Style" },
                    { new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), "Cars, motorcycles, boats, trucks and accessories", "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/vehicles.png", 0, "Vehicles" }
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
                    { new Guid("01d5f6b9-4637-44a0-957f-63da12b92840"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Cleaning Appliances" },
                    { new Guid("05ad0fbf-b523-4588-b0bf-c151df5e0d8c"), new Guid("60389343-b109-47f6-a93a-8f9155162a1b"), null, 0, "Mobile Numbers" },
                    { new Guid("06d9640a-baf3-4aee-bfc5-1d699ee791f5"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Commercials For Rent" },
                    { new Guid("07af5a4b-869b-47d8-b8d9-4bc0a44e72aa"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Motorcycles & ATV's" },
                    { new Guid("0ae8b0eb-d352-4aeb-a24d-cbe434be57e7"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "TV & Video" },
                    { new Guid("0bbfb5d1-6349-4e2a-94e8-80f090a7d73a"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Movies" },
                    { new Guid("0ce80d63-b4fe-4e40-b8f6-755b8f797c12"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Trucks & Buses" },
                    { new Guid("0dd631bd-edaf-4cec-89ff-036d0f74dba6"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Computer Parts & IT Accessories" },
                    { new Guid("136f428d-50dd-4c95-b4e6-531679e677d1"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Chalets & Cabins For Rent" },
                    { new Guid("14d53660-4d9a-44f2-babb-2662d91455e8"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Toys For Kids" },
                    { new Guid("1508bf18-bf35-48fa-9b22-d7760045c5f0"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Other Items" },
                    { new Guid("168f87ce-40e3-4202-9211-c26add776375"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Other for Kids & Babies" },
                    { new Guid("198e4a59-a278-402c-a64c-1285646d4794"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Events" },
                    { new Guid("1e4b5bfb-a00f-42d3-b87b-fb6149f3ea1e"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Home Audio & Speakers" },
                    { new Guid("21adf4c8-43cc-4670-9be0-43bbe6bc7ad7"), new Guid("60389343-b109-47f6-a93a-8f9155162a1b"), null, 0, "Mobile Phones" },
                    { new Guid("22e0722f-a192-4e13-b6de-df6e0ec01550"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Bicycles & Accessories" },
                    { new Guid("2ad01296-aca3-43e0-8e3a-347b78c3b744"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Video Games" },
                    { new Guid("2c98bd78-4605-4f7c-81d2-fa3351efd96b"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Feeding & Nursing" },
                    { new Guid("2d4694d3-1fb6-4e7d-b9ed-03a513937ec7"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Land For Rent" },
                    { new Guid("31e6423f-8ed2-411a-819f-900582f56d20"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Gaming Consoles & Accessories" },
                    { new Guid("34c0a4e2-a2c7-407a-8c07-2c16b3855b6a"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Water Sports & Diving" },
                    { new Guid("371a8cf8-7dd3-446d-9fd5-b28698166a70"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Kitchen & Kitchenware" },
                    { new Guid("37dda1d6-32f7-4057-924e-6def351ce696"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Accessories For Men" },
                    { new Guid("3842cda1-fa1c-44ef-b503-3359cbe20c19"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Cars For Sale" },
                    { new Guid("3889e141-1cf0-422d-aa47-b866fdbcbc53"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Jewelry & Faux-Bijou" },
                    { new Guid("3c62fd70-f40b-4908-9f27-5886bf218223"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Pet Accessories" },
                    { new Guid("3e4a21c9-3c78-47a7-b6c3-9c7154031b93"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Watches" },
                    { new Guid("3fa7ca14-9848-4827-a33c-0b634de67a4e"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "AC Cooling & Heating" },
                    { new Guid("47c16554-ae62-4c14-b7f5-029b9e37bf22"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Bathing Accessories" },
                    { new Guid("5190cca0-0fd0-400f-8e92-ae80ff149bf7"), new Guid("60389343-b109-47f6-a93a-8f9155162a1b"), null, 0, "Smart Watches" },
                    { new Guid("5475e904-2759-40bb-ad60-490ae898a416"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Houses For Sale" },
                    { new Guid("5997b795-0d8b-4f23-a870-77f92cf9d585"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Bedrooms" },
                    { new Guid("5caa8c20-d4d0-4ddb-be85-d9267bfb414f"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Kids & Babies Clothing" },
                    { new Guid("5f1ffdc7-e583-4278-b921-39fb1514cd7f"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Other Services" },
                    { new Guid("6a11d5bc-54b0-423f-b2c3-55c816216bdf"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Musical Instruments" },
                    { new Guid("6b62362b-9f45-41db-9838-02a087ca3c61"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Other Home Appliances" },
                    { new Guid("6f352136-0a3a-44d8-a3ea-feb6bf4878a2"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Vehicle Accessories" },
                    { new Guid("6f6a3b33-4efe-4ad3-bf7e-1d93d7d3686f"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Pet Food & Treats" },
                    { new Guid("72c0cbb3-32ee-44e9-9e33-4ea77a6b0950"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Other Furniture & Decor" },
                    { new Guid("76595fcc-db53-4a1a-8257-8234e79fe020"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Other Fashion & Style" },
                    { new Guid("77de3b09-bb1e-4cdb-9a00-11cb20aee274"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Ski & Winter Sports" },
                    { new Guid("7ac253d3-3c68-4ef6-9dea-44b129d62fc0"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Supplements & Nutrition" },
                    { new Guid("7fa7cedf-b80c-461b-befd-210f4d97e2bf"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Pet Services" },
                    { new Guid("84541a20-7f71-4b7c-89c0-5de17d6dd260"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Outdoors & Camping" },
                    { new Guid("85bf8b26-bd56-419b-939c-f85c654eb304"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Transport" },
                    { new Guid("86fe8a8f-b2ad-4d95-9564-35a549411ac4"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Home Services" },
                    { new Guid("90919b23-2a3c-4776-be56-15aaf2a62724"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Strollers & Seats" },
                    { new Guid("94928198-be42-4363-b33a-9a62d295d498"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Makeup & Cosmetics" },
                    { new Guid("954f3104-a25b-44a0-b199-a3fe7af95cdb"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Cameras" },
                    { new Guid("983cd4cf-44e5-4eed-ace6-e104c703b8ef"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Other Animals" },
                    { new Guid("9ac7733f-fdb5-47a8-9105-774a1344a19a"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Garden & Outdoors" },
                    { new Guid("9c46b5e5-030e-4279-be1a-427e97515ba5"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Laptops Tablets Computers" },
                    { new Guid("a0a7b7c9-a282-4cfe-b865-44d880b49542"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Pet Grooming" },
                    { new Guid("a1e88d83-7e9e-44f8-9a0b-48d3258085b3"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Home Decoration & Accessories" },
                    { new Guid("a9689c8e-b34d-4f1b-87ff-3d8f22d1d79a"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Personal Services" },
                    { new Guid("ac3a2348-a68b-4991-acbf-59fcc0bd3166"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Vehicle Spare Parts" },
                    { new Guid("ae859903-0b8b-4b80-b158-b89596fb790e"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Toys" },
                    { new Guid("af621425-61b4-4a4c-b12a-597d6a18ac0b"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Safety & Monitors" },
                    { new Guid("b0cd0865-f4db-4929-86bb-1c2fe5f68ad9"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Living Room" },
                    { new Guid("b2aa479b-8d7d-4007-a057-3db21f139cf1"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Washing Machines & Dryers" },
                    { new Guid("b314465d-8165-45aa-b38d-9c5bae95468c"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Accessories For Women" },
                    { new Guid("b70bcc13-5115-48f0-b453-b5e501233150"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Birds" },
                    { new Guid("ba9a11ed-32f0-4856-8890-b2abc82d9e32"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Billiard & Similar Games" },
                    { new Guid("bd4d31f9-bcda-48eb-83d5-82db508a919f"), new Guid("280ec89a-a20f-45f4-91fb-eed18e3b56ba"), null, 0, "Cribs & Bedroom Furniture" },
                    { new Guid("c38eccaa-9af6-40aa-a6d5-74fa72d8253e"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Books" },
                    { new Guid("c509201a-e175-4f59-a078-36b39d4fed30"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Clothing For Men" },
                    { new Guid("c53ce17e-3f57-4549-b305-1e3101d21018"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Rooms For Rent" },
                    { new Guid("c84f1651-599c-49b2-9781-fbc79c1014f2"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Games & Hobbies" },
                    { new Guid("c9722edf-0c30-4408-8264-18fb493eec89"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Other Sports" },
                    { new Guid("cef5d6cf-4f29-49f3-b4dc-82ea0df708be"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Boats" },
                    { new Guid("d59545ca-6911-4098-9505-1d6463962530"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Bathrooms" },
                    { new Guid("d693ca0f-cf0c-453d-9d42-bb54c52aebff"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Cats" },
                    { new Guid("d6bd1bc0-31f4-4b6e-bb72-ee4232a4d22a"), new Guid("9c522c39-1cc2-44a2-88ad-5f5037e7eb8d"), null, 0, "Professional Services" },
                    { new Guid("d823ca37-3da5-4306-956d-fab37b73ceeb"), new Guid("08b9f128-65d1-413b-ae94-183ee2279075"), null, 0, "Dogs" },
                    { new Guid("dc6d6f5b-1ada-42d8-bdbe-c710a2f94953"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Land For Sale" },
                    { new Guid("df07599b-ab7d-4155-8161-f2489ccd93b4"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Houses For Rent" },
                    { new Guid("e0e88653-f735-4af0-8054-8f7a7506898f"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Chalets & Cabins For Sale" },
                    { new Guid("e3050cfb-cf40-42ac-abce-0f1673d770bf"), new Guid("5516ebd4-e82b-467b-b3f5-51b32ab5d032"), null, 0, "Kitchen Equipment & Appliances" },
                    { new Guid("e42afd5b-d3d9-468e-84af-58120b4ff87f"), new Guid("8e84dac8-7c9a-4941-860a-0c867723a547"), null, 0, "Antiques & Collectibles" },
                    { new Guid("e43109ad-e0f8-41f3-92fd-3afdefb95844"), new Guid("f8fede12-48af-450f-b7b3-f67acfdbe51b"), null, 0, "Number Plates" },
                    { new Guid("ede6dba5-0fbc-427d-af10-7ef82cc16378"), new Guid("d3de0f39-c955-4275-9486-21dbb0a132a4"), null, 0, "Clothing For Women" },
                    { new Guid("f0be3b12-8240-4abc-82da-33d47179f61d"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Tennis & Racket Sports" },
                    { new Guid("f4bc258e-75c6-4317-b2e3-7047fe7b31cb"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Ball Sports" },
                    { new Guid("f6029d24-e261-478f-b946-13d97bfb23e7"), new Guid("20c7c515-b766-43dd-aee5-d0c412695a36"), null, 0, "Commercials For Sale" },
                    { new Guid("f63d25f4-8ea2-4109-843a-5d9aab3849da"), new Guid("60389343-b109-47f6-a93a-8f9155162a1b"), null, 0, "Mobile Accessories" },
                    { new Guid("fb8223dd-aa65-4ccd-8211-139a8f381918"), new Guid("d02ece08-fdc1-4e40-8967-568d8be5e006"), null, 0, "Dining Rooms" },
                    { new Guid("fee07014-1b73-4464-bae5-f46c31ce3d68"), new Guid("7d02e1ce-f08f-408b-b213-830efe85e688"), null, 0, "Gym Fitness & Combat Sports" }
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
