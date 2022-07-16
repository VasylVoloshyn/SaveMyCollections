using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Country",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_UserId",
                schema: "dbo",
                table: "Country",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_AspNetUsers_UserId",
                schema: "dbo",
                table: "Country",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_AspNetUsers_UserId",
                schema: "dbo",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_UserId",
                schema: "dbo",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Country");
        }
    }
}
