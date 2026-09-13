using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeSpeciesIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_Species_SpeciesId",
                table: "Sightings");

            migrationBuilder.AlterColumn<int>(
                name: "SpeciesId",
                table: "Sightings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_Species_SpeciesId",
                table: "Sightings",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_Species_SpeciesId",
                table: "Sightings");

            migrationBuilder.AlterColumn<int>(
                name: "SpeciesId",
                table: "Sightings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_Species_SpeciesId",
                table: "Sightings",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
