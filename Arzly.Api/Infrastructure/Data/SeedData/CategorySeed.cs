using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class CategorySeed
    {
        public static readonly List<Category> Data = new()
        {
            new Category
        {
            Id = Guid.NewGuid(),
            Name = "Vehicles",
            Description = "Cars, motorcycles, boats, trucks and accessories",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/vehicles.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Real Estate",
            Description = "Apartments, villas, land and commercial properties",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/real-estate.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Phones & Gadgets",
            Description = "Smartphones, tablets, watches and accessories",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/phones%26gadgets.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics & Appliances",
            Description = "TVs, laptops, cameras, kitchen and home appliances",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/electronics%26appliances.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Furniture & Decor",
            Description = "Home and office furniture, lighting, rugs and decor",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/furniture%26decor.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Pets",
            Description = "Dogs, cats, birds, fish and pet supplies",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/pets.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Kids & Babies",
            Description = "Toys, strollers, clothing and baby gear",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/kids%26babies.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Sports & Equipment",
            Description = "Gym equipment, bicycles, camping and fitness gear",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/sports%26equipment.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Hobbies",
            Description = "Books, music, art, collectibles and musical instruments",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/hobbies.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Fashion & Style",
            Description = "Clothing, shoes, bags, jewelry and cosmetics",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/fshion%26style.png"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Services",
            Description = "Home repair, cleaning, tutoring, moving and more",
            ImageUrl = "https://pub-05bb3464ec5d47b78fd741bfcf94d2ec.r2.dev/categories-images/services.png"
        },


        };
    }
}
