using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddBrokerInstrumentScreenerTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrokerInstruments_ScreenerTypes_ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.DropIndex(
                name: "IX_BrokerInstruments_ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.DropColumn(
                name: "ScreenerTypeid",
                table: "BrokerInstruments");

            migrationBuilder.CreateTable(
                name: "BrokerInstrumentScreenerTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerInstrumentId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ScreenerTypeId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerInstrumentScreenerTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_BrokerInstrumentScreenerTypes_BrokerInstruments_BrokerInstrumentId",
                        column: x => x.BrokerInstrumentId,
                        principalTable: "BrokerInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerInstrumentScreenerTypes_ScreenerTypes_ScreenerTypeId",
                        column: x => x.ScreenerTypeId,
                        principalTable: "ScreenerTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstrumentScreenerTypes_ScreenerTypeId",
                table: "BrokerInstrumentScreenerTypes",
                column: "ScreenerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstrumentScreenerTypes_BrokerInstrumentId_ScreenerTypeId",
                table: "BrokerInstrumentScreenerTypes",
                columns: new[] { "BrokerInstrumentId", "ScreenerTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrokerInstrumentScreenerTypes");

            migrationBuilder.AddColumn<int>(
                name: "ScreenerTypeid",
                table: "BrokerInstruments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrokerInstruments_ScreenerTypeid",
                table: "BrokerInstruments",
                column: "ScreenerTypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_BrokerInstruments_ScreenerTypes_ScreenerTypeid",
                table: "BrokerInstruments",
                column: "ScreenerTypeid",
                principalTable: "ScreenerTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
