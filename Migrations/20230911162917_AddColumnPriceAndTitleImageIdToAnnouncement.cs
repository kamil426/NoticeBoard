using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnnouncementsApp.Migrations
{
    public partial class AddColumnPriceAndTitleImageIdToAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Announcements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TitleImageId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "TitleImageId",
                table: "Announcements");
        }
    }
}
