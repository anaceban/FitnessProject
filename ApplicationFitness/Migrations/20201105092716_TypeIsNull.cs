using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationFitness.Migrations
{
    public partial class TypeIsNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Types_ProgramTypeId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "Schedules",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Types_ProgramTypeId",
                table: "Schedules",
                column: "ProgramTypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Types_ProgramTypeId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "Schedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Types_ProgramTypeId",
                table: "Schedules",
                column: "ProgramTypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
