using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationFitness.Infrastracture.Migrations
{
    public partial class UpdateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersPrograms",
                table: "UsersPrograms");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersPrograms",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersPrograms",
                table: "UsersPrograms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPrograms_UserId",
                table: "UsersPrograms",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersPrograms",
                table: "UsersPrograms");

            migrationBuilder.DropIndex(
                name: "IX_UsersPrograms_UserId",
                table: "UsersPrograms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersPrograms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersPrograms",
                table: "UsersPrograms",
                columns: new[] { "UserId", "ProgramScheduleId" });
        }
    }
}
