using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUserToBoneGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "BoneGrade",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoneGrade_UserId",
                schema: "dbo",
                table: "BoneGrade",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoneGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "BoneGrade",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoneGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "BoneGrade");

            migrationBuilder.DropIndex(
                name: "IX_BoneGrade_UserId",
                schema: "dbo",
                table: "BoneGrade");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "BoneGrade");
        }
    }
}
