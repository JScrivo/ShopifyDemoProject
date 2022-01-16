using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopifyDemoProject.Migrations
{
    public partial class MigrationDelta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "LocationID", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 20 },
                    { 2, 1, 50 },
                    { 1, 2, 50 },
                    { 2, 2, 500 },
                    { 1, 3, 15 },
                    { 2, 3, 100 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, "124 Real Place Ave.", 2500f, "Storefront" },
                    { 2, "471 Industrial Complex Rd.", 10000f, "Warehouse" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "LocationID", "ProductID", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2.25f },
                    { 1, 2, 0.75f }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DefaultPrice", "Description", "Name", "VolPerUnit" },
                values: new object[,]
                {
                    { 2, 0.95f, "Can of Soup", "Soup", 0.5f },
                    { 3, 5f, "Box of Cereal", "Cereal", 3.5f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumns: new[] { "LocationID", "ProductID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
