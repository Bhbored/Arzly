using Arzly.Api.Domain.Entities.Listings;



namespace Arzly.Api.Infrastructure.Data.SeedData

{

    public static class SubCategorySeed

    {

        public static readonly List<SubCategory> Data = new()

        {

            new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000001"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Cars For Sale",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000002"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Vehicle Accessories",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000003"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Vehicle Spare Parts",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000004"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Number Plates",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000005"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Motorcycles & ATV's",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000006"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Trucks & Buses",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000007"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001"),

            Name = "Boats",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000008"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Houses For Sale",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000009"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Houses For Rent",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000010"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Commercials For Sale",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000011"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Commercials For Rent",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000012"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Land For Sale",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000013"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Land For Rent",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000014"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Chalets & Cabins For Sale",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000015"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Chalets & Cabins For Rent",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000016"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000002"),

            Name = "Rooms For Rent",

            Priority=8

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000017"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000003"),

            Name = "Mobile Phones",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000018"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000003"),

            Name = "Mobile Accessories",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000019"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000003"),

            Name = "Mobile Numbers",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000020"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000003"),

            Name = "Smart Watches",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000021"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "TV & Video",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000022"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Home Audio & Speakers",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000023"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Kitchen Equipment & Appliances",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000024"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "AC Cooling & Heating",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000025"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Cleaning Appliances",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000026"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Washing Machines & Dryers",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000027"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Laptops Tablets Computers",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000028"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Computer Parts & IT Accessories",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000029"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Cameras",

            Priority=8

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000030"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Gaming Consoles & Accessories",

            Priority=9

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000031"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Video Games",

            Priority=10

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000032"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000004"),

            Name = "Other Home Appliances",

            Priority=11

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000033"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Living Room",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000034"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Bedrooms",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000035"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Dining Rooms",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000036"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Kitchen & Kitchenware",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000037"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Bathrooms",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000038"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Home Decoration & Accessories",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000039"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Garden & Outdoors",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000040"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000005"),

            Name = "Other Furniture & Decor",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000041"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Pet Food & Treats",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000042"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Toys",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000043"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Pet Grooming",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000044"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Pet Accessories",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000045"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Dogs",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000046"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Cats",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000047"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Birds",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000048"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Other Animals",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000049"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000006"),

            Name = "Pet Services",

            Priority=8

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000050"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Toys For Kids",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000051"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Strollers & Seats",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000052"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Kids & Babies Clothing",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000053"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Cribs & Bedroom Furniture",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000054"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Bathing Accessories",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000055"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Feeding & Nursing",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000056"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Safety & Monitors",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000057"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000007"),

            Name = "Other for Kids & Babies",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000058"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Bicycles & Accessories",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000059"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Outdoors & Camping",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000060"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Gym Fitness & Combat Sports",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000061"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Ball Sports",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000062"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Supplements & Nutrition",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000063"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Billiard & Similar Games",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000064"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Ski & Winter Sports",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000065"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Water Sports & Diving",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000066"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Tennis & Racket Sports",

            Priority=8

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000067"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000008"),

            Name = "Other Sports",

            Priority=9

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000068"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Antiques & Collectibles",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000069"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Musical Instruments",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000070"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Books",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000071"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Movies",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000072"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Games & Hobbies",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000073"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000009"),

            Name = "Other Items",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000074"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Clothing For Men",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000075"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Accessories For Men",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000076"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Clothing For Women",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000077"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Accessories For Women",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000078"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Makeup & Cosmetics",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000079"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Jewelry & Faux-Bijou",

            Priority=5

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000080"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Watches",

            Priority=6

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000081"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000A"),

            Name = "Other Fashion & Style",

            Priority=7

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000082"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Home Services",

            Priority=0

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000083"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Personal Services",

            Priority=1

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000084"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Professional Services",

            Priority=2

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000085"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Events",

            Priority=3

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000086"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Transport",

            Priority=4

        },

        new SubCategory

        {

            Id = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000087"),
            CategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-00000000000B"),

            Name = "Other Services",

            Priority=5

        },

        };

    }

}


