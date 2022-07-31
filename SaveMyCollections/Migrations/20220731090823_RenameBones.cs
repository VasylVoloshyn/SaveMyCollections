using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class RenameBones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonePhoto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Bone",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BoneGrade",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "BanknoteGrade",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknoteGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanknoteGrade_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Banknote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Nominal = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    SignatureId = table.Column<int>(type: "int", nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banknote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banknote_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Banknote_BanknoteGrade_GradeID",
                        column: x => x.GradeID,
                        principalSchema: "dbo",
                        principalTable: "BanknoteGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banknote_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banknote_Signature_SignatureId",
                        column: x => x.SignatureId,
                        principalSchema: "dbo",
                        principalTable: "Signature",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BanknotePhoto",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanknoteId = table.Column<int>(type: "int", nullable: false),
                    IsAvers = table.Column<bool>(type: "bit", nullable: false),
                    IsRevers = table.Column<bool>(type: "bit", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanknotePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanknotePhoto_Banknote_BanknoteId",
                        column: x => x.BanknoteId,
                        principalSchema: "dbo",
                        principalTable: "Banknote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanknotePhoto_UserPhoto_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "dbo",
                        principalTable: "UserPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_CurrencyId",
                schema: "dbo",
                table: "Banknote",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_GradeID",
                schema: "dbo",
                table: "Banknote",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_SignatureId",
                schema: "dbo",
                table: "Banknote",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_UserId",
                schema: "dbo",
                table: "Banknote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BanknoteGrade_UserId",
                schema: "dbo",
                table: "BanknoteGrade",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BanknotePhoto_BanknoteId",
                schema: "dbo",
                table: "BanknotePhoto",
                column: "BanknoteId");

            migrationBuilder.CreateIndex(
                name: "IX_BanknotePhoto_PhotoId",
                schema: "dbo",
                table: "BanknotePhoto",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanknotePhoto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Banknote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BanknoteGrade",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "BoneGrade",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoneGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoneGrade_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bone",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    GradeID = table.Column<int>(type: "int", nullable: false),
                    SignatureId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nominal = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bone_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bone_BoneGrade_GradeID",
                        column: x => x.GradeID,
                        principalSchema: "dbo",
                        principalTable: "BoneGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bone_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bone_Signature_SignatureId",
                        column: x => x.SignatureId,
                        principalSchema: "dbo",
                        principalTable: "Signature",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BonePhoto",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoneId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    IsAvers = table.Column<bool>(type: "bit", nullable: false),
                    IsRevers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonePhoto_Bone_BoneId",
                        column: x => x.BoneId,
                        principalSchema: "dbo",
                        principalTable: "Bone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonePhoto_UserPhoto_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "dbo",
                        principalTable: "UserPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bone_CurrencyId",
                schema: "dbo",
                table: "Bone",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bone_GradeID",
                schema: "dbo",
                table: "Bone",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bone_SignatureId",
                schema: "dbo",
                table: "Bone",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Bone_UserId",
                schema: "dbo",
                table: "Bone",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoneGrade_UserId",
                schema: "dbo",
                table: "BoneGrade",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BonePhoto_BoneId",
                schema: "dbo",
                table: "BonePhoto",
                column: "BoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BonePhoto_PhotoId",
                schema: "dbo",
                table: "BonePhoto",
                column: "PhotoId");
        }
    }
}
