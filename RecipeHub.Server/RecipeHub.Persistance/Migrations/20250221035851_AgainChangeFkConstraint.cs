using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgainChangeFkConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes",
                column: "RecipeImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes",
                column: "RecipeImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
