using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class CandelConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", unicode: false, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timeframe = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candel");
        }
    }
}
