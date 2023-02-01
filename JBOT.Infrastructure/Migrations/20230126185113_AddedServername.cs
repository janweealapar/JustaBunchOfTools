using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedServername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "UnitTests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Server",
                table: "UnitTests");
        }
    }
}
