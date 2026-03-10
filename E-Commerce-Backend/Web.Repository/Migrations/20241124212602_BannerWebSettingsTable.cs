using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Repository.Migrations
{
    /// <inheritdoc />
    public partial class BannerWebSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressIds",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserAddresses",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "WebSiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeoKeyword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuthorUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FreeShippingThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaviconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    BannerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BannerTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BannerLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BannerDisplayFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BannerDisplayTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivate = table.Column<bool>(type: "bit", nullable: false),
                    AdminNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WebSiteSettingsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.BannerId);
                    table.ForeignKey(
                        name: "FK_Banners_WebSiteSettings_WebSiteSettingsModelId",
                        column: x => x.WebSiteSettingsModelId,
                        principalTable: "WebSiteSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_WebSiteSettingsModelId",
                table: "Banners",
                column: "WebSiteSettingsModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "WebSiteSettings");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "UserAddresses",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AddressIds",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
