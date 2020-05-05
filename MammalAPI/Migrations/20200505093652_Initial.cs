using Microsoft.EntityFrameworkCore.Migrations;

namespace MammalAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    FamilyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.FamilyId);
                });

            migrationBuilder.CreateTable(
                name: "Habitats",
                columns: table => new
                {
                    HabitatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitats", x => x.HabitatID);
                });

            migrationBuilder.CreateTable(
                name: "Mammals",
                columns: table => new
                {
                    MammalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    LatinName = table.Column<string>(nullable: true),
                    Lifespan = table.Column<int>(nullable: false),
                    FamilyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mammals", x => x.MammalId);
                    table.ForeignKey(
                        name: "FK_Mammals_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "FamilyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MammalHabitats",
                columns: table => new
                {
                    MammalId = table.Column<int>(nullable: false),
                    HabitatId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MammalHabitats", x => new { x.HabitatId, x.MammalId });
                    table.ForeignKey(
                        name: "FK_MammalHabitats_Habitats_HabitatId",
                        column: x => x.HabitatId,
                        principalTable: "Habitats",
                        principalColumn: "HabitatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MammalHabitats_Mammals_MammalId",
                        column: x => x.MammalId,
                        principalTable: "Mammals",
                        principalColumn: "MammalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MammalHabitats_MammalId",
                table: "MammalHabitats",
                column: "MammalId");

            migrationBuilder.CreateIndex(
                name: "IX_Mammals_FamilyId",
                table: "Mammals",
                column: "FamilyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MammalHabitats");

            migrationBuilder.DropTable(
                name: "Habitats");

            migrationBuilder.DropTable(
                name: "Mammals");

            migrationBuilder.DropTable(
                name: "Families");
        }
    }
}
