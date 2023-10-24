﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Diamond.Persistance.Migrations
{
    [DbContext(typeof(DiamondDbContext))]
    [Migration("20230625140539_candel1")]
    partial class candel1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Diamond.Domain.Entities.Candel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstrumentId1")
                        .HasColumnType("int");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TimeFrame")
                        .HasColumnType("int");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId1");

                    b.ToTable("Candel");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .IsUnicode(false)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Board")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndustryGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndustryGroupCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentPersianName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("MarketName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.InstrumentExtraInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AllowedPriceMax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AllowedPriceMin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AverageMonthVolume")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BaseVol")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Eps")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FloatingShares")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaxWeek")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxYear")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinWeek")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinYear")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Nav")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SectorPE")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("InstrumentExtraInfo");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.InstrumentGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InstrumentGroup");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.InstrumentsEfficiency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AnnualClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("AnnualDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AnnualProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstrumentId1")
                        .HasColumnType("int");

                    b.Property<decimal>("OneMonthClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OneMonthProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OneMounthDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SixMonthsClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("SixMonthsDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SixMonthsProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ThreeMonthsClosePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ThreeMonthsDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ThreeMonthsProfit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId1");

                    b.ToTable("InstrumentsEfficiency");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.MarketSegment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MarketSegment");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.Trade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("BrokerCommission")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstrumentId1")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TradeDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("TradeTypeId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId1");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.TsePublic.TseClientType", b =>
                {
                    b.Property<string>("InsCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Buy_I_Count")
                        .HasColumnType("bigint");

                    b.Property<long>("Buy_I_Volume")
                        .HasColumnType("bigint");

                    b.Property<long>("Buy_N_Count")
                        .HasColumnType("bigint");

                    b.Property<long>("Buy_N_Volume")
                        .HasColumnType("bigint");

                    b.Property<long>("Sell_I_Count")
                        .HasColumnType("bigint");

                    b.Property<long>("Sell_I_Volume")
                        .HasColumnType("bigint");

                    b.Property<long>("Sell_N_Count")
                        .HasColumnType("bigint");

                    b.Property<long>("Sell_N_Volume")
                        .HasColumnType("bigint");

                    b.HasKey("InsCode");

                    b.ToTable("TseClientType");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.TsePublic.TseInstrument", b =>
                {
                    b.Property<long>("InsCode")
                        .HasColumnType("bigint");

                    b.Property<long>("BaseVol")
                        .HasColumnType("bigint");

                    b.Property<long>("BoardCode")
                        .HasColumnType("bigint");

                    b.Property<string>("CGdSVal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIsin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DInMar")
                        .HasColumnType("bigint");

                    b.Property<long>("Date")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeSop")
                        .HasColumnType("bigint");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Flow")
                        .HasColumnType("bigint");

                    b.Property<string>("FourDigitCompanyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentGroupId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentMnemonic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarketSegment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PSaiSMaxOkValMdv")
                        .HasColumnType("bigint");

                    b.Property<long?>("PSaiSMinOkValMdv")
                        .HasColumnType("bigint");

                    b.Property<long?>("QNmVlo")
                        .HasColumnType("bigint");

                    b.Property<long?>("QPasCotFxeVal")
                        .HasColumnType("bigint");

                    b.Property<long?>("QQtTranMarVal")
                        .HasColumnType("bigint");

                    b.Property<long?>("QtitMaxSaiOmProd")
                        .HasColumnType("bigint");

                    b.Property<long?>("QtitMinSaiOmProd")
                        .HasColumnType("bigint");

                    b.Property<string>("SectorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubSectorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Valid")
                        .HasColumnType("int");

                    b.Property<long?>("YDeComp")
                        .HasColumnType("bigint");

                    b.Property<long?>("YUniExpP")
                        .HasColumnType("bigint");

                    b.Property<long?>("YVal")
                        .HasColumnType("bigint");

                    b.Property<long?>("Yopsj")
                        .HasColumnType("bigint");

                    b.Property<long?>("ZTitad")
                        .HasColumnType("bigint");

                    b.HasKey("InsCode");

                    b.ToTable("TseInstrument");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.TsePublic.TseShareChange", b =>
                {
                    b.Property<string>("InsCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NumberOfShareNew")
                        .HasColumnType("bigint");

                    b.Property<long>("NumberOfShareOld")
                        .HasColumnType("bigint");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsCode");

                    b.ToTable("TseShareChange");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.TsePublic.TseSubSector", b =>
                {
                    b.Property<string>("SubSectorCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubSectorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubSectorCode");

                    b.ToTable("TseSubSector");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.TsePublic.TseTrade", b =>
                {
                    b.Property<long>("InsCode")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ChangePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FirstPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HEven")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LastTrade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfSharesIssued")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfTransactions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TransactionValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("YesterdayPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InsCode");

                    b.ToTable("TseTrade");
                });

            modelBuilder.Entity("Diamond.Domain.Models.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Diamond.Domain.Models.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Diamond.Identity.LoginHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IP")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTimeOffset>("LoginDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Succeed")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistories");
                });

            modelBuilder.Entity("Diamond.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Diamond.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Diamond.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Diamond.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Diamond.Identity.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Diamond.Domain.Entities.Candel", b =>
                {
                    b.HasOne("Diamond.Domain.Entities.Instrument", "Instrument")
                        .WithMany("Candels")
                        .HasForeignKey("InstrumentId1");

                    b.Navigation("Instrument");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.InstrumentsEfficiency", b =>
                {
                    b.HasOne("Diamond.Domain.Entities.Instrument", "Instrument")
                        .WithMany("InstrumentsEfficiencies")
                        .HasForeignKey("InstrumentId1");

                    b.Navigation("Instrument");
                });

            modelBuilder.Entity("Diamond.Domain.Entities.Trade", b =>
                {
                    b.HasOne("Diamond.Domain.Entities.Instrument", "Instrument")
                        .WithMany("Trades")
                        .HasForeignKey("InstrumentId1");

                    b.Navigation("Instrument");
                });

            modelBuilder.Entity("Diamond.Identity.LoginHistory", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.User", null)
                        .WithMany("LoginHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Identity.RoleClaim", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Identity.UserClaim", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Identity.UserLogin", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Identity.UserRole", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Domain.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Identity.UserToken", b =>
                {
                    b.HasOne("Diamond.Domain.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Domain.Entities.Instrument", b =>
                {
                    b.Navigation("Candels");

                    b.Navigation("InstrumentsEfficiencies");

                    b.Navigation("Trades");
                });

            modelBuilder.Entity("Diamond.Domain.Models.Identity.User", b =>
                {
                    b.Navigation("LoginHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
