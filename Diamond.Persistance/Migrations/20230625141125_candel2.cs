using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class candel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candel_Instrument_InstrumentId1",
                table: "Candel");

            migrationBuilder.DropIndex(
                name: "IX_Candel_InstrumentId1",
                table: "Candel");

            migrationBuilder.DropColumn(
                name: "InstrumentId1",
                table: "Candel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstrumentId1",
                table: "Candel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candel_InstrumentId1",
                table: "Candel",
                column: "InstrumentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Candel_Instrument_InstrumentId1",
                table: "Candel",
                column: "InstrumentId1",
                principalTable: "Instrument",
                principalColumn: "Id");
        }
    }
}
