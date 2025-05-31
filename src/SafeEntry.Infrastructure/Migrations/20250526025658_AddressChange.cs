using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SafeEntry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddressChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Addresses",
                newName: "HomeNumber");

            migrationBuilder.AddColumn<int>(
                name: "CondominiumId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CondominiumId",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HomeStreet",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Condominium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Neighborhood = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condominium", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CondominiumId",
                table: "Employees",
                column: "CondominiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CondominiumId",
                table: "Addresses",
                column: "CondominiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Condominium_CondominiumId",
                table: "Addresses",
                column: "CondominiumId",
                principalTable: "Condominium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Condominium_CondominiumId",
                table: "Employees",
                column: "CondominiumId",
                principalTable: "Condominium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Condominium_CondominiumId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Condominium_CondominiumId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Condominium");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CondominiumId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CondominiumId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CondominiumId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CondominiumId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HomeStreet",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "HomeNumber",
                table: "Addresses",
                newName: "Number");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
