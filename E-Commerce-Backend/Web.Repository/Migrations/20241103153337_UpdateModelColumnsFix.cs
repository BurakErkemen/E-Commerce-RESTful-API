using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelColumnsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_City",
                table: "UserAddresses",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_Country",
                table: "UserAddresses",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_PostCode",
                table: "UserAddresses",
                column: "PostCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Users_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Users_UserId",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_City",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_Country",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_PostCode",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAddresses");
        }
    }
}
