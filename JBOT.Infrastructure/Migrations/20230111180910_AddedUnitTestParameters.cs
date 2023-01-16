using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnitTestParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arrange",
                table: "UnitTests");

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "UnitTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UnitTestParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitTestId = table.Column<int>(type: "int", nullable: true),
                    RecordUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTestParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitTestParameter_UnitTests_UnitTestId",
                        column: x => x.UnitTestId,
                        principalTable: "UnitTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestParameter_UnitTestId",
                table: "UnitTestParameter",
                column: "UnitTestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitTestParameter");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "UnitTests");

            migrationBuilder.AddColumn<string>(
                name: "Arrange",
                table: "UnitTests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
