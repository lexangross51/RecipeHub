using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFkNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Recipes",
                newName: "RecipeImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_ImageId",
                table: "Recipes",
                newName: "IX_Recipes_RecipeImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes",
                column: "RecipeImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeImageId",
                table: "Recipes",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeImageId",
                table: "Recipes",
                newName: "IX_Recipes_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
