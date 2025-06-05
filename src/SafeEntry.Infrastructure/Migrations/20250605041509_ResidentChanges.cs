using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeEntry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResidentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHomeOwner",
                table: "Residents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HouseOwnerId",
                table: "Addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_HouseOwnerId",
                table: "Addresses",
                column: "HouseOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Residents_HouseOwnerId",
                table: "Addresses",
                column: "HouseOwnerId",
                principalTable: "Residents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Residents_HouseOwnerId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_HouseOwnerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsHomeOwner",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "HouseOwnerId",
                table: "Addresses");
        }
    }
}
