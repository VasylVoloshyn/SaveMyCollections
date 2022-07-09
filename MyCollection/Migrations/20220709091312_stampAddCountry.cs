using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class stampAddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Stamps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_CountryId",
                table: "Stamps",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stamps_Countries_CountryId",
                table: "Stamps",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stamps_Countries_CountryId",
                table: "Stamps");

            migrationBuilder.DropIndex(
                name: "IX_Stamps_CountryId",
                table: "Stamps");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Stamps");
        }
    }
}
