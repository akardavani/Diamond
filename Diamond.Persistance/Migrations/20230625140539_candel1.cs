using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class candel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candel_Instrument_InstrumentId",
                table: "Candel");

            migrationBuilder.DropIndex(
                name: "IX_Candel_InstrumentId",
                table: "Candel");

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentId",
                table: "Candel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "InstrumentId",
                table: "Candel",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candel_InstrumentId",
                table: "Candel",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candel_Instrument_InstrumentId",
                table: "Candel",
                column: "InstrumentId",
                principalTable: "Instrument",
                principalColumn: "Id");
        }
    }
}
