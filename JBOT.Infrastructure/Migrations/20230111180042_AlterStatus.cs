using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UnitTests");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "UnitTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitTests_StatusId",
                table: "UnitTests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTests_Statuses_StatusId",
                table: "UnitTests",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTests_Statuses_StatusId",
                table: "UnitTests");

            migrationBuilder.DropIndex(
                name: "IX_UnitTests_StatusId",
                table: "UnitTests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "UnitTests");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UnitTests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
