using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class candel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeFrame = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candel_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candel_InstrumentId",
                table: "Candel",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candel");
        }
    }
}
