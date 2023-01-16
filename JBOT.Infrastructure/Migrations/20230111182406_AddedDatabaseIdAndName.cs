using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDatabaseIdAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DatabaseId",
                table: "UnitTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseName",
                table: "UnitTests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatabaseId",
                table: "UnitTests");

            migrationBuilder.DropColumn(
                name: "DatabaseName",
                table: "UnitTests");
        }
    }
}
