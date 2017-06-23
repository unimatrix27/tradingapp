using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Trading.Migrations
{
    public partial class AddSymbolsInstruments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(maxLength: 2, nullable: true),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 3, nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    DurationTicks = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeIntervals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerId = table.Column<int>(nullable: false),
                    CloseTime = table.Column<TimeSpan>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OpenTime = table.Column<TimeSpan>(nullable: false),
                    TimeZoneId = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchanges_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exchanges_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrokerSymbols",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ExchangeId = table.Column<int>(nullable: false),
                    InstrumentNameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerSymbols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerSymbols_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerSymbols_InstrumentNames_InstrumentNameId",
                        column: x => x.InstrumentNameId,
                        principalTable: "InstrumentNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrokerInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerSymbolId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    InstrumentTypeId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    expiry = table.Column<string>(maxLength: 50, nullable: true),
                    multiplicator = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerInstruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerInstruments_BrokerSymbols_BrokerSymbolId",
                        column: x => x.BrokerSymbolId,
                        principalTable: "BrokerSymbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerInstruments_InstrumentTypes_InstrumentTypeId",
                        column: x => x.InstrumentTypeId,
                        principalTable: "InstrumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_Name",
                table: "Brokers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstruments_BrokerSymbolId",
                table: "BrokerInstruments",
                column: "BrokerSymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstruments_InstrumentTypeId",
                table: "BrokerInstruments",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerSymbols_ExchangeId",
                table: "BrokerSymbols",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerSymbols_InstrumentNameId",
                table: "BrokerSymbols",
                column: "InstrumentNameId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerSymbols_Name",
                table: "BrokerSymbols",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_BrokerId",
                table: "Exchanges",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_CurrencyId",
                table: "Exchanges",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_Name_BrokerId",
                table: "Exchanges",
                columns: new[] { "Name", "BrokerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentNames_Name",
                table: "InstrumentNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTypes_Name",
                table: "InstrumentTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeIntervals_DurationTicks",
                table: "TimeIntervals",
                column: "DurationTicks",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrokerInstruments");

            migrationBuilder.DropTable(
                name: "TimeIntervals");

            migrationBuilder.DropTable(
                name: "BrokerSymbols");

            migrationBuilder.DropTable(
                name: "InstrumentTypes");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "InstrumentNames");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
