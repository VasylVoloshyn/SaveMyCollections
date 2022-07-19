using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddUser_Bone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonePhoto_Photo_PhotoId",
                schema: "dbo",
                table: "BonePhoto");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Bone",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bone_UserId",
                schema: "dbo",
                table: "Bone",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bone_AspNetUsers_UserId",
                schema: "dbo",
                table: "Bone",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhoto_UserPhoto_PhotoId",
                schema: "dbo",
                table: "BonePhoto",
                column: "PhotoId",
                principalSchema: "dbo",
                principalTable: "UserPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bone_AspNetUsers_UserId",
                schema: "dbo",
                table: "Bone");

            migrationBuilder.DropForeignKey(
                name: "FK_BonePhoto_UserPhoto_PhotoId",
                schema: "dbo",
                table: "BonePhoto");

            migrationBuilder.DropIndex(
                name: "IX_Bone_UserId",
                schema: "dbo",
                table: "Bone");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Bone");

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhoto_Photo_PhotoId",
                schema: "dbo",
                table: "BonePhoto",
                column: "PhotoId",
                principalSchema: "dbo",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
