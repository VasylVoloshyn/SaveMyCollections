using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUser_ToStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Stamp",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stamp_UserId",
                schema: "dbo",
                table: "Stamp",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stamp_AspNetUsers_UserId",
                schema: "dbo",
                table: "Stamp",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stamp_AspNetUsers_UserId",
                schema: "dbo",
                table: "Stamp");

            migrationBuilder.DropIndex(
                name: "IX_Stamp_UserId",
                schema: "dbo",
                table: "Stamp");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Stamp");
        }
    }
}
