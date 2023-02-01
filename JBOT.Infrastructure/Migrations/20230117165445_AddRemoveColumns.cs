using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemoveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Act",
                table: "UnitTests",
                newName: "ObjectType");

            migrationBuilder.AddColumn<bool>(
                name: "IsOutput",
                table: "UnitTestParameter",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxLength",
                table: "UnitTestParameter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Precision",
                table: "UnitTestParameter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Scale",
                table: "UnitTestParameter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutput",
                table: "UnitTestParameter");

            migrationBuilder.DropColumn(
                name: "MaxLength",
                table: "UnitTestParameter");

            migrationBuilder.DropColumn(
                name: "Precision",
                table: "UnitTestParameter");

            migrationBuilder.DropColumn(
                name: "Scale",
                table: "UnitTestParameter");

            migrationBuilder.RenameColumn(
                name: "ObjectType",
                table: "UnitTests",
                newName: "Act");
        }
    }
}
