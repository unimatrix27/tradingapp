using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddBrokerInstIdToSignals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrokerInstrumentId",
                table: "Signals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeIntervalId",
                table: "Signals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Signals_BrokerInstrumentId",
                table: "Signals",
                column: "BrokerInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Signals_TimeIntervalId",
                table: "Signals",
                column: "TimeIntervalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signals_BrokerInstruments_BrokerInstrumentId",
                table: "Signals",
                column: "BrokerInstrumentId",
                principalTable: "BrokerInstruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Signals_TimeIntervals_TimeIntervalId",
                table: "Signals",
                column: "TimeIntervalId",
                principalTable: "TimeIntervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signals_BrokerInstruments_BrokerInstrumentId",
                table: "Signals");

            migrationBuilder.DropForeignKey(
                name: "FK_Signals_TimeIntervals_TimeIntervalId",
                table: "Signals");

            migrationBuilder.DropIndex(
                name: "IX_Signals_BrokerInstrumentId",
                table: "Signals");

            migrationBuilder.DropIndex(
                name: "IX_Signals_TimeIntervalId",
                table: "Signals");

            migrationBuilder.DropColumn(
                name: "BrokerInstrumentId",
                table: "Signals");

            migrationBuilder.DropColumn(
                name: "TimeIntervalId",
                table: "Signals");
        }
    }
}
