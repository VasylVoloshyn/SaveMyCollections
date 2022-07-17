using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class CoinGrade_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "CoinGrade",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoinGrade_UserId",
                schema: "dbo",
                table: "CoinGrade",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "CoinGrade",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "CoinGrade");

            migrationBuilder.DropIndex(
                name: "IX_CoinGrade_UserId",
                schema: "dbo",
                table: "CoinGrade");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "CoinGrade");
        }
    }
}
