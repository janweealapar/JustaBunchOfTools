using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssertations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assert",
                table: "UnitTests");

            migrationBuilder.CreateTable(
                name: "UnitTestAssertation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitTestId = table.Column<int>(type: "int", nullable: true),
                    UnitTestParameterId = table.Column<int>(type: "int", nullable: true),
                    ExpectedValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorId = table.Column<int>(type: "int", nullable: true),
                    ActualValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTestAssertation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitTestAssertation_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitTestAssertation_UnitTestParameter_UnitTestParameterId",
                        column: x => x.UnitTestParameterId,
                        principalTable: "UnitTestParameter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitTestAssertation_UnitTests_UnitTestId",
                        column: x => x.UnitTestId,
                        principalTable: "UnitTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_OperatorId",
                table: "UnitTestAssertation",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_UnitTestId",
                table: "UnitTestAssertation",
                column: "UnitTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_UnitTestParameterId",
                table: "UnitTestAssertation",
                column: "UnitTestParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitTestAssertation");

            migrationBuilder.AddColumn<bool>(
                name: "Assert",
                table: "UnitTests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
