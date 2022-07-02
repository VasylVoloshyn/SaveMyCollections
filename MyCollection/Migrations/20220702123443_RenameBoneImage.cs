using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class RenameBoneImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoneImage");

            migrationBuilder.CreateTable(
                name: "BonePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoneId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonePhoto_Bones_BoneId",
                        column: x => x.BoneId,
                        principalTable: "Bones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonePhoto_Images_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonePhoto_BoneId",
                table: "BonePhoto",
                column: "BoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BonePhoto_PhotoId",
                table: "BonePhoto",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonePhoto");

            migrationBuilder.CreateTable(
                name: "BoneImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoneId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoneImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoneImage_Bones_BoneId",
                        column: x => x.BoneId,
                        principalTable: "Bones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoneImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoneImage_BoneId",
                table: "BoneImage",
                column: "BoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BoneImage_ImageId",
                table: "BoneImage",
                column: "ImageId");
        }
    }
}
