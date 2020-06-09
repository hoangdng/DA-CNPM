using Microsoft.EntityFrameworkCore.Migrations;

namespace PetWeb.Data.Migrations
{
    public partial class AddImageURLProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Posts");
        }
    }
}
