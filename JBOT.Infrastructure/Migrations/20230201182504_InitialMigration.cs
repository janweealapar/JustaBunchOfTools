using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatabaseId = table.Column<int>(type: "int", nullable: false),
                    DatabaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    RecordUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitTests_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitTestParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: false),
                    Precision = table.Column<int>(type: "int", nullable: false),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    IsOutput = table.Column<bool>(type: "bit", nullable: false),
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
                    StatusId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_UnitTestAssertation_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
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

            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Equal" },
                    { 2, "Not Equal" },
                    { 3, "Greater Than" },
                    { 4, "Greater Than Or Equal To" },
                    { 5, "LessThan" },
                    { 6, "Less Than Or Equal To" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Failed" },
                    { 2, "Success" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_OperatorId",
                table: "UnitTestAssertation",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_StatusId",
                table: "UnitTestAssertation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_UnitTestId",
                table: "UnitTestAssertation",
                column: "UnitTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestAssertation_UnitTestParameterId",
                table: "UnitTestAssertation",
                column: "UnitTestParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTestParameter_UnitTestId",
                table: "UnitTestParameter",
                column: "UnitTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTests_StatusId",
                table: "UnitTests",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitTestAssertation");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "UnitTestParameter");

            migrationBuilder.DropTable(
                name: "UnitTests");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
