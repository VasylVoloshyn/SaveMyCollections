using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveMyCollections.Migrations
{
    public partial class addPrevFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrevFileName",
                schema: "dbo",
                table: "UserPhoto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevFileName",
                schema: "dbo",
                table: "UserPhoto");
        }
    }
}
