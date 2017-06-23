using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddSCreenerRefData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PriceEntries_BrokerInstrumentId_TimeIntervalId_TimeStamp",
                table: "PriceEntries");

            migrationBuilder.DropIndex(
                name: "IX_BrokerTimeIntervals_TimeIntervalId",
                table: "BrokerTimeIntervals");

            migrationBuilder.AddColumn<int>(
                name: "ScreenerTypeid",
                table: "BrokerInstruments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScreenerEntryTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerEntryTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenerTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    LastCheck = table.Column<DateTime>(nullable: false),
                    LastHash = table.Column<string>(nullable: true),
                    LastResult = table.Column<string>(nullable: true),
                    MarkerBlue = table.Column<int>(nullable: true),
                    MarkerGreen = table.Column<int>(nullable: true),
                    MarkerRed = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    NameColumn = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    TimeIntervalId = table.Column<int>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    UpdateFrequencyTicks = table.Column<long>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ScreenerTypes_TimeIntervals_TimeIntervalId",
                        column: x => x.TimeIntervalId,
                        principalTable: "TimeIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screeners",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ImageFile = table.Column<string>(nullable: true),
                    ImageHash = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false),
                    NextId = table.Column<int>(nullable: true),
                    ParseError = table.Column<bool>(nullable: false),
                    ParseErrorString = table.Column<string>(nullable: true),
                    PrevId = table.Column<int>(nullable: true),
                    ScreenerTypeId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screeners", x => x.id);
                    table.ForeignKey(
                        name: "FK_Screeners_ScreenerTypes_ScreenerTypeId",
                        column: x => x.ScreenerTypeId,
                        principalTable: "ScreenerTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenerEntryMappings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ScreenerEntryTypeId = table.Column<int>(nullable: false),
                    ScreenerTypeId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerEntryMappings", x => x.id);
                    table.ForeignKey(
                        name: "FK_ScreenerEntryMappings_ScreenerEntryTypes_ScreenerEntryTypeId",
                        column: x => x.ScreenerEntryTypeId,
                        principalTable: "ScreenerEntryTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenerEntryMappings_ScreenerTypes_ScreenerTypeId",
                        column: x => x.ScreenerTypeId,
                        principalTable: "ScreenerTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenerReferenceImages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerInstrumentId = table.Column<int>(nullable: true),
                    CellColor = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ImageSignature = table.Column<string>(nullable: true),
                    ScreenerTypeId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerReferenceImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_ScreenerReferenceImages_BrokerInstruments_BrokerInstrumentId",
                        column: x => x.BrokerInstrumentId,
                        principalTable: "BrokerInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreenerReferenceImages_ScreenerTypes_ScreenerTypeId",
                        column: x => x.ScreenerTypeId,
                        principalTable: "ScreenerTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenerLines",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerInstrumentId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ScreenerId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerLines", x => x.id);
                    table.ForeignKey(
                        name: "FK_ScreenerLines_BrokerInstruments_BrokerInstrumentId",
                        column: x => x.BrokerInstrumentId,
                        principalTable: "BrokerInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenerLines_Screeners_ScreenerId",
                        column: x => x.ScreenerId,
                        principalTable: "Screeners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenerEntries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bg = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Fg = table.Column<int>(nullable: false),
                    ScreenerEntryTypeId = table.Column<int>(nullable: false),
                    ScreenerLineId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    value = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenerEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_ScreenerEntries_ScreenerEntryTypes_ScreenerEntryTypeId",
                        column: x => x.ScreenerEntryTypeId,
                        principalTable: "ScreenerEntryTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenerEntries_ScreenerLines_ScreenerLineId",
                        column: x => x.ScreenerLineId,
                        principalTable: "ScreenerLines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceEntries_BrokerInstrumentId_TimeIntervalId_TimeStamp",
                table: "PriceEntries",
                columns: new[] { "BrokerInstrumentId", "TimeIntervalId", "TimeStamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTimeIntervals_TimeIntervalId_BrokerId",
                table: "BrokerTimeIntervals",
                columns: new[] { "TimeIntervalId", "BrokerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstruments_ScreenerTypeid",
                table: "BrokerInstruments",
                column: "ScreenerTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Screeners_ImageFile",
                table: "Screeners",
                column: "ImageFile",
                unique: true,
                filter: "[ImageFile] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Screeners_NextId",
                table: "Screeners",
                column: "NextId",
                unique: true,
                filter: "[NextId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Screeners_PrevId",
                table: "Screeners",
                column: "PrevId",
                unique: true,
                filter: "[PrevId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Screeners_ScreenerTypeId",
                table: "Screeners",
                column: "ScreenerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerEntries_ScreenerLineId",
                table: "ScreenerEntries",
                column: "ScreenerLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerEntries_ScreenerEntryTypeId_ScreenerLineId",
                table: "ScreenerEntries",
                columns: new[] { "ScreenerEntryTypeId", "ScreenerLineId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerEntryMappings_ScreenerTypeId",
                table: "ScreenerEntryMappings",
                column: "ScreenerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerEntryMappings_ScreenerEntryTypeId_ScreenerTypeId",
                table: "ScreenerEntryMappings",
                columns: new[] { "ScreenerEntryTypeId", "ScreenerTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerEntryTypes_Name",
                table: "ScreenerEntryTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerLines_ScreenerId",
                table: "ScreenerLines",
                column: "ScreenerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerLines_BrokerInstrumentId_ScreenerId",
                table: "ScreenerLines",
                columns: new[] { "BrokerInstrumentId", "ScreenerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerReferenceImages_BrokerInstrumentId",
                table: "ScreenerReferenceImages",
                column: "BrokerInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerReferenceImages_ImageSignature",
                table: "ScreenerReferenceImages",
                column: "ImageSignature",
                unique: true,
                filter: "[ImageSignature] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerReferenceImages_ScreenerTypeId",
                table: "ScreenerReferenceImages",
                column: "ScreenerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerTypes_Name",
                table: "ScreenerTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenerTypes_TimeIntervalId",
                table: "ScreenerTypes",
                column: "TimeIntervalId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrokerInstruments_ScreenerTypes_ScreenerTypeid",
                table: "BrokerInstruments",
                column: "ScreenerTypeid",
                principalTable: "ScreenerTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrokerInstruments_ScreenerTypes_ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.DropTable(
                name: "ScreenerEntries");

            migrationBuilder.DropTable(
                name: "ScreenerEntryMappings");

            migrationBuilder.DropTable(
                name: "ScreenerReferenceImages");

            migrationBuilder.DropTable(
                name: "ScreenerLines");

            migrationBuilder.DropTable(
                name: "ScreenerEntryTypes");

            migrationBuilder.DropTable(
                name: "Screeners");

            migrationBuilder.DropTable(
                name: "ScreenerTypes");

            migrationBuilder.DropIndex(
                name: "IX_PriceEntries_BrokerInstrumentId_TimeIntervalId_TimeStamp",
                table: "PriceEntries");

            migrationBuilder.DropIndex(
                name: "IX_BrokerTimeIntervals_TimeIntervalId_BrokerId",
                table: "BrokerTimeIntervals");

            migrationBuilder.DropIndex(
                name: "IX_BrokerInstruments_ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.DropColumn(
                name: "ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEntries_BrokerInstrumentId_TimeIntervalId_TimeStamp",
                table: "PriceEntries",
                columns: new[] { "BrokerInstrumentId", "TimeIntervalId", "TimeStamp" });

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTimeIntervals_TimeIntervalId",
                table: "BrokerTimeIntervals",
                column: "TimeIntervalId");
        }
    }
}
