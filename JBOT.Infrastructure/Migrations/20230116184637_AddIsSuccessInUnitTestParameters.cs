using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSuccessInUnitTestParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "UnitTestAssertation",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Not Equal");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Greater Than");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Greater Than Or Equal To");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Less Than Or Equal To");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_StatusId",
                table: "UnitTestAssertation",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTestAssertation_Statuses_StatusId",
                table: "UnitTestAssertation",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTestAssertation_Statuses_StatusId",
                table: "UnitTestAssertation");

            migrationBuilder.DropIndex(
                name: "IX_UnitTestAssertation_StatusId",
                table: "UnitTestAssertation");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "UnitTestAssertation");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "NotEqual");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "GreaterThan");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "GreaterThanOrEqualTo");

            migrationBuilder.UpdateData(
                table: "Operators",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "LessThanOrEqualTo");
        }
    }
}
