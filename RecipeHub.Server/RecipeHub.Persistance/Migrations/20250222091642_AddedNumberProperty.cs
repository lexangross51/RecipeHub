using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNumberProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "RecipeStep",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "RecipeStep");
        }
    }
}
