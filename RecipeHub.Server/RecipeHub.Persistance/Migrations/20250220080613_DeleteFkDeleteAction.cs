using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFkDeleteAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes",
                column: "RecipeImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_RecipeImageId",
                table: "Recipes",
                column: "RecipeImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
