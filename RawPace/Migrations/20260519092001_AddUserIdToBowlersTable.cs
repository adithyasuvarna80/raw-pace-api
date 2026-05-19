using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RawPace.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToBowlersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bowlers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bowlers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
