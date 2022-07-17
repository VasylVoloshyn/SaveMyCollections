using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Signature",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Signature_UserId",
                schema: "dbo",
                table: "Signature",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_AspNetUsers_UserId",
                schema: "dbo",
                table: "Signature",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signature_AspNetUsers_UserId",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.DropIndex(
                name: "IX_Signature_UserId",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Signature");
        }
    }
}
