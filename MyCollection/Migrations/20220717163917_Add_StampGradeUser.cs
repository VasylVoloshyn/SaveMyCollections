using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class Add_StampGradeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "StampGrade",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StampGrade_UserId",
                schema: "dbo",
                table: "StampGrade",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StampGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "StampGrade",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StampGrade_AspNetUsers_UserId",
                schema: "dbo",
                table: "StampGrade");

            migrationBuilder.DropIndex(
                name: "IX_StampGrade_UserId",
                schema: "dbo",
                table: "StampGrade");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "StampGrade");
        }
    }
}
