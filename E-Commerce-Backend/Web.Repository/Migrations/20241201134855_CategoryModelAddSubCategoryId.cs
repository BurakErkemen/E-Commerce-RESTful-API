using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CategoryModelAddSubCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Categories");
        }
    }
}
