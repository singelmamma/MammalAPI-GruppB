using Microsoft.EntityFrameworkCore.Migrations;

namespace MammalAPI.Migrations
{
    public partial class newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mammals_Families_FamilyId",
                table: "Mammals");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Mammals",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Mammals_Families_FamilyId",
                table: "Mammals",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mammals_Families_FamilyId",
                table: "Mammals");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Mammals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mammals_Families_FamilyId",
                table: "Mammals",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
