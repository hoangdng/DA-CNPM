using Microsoft.EntityFrameworkCore.Migrations;

namespace PetWeb.Data.Migrations
{
    public partial class AddSubscriber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "AnimalId");

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    SubscriberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.SubscriberID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animals_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Animals_AnimalId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Animal_AnimalId",
                table: "Posts",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
