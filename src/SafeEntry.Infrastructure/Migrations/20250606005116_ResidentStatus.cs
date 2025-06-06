using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeEntry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResidentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusResident",
                table: "Residents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusResident",
                table: "Residents");
        }
    }
}
