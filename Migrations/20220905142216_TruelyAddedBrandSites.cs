using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class TruelyAddedBrandSites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandSite",
                table: "Phones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandSite",
                table: "Phones");
        }
    }
}
