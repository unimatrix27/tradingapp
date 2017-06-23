using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class RemoveNullableKeysForSignalStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signals_BrokerInstruments_BrokerInstrumentId",
                table: "Signals");

            migrationBuilder.DropForeignKey(
                name: "FK_Signals_TimeIntervals_TimeIntervalId",
                table: "Signals");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalSteps_PriceEntries_PriceEntryid",
                table: "SignalSteps");

            migrationBuilder.RenameColumn(
                name: "PriceEntryid",
                table: "SignalSteps",
                newName: "PriceEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_SignalSteps_PriceEntryid",
                table: "SignalSteps",
                newName: "IX_SignalSteps_PriceEntryId");

            migrationBuilder.AlterColumn<int>(
                name: "PriceEntryId",
                table: "SignalSteps",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeIntervalId",
                table: "Signals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrokerInstrumentId",
                table: "Signals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_SignalSteps_PriceEntries_PriceEntryId",
                table: "SignalSteps",
                column: "PriceEntryId",
                principalTable: "PriceEntries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signals_BrokerInstruments_BrokerInstrumentId",
                table: "Signals");

            migrationBuilder.DropForeignKey(
                name: "FK_Signals_TimeIntervals_TimeIntervalId",
                table: "Signals");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalSteps_PriceEntries_PriceEntryId",
                table: "SignalSteps");

            migrationBuilder.RenameColumn(
                name: "PriceEntryId",
                table: "SignalSteps",
                newName: "PriceEntryid");

            migrationBuilder.RenameIndex(
                name: "IX_SignalSteps_PriceEntryId",
                table: "SignalSteps",
                newName: "IX_SignalSteps_PriceEntryid");

            migrationBuilder.AlterColumn<int>(
                name: "PriceEntryid",
                table: "SignalSteps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TimeIntervalId",
                table: "Signals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BrokerInstrumentId",
                table: "Signals",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.AddForeignKey(
                name: "FK_SignalSteps_PriceEntries_PriceEntryid",
                table: "SignalSteps",
                column: "PriceEntryid",
                principalTable: "PriceEntries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
