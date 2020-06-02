using Microsoft.EntityFrameworkCore.Migrations;

namespace PetWeb.Data.Migrations
{
    public partial class AddAnimalProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AnimalId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Posts");
        }
    }
}
