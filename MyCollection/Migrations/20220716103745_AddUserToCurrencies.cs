using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToCurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Currency",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_UserId",
                schema: "dbo",
                table: "Currency",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_AspNetUsers_UserId",
                schema: "dbo",
                table: "Currency",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_AspNetUsers_UserId",
                schema: "dbo",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Currency_UserId",
                schema: "dbo",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Currency");
        }
    }
}
