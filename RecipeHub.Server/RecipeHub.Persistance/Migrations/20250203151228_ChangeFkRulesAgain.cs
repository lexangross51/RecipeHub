using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFkRulesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_RecipeStep_RecipeStepId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Recipes_RecipeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RecipeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RecipeStepId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RecipeStepId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "RecipeStep",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Recipes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_ImageId",
                table: "RecipeStep",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ImageId",
                table: "Recipes",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_ImageId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_Images_ImageId",
                table: "RecipeStep");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStep_ImageId",
                table: "RecipeStep");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ImageId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeId",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeStepId",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_RecipeId",
                table: "Images",
                column: "RecipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_RecipeStepId",
                table: "Images",
                column: "RecipeStepId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_RecipeStep_RecipeStepId",
                table: "Images",
                column: "RecipeStepId",
                principalTable: "RecipeStep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Recipes_RecipeId",
                table: "Images",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
