using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinPhoto_Photo_PhotoId",
                schema: "dbo",
                table: "CoinPhoto");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Coin",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coin_UserId",
                schema: "dbo",
                table: "Coin",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coin_AspNetUsers_UserId",
                schema: "dbo",
                table: "Coin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinPhoto_UserPhoto_PhotoId",
                schema: "dbo",
                table: "CoinPhoto",
                column: "PhotoId",
                principalSchema: "dbo",
                principalTable: "UserPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coin_AspNetUsers_UserId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropForeignKey(
                name: "FK_CoinPhoto_UserPhoto_PhotoId",
                schema: "dbo",
                table: "CoinPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Coin_UserId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinPhoto_Photo_PhotoId",
                schema: "dbo",
                table: "CoinPhoto",
                column: "PhotoId",
                principalSchema: "dbo",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
