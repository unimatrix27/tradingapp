using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddUseTodayToScreenerTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "TradeSteps",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Trades",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StopLossRules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SignalSteps",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Signals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerReferenceImages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerLines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerEntryTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerEntryMappings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ScreenerEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Screeners",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PriceEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "IndicatorEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ExitRules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BrokerTimeIntervals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BrokerInstrumentScreenerTypes",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "UseToday",
                table: "ScreenerTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseToday",
                table: "ScreenerTypes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TradeSteps",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trades",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StopLossRules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SignalSteps",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Signals",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerReferenceImages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerLines",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerEntryTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerEntryMappings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScreenerEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Screeners",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PriceEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IndicatorEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ExitRules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BrokerTimeIntervals",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BrokerInstrumentScreenerTypes",
                newName: "id");
        }
    }
}
