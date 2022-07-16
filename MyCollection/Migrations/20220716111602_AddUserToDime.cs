using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToDime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Dime",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dime_UserId",
                schema: "dbo",
                table: "Dime",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dime_AspNetUsers_UserId",
                schema: "dbo",
                table: "Dime",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dime_AspNetUsers_UserId",
                schema: "dbo",
                table: "Dime");

            migrationBuilder.DropIndex(
                name: "IX_Dime_UserId",
                schema: "dbo",
                table: "Dime");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Dime");
        }
    }
}
