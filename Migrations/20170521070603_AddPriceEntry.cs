using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddPriceEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceEntries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerInstrumentId = table.Column<int>(nullable: false),
                    Close = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    High = table.Column<decimal>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    Low = table.Column<decimal>(nullable: false),
                    Open = table.Column<decimal>(nullable: false),
                    TimeIntervalId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Volume = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_PriceEntries_BrokerInstruments_BrokerInstrumentId",
                        column: x => x.BrokerInstrumentId,
                        principalTable: "BrokerInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceEntries_TimeIntervals_TimeIntervalId",
                        column: x => x.TimeIntervalId,
                        principalTable: "TimeIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceEntries_TimeIntervalId",
                table: "PriceEntries",
                column: "TimeIntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEntries_BrokerInstrumentId_TimeIntervalId_TimeStamp",
                table: "PriceEntries",
                columns: new[] { "BrokerInstrumentId", "TimeIntervalId", "TimeStamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceEntries");
        }
    }
}
