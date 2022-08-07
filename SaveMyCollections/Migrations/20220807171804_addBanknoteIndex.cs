using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class addBanknoteIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Banknote_UserId",
                schema: "dbo",
                table: "Banknote");

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_UserId_Id",
                schema: "dbo",
                table: "Banknote",
                columns: new[] { "UserId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Banknote_UserId_Id",
                schema: "dbo",
                table: "Banknote");

            migrationBuilder.CreateIndex(
                name: "IX_Banknote_UserId",
                schema: "dbo",
                table: "Banknote",
                column: "UserId");
        }
    }
}
