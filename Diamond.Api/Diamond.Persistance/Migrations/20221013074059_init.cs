using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentPersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentExtraInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SectorPE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nav = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseVol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllowedPriceMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllowedPriceMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AverageMonthVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FloatingShares = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentExtraInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSegment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TseClientType",
                columns: table => new
                {
                    InsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Buy_I_Count = table.Column<long>(type: "bigint", nullable: false),
                    Buy_N_Count = table.Column<long>(type: "bigint", nullable: false),
                    Buy_I_Volume = table.Column<long>(type: "bigint", nullable: false),
                    Buy_N_Volume = table.Column<long>(type: "bigint", nullable: false),
                    Sell_I_Count = table.Column<long>(type: "bigint", nullable: false),
                    Sell_N_Count = table.Column<long>(type: "bigint", nullable: false),
                    Sell_I_Volume = table.Column<long>(type: "bigint", nullable: false),
                    Sell_N_Volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseClientType", x => x.InsCode);
                });

            migrationBuilder.CreateTable(
                name: "TseInstrument",
                columns: table => new
                {
                    InsCode = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentMnemonic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FourDigitCompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIsin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QNmVlo = table.Column<long>(type: "bigint", nullable: true),
                    ZTitad = table.Column<long>(type: "bigint", nullable: true),
                    DeSop = table.Column<long>(type: "bigint", nullable: true),
                    Yopsj = table.Column<long>(type: "bigint", nullable: true),
                    CGdSVal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DInMar = table.Column<long>(type: "bigint", nullable: true),
                    YUniExpP = table.Column<long>(type: "bigint", nullable: true),
                    MarketSegment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardCode = table.Column<long>(type: "bigint", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSectorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YDeComp = table.Column<long>(type: "bigint", nullable: true),
                    PSaiSMaxOkValMdv = table.Column<long>(type: "bigint", nullable: true),
                    PSaiSMinOkValMdv = table.Column<long>(type: "bigint", nullable: true),
                    BaseVol = table.Column<long>(type: "bigint", nullable: false),
                    YVal = table.Column<long>(type: "bigint", nullable: true),
                    QPasCotFxeVal = table.Column<long>(type: "bigint", nullable: true),
                    QQtTranMarVal = table.Column<long>(type: "bigint", nullable: true),
                    Flow = table.Column<long>(type: "bigint", nullable: false),
                    QtitMinSaiOmProd = table.Column<long>(type: "bigint", nullable: true),
                    QtitMaxSaiOmProd = table.Column<long>(type: "bigint", nullable: true),
                    Valid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseInstrument", x => x.InsCode);
                });

            migrationBuilder.CreateTable(
                name: "TseShareChange",
                columns: table => new
                {
                    InsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfShareOld = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfShareNew = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseShareChange", x => x.InsCode);
                });

            migrationBuilder.CreateTable(
                name: "TseSubSector",
                columns: table => new
                {
                    SubSectorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseSubSector", x => x.SubSectorCode);
                });

            migrationBuilder.CreateTable(
                name: "TseTrade",
                columns: table => new
                {
                    InsCode = table.Column<long>(type: "bigint", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastTrade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfTransactions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSharesIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YesterdayPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEven = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseTrade", x => x.InsCode);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentsEfficiency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OneMounthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThreeMonthsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SixMonthsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnualDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneMonthClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThreeMonthsClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SixMonthsClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneMonthProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThreeMonthsProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SixMonthsProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstrumentId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentsEfficiency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentsEfficiency_Instrument_InstrumentId1",
                        column: x => x.InstrumentId1,
                        principalTable: "Instrument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TradeTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrokerCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstrumentId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_Instrument_InstrumentId1",
                        column: x => x.InstrumentId1,
                        principalTable: "Instrument",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Succeed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentsEfficiency_InstrumentId1",
                table: "InstrumentsEfficiency",
                column: "InstrumentId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_UserId",
                table: "LoginHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_InstrumentId1",
                table: "Trade",
                column: "InstrumentId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentExtraInfo");

            migrationBuilder.DropTable(
                name: "InstrumentGroup");

            migrationBuilder.DropTable(
                name: "InstrumentsEfficiency");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "MarketSegment");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "TseClientType");

            migrationBuilder.DropTable(
                name: "TseInstrument");

            migrationBuilder.DropTable(
                name: "TseShareChange");

            migrationBuilder.DropTable(
                name: "TseSubSector");

            migrationBuilder.DropTable(
                name: "TseTrade");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
