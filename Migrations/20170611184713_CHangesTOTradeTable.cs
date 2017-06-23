using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class CHangesTOTradeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Trade_TradeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Trade_Signals_SignalId",
                table: "Trade");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeStep_Trade_TradeId",
                table: "TradeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeStep",
                table: "TradeStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trade",
                table: "Trade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "TradeStep",
                newName: "TradeSteps");

            migrationBuilder.RenameTable(
                name: "Trade",
                newName: "Trades");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_TradeStep_TradeId",
                table: "TradeSteps",
                newName: "IX_TradeSteps_TradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trade_SignalId",
                table: "Trades",
                newName: "IX_Trades_SignalId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_TradeId",
                table: "Orders",
                newName: "IX_Orders_TradeId");

            migrationBuilder.AlterColumn<int>(
                name: "Reason",
                table: "TradeSteps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradeSteps",
                table: "TradeSteps",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trades",
                table: "Trades",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Trades_TradeId",
                table: "Orders",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Signals_SignalId",
                table: "Trades",
                column: "SignalId",
                principalTable: "Signals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeSteps_Trades_TradeId",
                table: "TradeSteps",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Trades_TradeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Signals_SignalId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeSteps_Trades_TradeId",
                table: "TradeSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeSteps",
                table: "TradeSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trades",
                table: "Trades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "TradeSteps",
                newName: "TradeStep");

            migrationBuilder.RenameTable(
                name: "Trades",
                newName: "Trade");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_TradeSteps_TradeId",
                table: "TradeStep",
                newName: "IX_TradeStep_TradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trades_SignalId",
                table: "Trade",
                newName: "IX_Trade_SignalId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_TradeId",
                table: "Order",
                newName: "IX_Order_TradeId");

            migrationBuilder.AlterColumn<int>(
                name: "Reason",
                table: "TradeStep",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradeStep",
                table: "TradeStep",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trade",
                table: "Trade",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Trade_TradeId",
                table: "Order",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trade_Signals_SignalId",
                table: "Trade",
                column: "SignalId",
                principalTable: "Signals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeStep_Trade_TradeId",
                table: "TradeStep",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
