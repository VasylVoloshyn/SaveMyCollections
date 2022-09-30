using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class AddMaterialsUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Material",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_UserId",
                schema: "dbo",
                table: "Material",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_AspNetUsers_UserId",
                schema: "dbo",
                table: "Material",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_AspNetUsers_UserId",
                schema: "dbo",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_UserId",
                schema: "dbo",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Material");
        }
    }
}
