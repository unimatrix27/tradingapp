using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddLevelsToTradeSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EntryLevel",
                table: "TradeSteps",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitLevel",
                table: "TradeSteps",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StopLevel",
                table: "TradeSteps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryLevel",
                table: "TradeSteps");

            migrationBuilder.DropColumn(
                name: "ProfitLevel",
                table: "TradeSteps");

            migrationBuilder.DropColumn(
                name: "StopLevel",
                table: "TradeSteps");
        }
    }
}
