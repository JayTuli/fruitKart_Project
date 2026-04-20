using FruitCart_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FruitCart_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "Fresh and crispy apples, rich in fiber and perfect for a healthy snack.",
                    Image = "images/apple.jpg",
                    Price = 197.99,
                    Category = "Fruit",
                    SpecialTag = "Fresh Arrival"
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Orange",
                    Description = "Juicy and tangy oranges loaded with vitamin C to boost immunity.",
                    Image = "images/orange.jpg",
                    Price = 89.99,
                    Category = "Fruit",
                    SpecialTag = "Fresh Arrival"
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Papaya",
                    Description = "Naturally sweet papaya known for aiding digestion and improving gut health.",
                    Image = "images/papaya.jpg",
                    Price = 59.99,
                    Category = "Fruit",
                    SpecialTag = "Best Seller"
                },
                new MenuItem
                {
                    Id = 4,
                    Name = "Watermelon",
                    Description = "Refreshing and hydrating watermelon, perfect for hot days.",
                    Image = "images/watermelon.jpg",
                    Price = 49.99,
                    Category = "Fruit",
                    SpecialTag = "Seasonal"
                },
                new MenuItem
                {
                    Id = 5,
                    Name = "Grapes",
                    Description = "Sweet and seedless grapes packed with antioxidants.",
                    Image = "images/grapes.jpg",
                    Price = 129.99,
                    Category = "Fruit",
                    SpecialTag = "Organic"
                },
                new MenuItem
                {
                    Id = 6,
                    Name = "Peach",
                    Description = "Soft and juicy peaches with a delightful aroma and natural sweetness.",
                    Image = "images/peach.jpg",
                    Price = 149.99,
                    Category = "Fruit",
                    SpecialTag = ""
                },
                new MenuItem
                {
                    Id = 7,
                    Name = "Alphonso Mango",
                    Description = "Premium Alphonso mangoes known for their rich flavor and creamy texture.",
                    Image = "images/alphonso_mango.jpg",
                    Price = 299.99,
                    Category = "Fruit",
                    SpecialTag = "Seasonal"
                },
                new MenuItem
                {
                    Id = 8,
                    Name = "Carrot",
                    Description = "Fresh and crunchy carrots rich in beta-carotene, great for eyesight.",
                    Image = "images/carrot.jpg",
                    Price = 39.99,
                    Category = "Vegetable",
                    SpecialTag = ""
                },
                new MenuItem
                {
                    Id = 9,
                    Name = "Pomegranate",
                    Description = "Nutrient-rich pomegranates with juicy seeds full of antioxidants.",
                    Image = "images/pomegranate.jpg",
                    Price = 179.99,
                    Category = "Fruit",
                    SpecialTag = "Best Seller"
                });
        }
    }
}
