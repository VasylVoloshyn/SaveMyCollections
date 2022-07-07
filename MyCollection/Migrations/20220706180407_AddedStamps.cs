using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    public partial class AddedStamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonePhoto_Bones_BoneId",
                table: "BonePhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_BonePhoto_Photos_PhotoId",
                table: "BonePhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Bones_Currency_CurrencyId",
                table: "Bones");

            migrationBuilder.DropForeignKey(
                name: "FK_Bones_Grades_GradeID",
                table: "Bones");

            migrationBuilder.DropForeignKey(
                name: "FK_Currency_Countries_CountryId",
                table: "Currency");

            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_Person_PersonId",
                table: "Signatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonePhoto",
                table: "BonePhoto");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "BoneGrades");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "BonePhoto",
                newName: "BonePhotos");

            migrationBuilder.RenameIndex(
                name: "IX_Currency_CountryId",
                table: "Currencies",
                newName: "IX_Currencies_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_BonePhoto_PhotoId",
                table: "BonePhotos",
                newName: "IX_BonePhotos_PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_BonePhoto_BoneId",
                table: "BonePhotos",
                newName: "IX_BonePhotos_BoneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoneGrades",
                table: "BoneGrades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonePhotos",
                table: "BonePhotos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StampGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StampGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StampTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StampTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stamps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    DimeId = table.Column<int>(type: "int", nullable: true),
                    Nominal = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    StampGradeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StampPhotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stamps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stamps_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stamps_Dimes_DimeId",
                        column: x => x.DimeId,
                        principalTable: "Dimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stamps_Photos_StampPhotoId",
                        column: x => x.StampPhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stamps_StampGrades_StampGradeId",
                        column: x => x.StampGradeId,
                        principalTable: "StampGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_CurrencyId",
                table: "Stamps",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_DimeId",
                table: "Stamps",
                column: "DimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_StampGradeId",
                table: "Stamps",
                column: "StampGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_StampPhotoId",
                table: "Stamps",
                column: "StampPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhotos_Bones_BoneId",
                table: "BonePhotos",
                column: "BoneId",
                principalTable: "Bones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhotos_Photos_PhotoId",
                table: "BonePhotos",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bones_BoneGrades_GradeID",
                table: "Bones",
                column: "GradeID",
                principalTable: "BoneGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bones_Currencies_CurrencyId",
                table: "Bones",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Countries_CountryId",
                table: "Currencies",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_Persons_PersonId",
                table: "Signatures",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonePhotos_Bones_BoneId",
                table: "BonePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_BonePhotos_Photos_PhotoId",
                table: "BonePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Bones_BoneGrades_GradeID",
                table: "Bones");

            migrationBuilder.DropForeignKey(
                name: "FK_Bones_Currencies_CurrencyId",
                table: "Bones");

            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Countries_CountryId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Signatures_Persons_PersonId",
                table: "Signatures");

            migrationBuilder.DropTable(
                name: "Stamps");

            migrationBuilder.DropTable(
                name: "StampTypes");

            migrationBuilder.DropTable(
                name: "StampGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonePhotos",
                table: "BonePhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoneGrades",
                table: "BoneGrades");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "BonePhotos",
                newName: "BonePhoto");

            migrationBuilder.RenameTable(
                name: "BoneGrades",
                newName: "Grades");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_CountryId",
                table: "Currency",
                newName: "IX_Currency_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_BonePhotos_PhotoId",
                table: "BonePhoto",
                newName: "IX_BonePhoto_PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_BonePhotos_BoneId",
                table: "BonePhoto",
                newName: "IX_BonePhoto_BoneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonePhoto",
                table: "BonePhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhoto_Bones_BoneId",
                table: "BonePhoto",
                column: "BoneId",
                principalTable: "Bones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonePhoto_Photos_PhotoId",
                table: "BonePhoto",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bones_Currency_CurrencyId",
                table: "Bones",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bones_Grades_GradeID",
                table: "Bones",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_Countries_CountryId",
                table: "Currency",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Signatures_Person_PersonId",
                table: "Signatures",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
