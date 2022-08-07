using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class addBanknoteCurrencyIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Banknote_UserId_CurrencyId",
                schema: "dbo",
                table: "Banknote",
                columns: new[] { "UserId", "CurrencyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Banknote_UserId_CurrencyId",
                schema: "dbo",
                table: "Banknote");
        }
    }
}
