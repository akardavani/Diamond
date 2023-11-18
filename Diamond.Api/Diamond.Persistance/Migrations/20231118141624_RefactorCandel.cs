using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class RefactorCandel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersianDate",
                table: "Candel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersianDate",
                table: "Candel");
        }
    }
}
