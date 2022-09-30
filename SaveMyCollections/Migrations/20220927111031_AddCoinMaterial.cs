using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class AddCoinMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinMaterialId",
                schema: "dbo",
                table: "Coin",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Commemorative",
                schema: "dbo",
                table: "Coin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Coin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstDate",
                schema: "dbo",
                table: "Coin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Coin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Material",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coin_CoinMaterialId",
                schema: "dbo",
                table: "Coin",
                column: "CoinMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coin_Material_CoinMaterialId",
                schema: "dbo",
                table: "Coin",
                column: "CoinMaterialId",
                principalSchema: "dbo",
                principalTable: "Material",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coin_Material_CoinMaterialId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropTable(
                name: "Material",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Coin_CoinMaterialId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "CoinMaterialId",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Commemorative",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "FirstDate",
                schema: "dbo",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "Coin");
        }
    }
}
