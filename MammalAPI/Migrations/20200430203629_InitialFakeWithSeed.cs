using Microsoft.EntityFrameworkCore.Migrations;

namespace MammalAPI.Migrations
{
    public partial class InitialFakeWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FakeMammal",
                columns: table => new
                {
                    FakeMammalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakeMammal", x => x.FakeMammalId);
                });

            migrationBuilder.InsertData(
                table: "FakeMammal",
                columns: new[] { "FakeMammalId", "Name" },
                values: new object[] { 1, "Raninbow Whale" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FakeMammal");
        }
    }
}
