using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyResturants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditResturantIdNameInDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Resturants_RestaurantId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Dishes",
                newName: "ResturantId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                newName: "IX_Dishes_ResturantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Resturants_ResturantId",
                table: "Dishes",
                column: "ResturantId",
                principalTable: "Resturants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Resturants_ResturantId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "ResturantId",
                table: "Dishes",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_ResturantId",
                table: "Dishes",
                newName: "IX_Dishes_RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Resturants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Resturants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
