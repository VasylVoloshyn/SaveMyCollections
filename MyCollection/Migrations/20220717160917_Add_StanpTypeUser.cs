using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class Add_StanpTypeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "StampType",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StampType_UserId",
                schema: "dbo",
                table: "StampType",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StampType_AspNetUsers_UserId",
                schema: "dbo",
                table: "StampType",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StampType_AspNetUsers_UserId",
                schema: "dbo",
                table: "StampType");

            migrationBuilder.DropIndex(
                name: "IX_StampType_UserId",
                schema: "dbo",
                table: "StampType");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "StampType");
        }
    }
}
