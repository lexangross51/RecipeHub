using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFkConstraint1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_Recipes_Id",
                table: "RecipeStep");

            migrationBuilder.AddColumn<string>(
                name: "RecipeId",
                table: "RecipeStep",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_RecipeId",
                table: "RecipeStep",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_Recipes_RecipeId",
                table: "RecipeStep",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeStep_Recipes_RecipeId",
                table: "RecipeStep");

            migrationBuilder.DropIndex(
                name: "IX_RecipeStep_RecipeId",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeStep");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeStep_Recipes_Id",
                table: "RecipeStep",
                column: "Id",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
