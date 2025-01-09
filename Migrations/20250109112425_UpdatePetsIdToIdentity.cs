using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCare.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePetsIdToIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Usunięcie istniejącej kolumny
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pets");

            // Dodanie kolumny z IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pets",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Przywrócenie kolumny bez IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pets",
                nullable: false,
                defaultValue: 0);
        }

    }
}
