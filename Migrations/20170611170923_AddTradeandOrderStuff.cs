using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddTradeandOrderStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    SignalId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.id);
                    table.ForeignKey(
                        name: "FK_Trade_Signals_SignalId",
                        column: x => x.SignalId,
                        principalTable: "Signals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerOrderId = table.Column<string>(nullable: true),
                    Costs = table.Column<decimal>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ExecutedLevel = table.Column<decimal>(nullable: true),
                    Function = table.Column<int>(nullable: false),
                    LimitLevel = table.Column<decimal>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    StopLevel = table.Column<decimal>(nullable: true),
                    TradeId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeStep",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Reason = table.Column<int>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    TradeId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeStep", x => x.id);
                    table.ForeignKey(
                        name: "FK_TradeStep_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_TradeId",
                table: "Order",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_SignalId",
                table: "Trade",
                column: "SignalId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeStep_TradeId",
                table: "TradeStep",
                column: "TradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "TradeStep");

            migrationBuilder.DropTable(
                name: "Trade");
        }
    }
}
