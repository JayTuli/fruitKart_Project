using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitCart_API.Migrations
{
    /// <inheritdoc />
    public partial class DataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Fresh and crispy apples, rich in fiber and perfect for a healthy snack.", "images/apple.jpg", "Apple", 197.99000000000001, "Fresh Arrival" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Juicy and tangy oranges loaded with vitamin C to boost immunity.", "images/orange.jpg", "Orange", 89.989999999999995, "Fresh Arrival" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Fruit", "Naturally sweet papaya known for aiding digestion and improving gut health.", "images/papaya.jpg", "Papaya", 59.990000000000002 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Refreshing and hydrating watermelon, perfect for hot days.", "images/watermelon.jpg", "Watermelon", 49.990000000000002, "Seasonal" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Sweet and seedless grapes packed with antioxidants.", "images/grapes.jpg", "Grapes", 129.99000000000001, "Organic" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Fruit", "Soft and juicy peaches with a delightful aroma and natural sweetness.", "images/peach.jpg", "Peach", 149.99000000000001 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Premium Alphonso mangoes known for their rich flavor and creamy texture.", "images/alphonso_mango.jpg", "Alphonso Mango", 299.99000000000001, "Seasonal" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Vegetable", "Fresh and crunchy carrots rich in beta-carotene, great for eyesight.", "images/carrot.jpg", "Carrot", 39.990000000000002 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Fruit", "Nutrient-rich pomegranates with juicy seeds full of antioxidants.", "images/pomegranate.jpg", "Pomegranate", 179.99000000000001, "Best Seller" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Appetizer", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/spring_roll.jpg", "Spring Roll", 7.9900000000000002, "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Appetizer", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/samosa.jpg", "Samosa", 8.9900000000000002, "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Appetizer", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/soup.jpg", "Soup", 8.9900000000000002 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Entrée", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/noodles.jpg", "Noodles", 10.99, "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Entrée", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/pav_bhaji.jpg", "Pav Bhaji", 12.99, "Top Rated" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Entrée", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/pizza.jpg", "Paneer Pizza", 11.99 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Dessert", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/mango_paradise.jpg", "Mango Paradise", 13.99, "Chef's Special" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Category", "Description", "Image", "Name", "Price" },
                values: new object[] { "Dessert", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/carrot_love.jpg", "Carrot Love", 4.9900000000000002 });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[] { "Dessert", "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "images/sweet_rolls.jpg", "Sweet Rolls", 4.9900000000000002, "Chef's Special" });
        }
    }
}
