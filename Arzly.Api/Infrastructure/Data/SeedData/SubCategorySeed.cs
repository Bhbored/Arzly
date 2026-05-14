using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SubCategorySeed
    {
        public static readonly List<SubCategory> Data = new()
        {
            new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Cars For Sale",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Vehicle Accessories",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Vehicle Spare Parts",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Number Plates",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Motorcycles & ATV's",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Trucks & Buses",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[0].Id,
            Name = "Boats",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Houses For Sale",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Houses For Rent",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Commercials For Sale",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Commercials For Rent",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Land For Sale",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Land For Rent",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Chalets & Cabins For Sale",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Chalets & Cabins For Rent",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[1].Id,
            Name = "Rooms For Rent",
            Priority=8
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[2].Id,
            Name = "Mobile Phones",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[2].Id,
            Name = "Mobile Accessories",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[2].Id,
            Name = "Mobile Numbers",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[2].Id,
            Name = "Smart Watches",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "TV & Video",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Home Audio & Speakers",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Kitchen Equipment & Appliances",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "AC Cooling & Heating",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Cleaning Appliances",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Washing Machines & Dryers",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Laptops Tablets Computers",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Computer Parts & IT Accessories",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Cameras",
            Priority=8
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Gaming Consoles & Accessories",
            Priority=9
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Video Games",
            Priority=10
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[3].Id,
            Name = "Other Home Appliances",
            Priority=11
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Living Room",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Bedrooms",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Dining Rooms",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Kitchen & Kitchenware",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Bathrooms",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Home Decoration & Accessories",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Garden & Outdoors",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[4].Id,
            Name = "Other Furniture & Decor",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Pet Food & Treats",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Toys",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Pet Grooming",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Pet Accessories",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Dogs",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Cats",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Birds",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Other Animals",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[5].Id,
            Name = "Pet Services",
            Priority=8
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Toys For Kids",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Strollers & Seats",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Kids & Babies Clothing",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Cribs & Bedroom Furniture",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Bathing Accessories",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Feeding & Nursing",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Safety & Monitors",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[6].Id,
            Name = "Other for Kids & Babies",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Bicycles & Accessories",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Outdoors & Camping",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Gym Fitness & Combat Sports",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Ball Sports",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Supplements & Nutrition",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Billiard & Similar Games",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Ski & Winter Sports",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Water Sports & Diving",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Tennis & Racket Sports",
            Priority=8
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[7].Id,
            Name = "Other Sports",
            Priority=9
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Antiques & Collectibles",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Musical Instruments",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Books",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Movies",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Games & Hobbies",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[8].Id,
            Name = "Other Items",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Clothing For Men",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Accessories For Men",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Clothing For Women",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Accessories For Women",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Makeup & Cosmetics",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Jewelry & Faux-Bijou",
            Priority=5
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Watches",
            Priority=6
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[9].Id,
            Name = "Other Fashion & Style",
            Priority=7
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Home Services",
            Priority=0
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Personal Services",
            Priority=1
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Professional Services",
            Priority=2
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Events",
            Priority=3
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Transport",
            Priority=4
        },
        new SubCategory
        {
            Id = Guid.NewGuid(),
            CategoryId = CategorySeed.Data[10].Id,
            Name = "Other Services",
            Priority=5
        },
        };
    }
}
