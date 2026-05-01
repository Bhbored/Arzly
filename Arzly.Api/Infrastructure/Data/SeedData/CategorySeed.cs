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
            ImageUrl = "/images/categories/vehicles.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Real Estate",
            Description = "Apartments, villas, land and commercial properties",
            ImageUrl = "/images/categories/properties.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Phones & Gadgets",
            Description = "Smartphones, tablets, watches and accessories",
            ImageUrl = "/images/categories/mobiles.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics & Appliances",
            Description = "TVs, laptops, cameras, kitchen and home appliances",
            ImageUrl = "/images/categories/electronics.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Furniture & Decor",
            Description = "Home and office furniture, lighting, rugs and decor",
            ImageUrl = "/images/categories/furniture.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Pets",
            Description = "Dogs, cats, birds, fish and pet supplies",
            ImageUrl = "/images/categories/pets.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Kids & Babies",
            Description = "Toys, strollers, clothing and baby gear",
            ImageUrl = "/images/categories/kids.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Sports & Equipment",
            Description = "Gym equipment, bicycles, camping and fitness gear",
            ImageUrl = "/images/categories/sports.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Hobbies",
            Description = "Books, music, art, collectibles and musical instruments",
            ImageUrl = "/images/categories/hobbies.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Fashion & Style",
            Description = "Clothing, shoes, bags, jewelry and cosmetics",
            ImageUrl = "/images/categories/fashion.svg"
        },
        new Category
        {
            Id = Guid.NewGuid(),
            Name = "Services",
            Description = "Home repair, cleaning, tutoring, moving and more",
            ImageUrl = "/images/categories/services.svg"
        },


        };
    }
}
