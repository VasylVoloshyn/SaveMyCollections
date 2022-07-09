using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class stampPhotoNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stamps_Photos_StampPhotoId",
                table: "Stamps");

            migrationBuilder.AlterColumn<int>(
                name: "StampPhotoId",
                table: "Stamps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stamps_Photos_StampPhotoId",
                table: "Stamps",
                column: "StampPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stamps_Photos_StampPhotoId",
                table: "Stamps");

            migrationBuilder.AlterColumn<int>(
                name: "StampPhotoId",
                table: "Stamps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stamps_Photos_StampPhotoId",
                table: "Stamps",
                column: "StampPhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
