using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddSignalStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExitRules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    ExitType = table.Column<int>(nullable: false),
                    RuleRelation = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitRules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Signals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    SignalType = table.Column<int>(nullable: false),
                    TradeDirection = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StopLossRules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    NewRuleRelation = table.Column<int>(nullable: false),
                    NewValue = table.Column<decimal>(nullable: true),
                    RuleRelation = table.Column<int>(nullable: false),
                    SlRuleType = table.Column<int>(nullable: false),
                    SlType = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopLossRules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SignalSteps",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Entry = table.Column<decimal>(nullable: true),
                    EntryType = table.Column<int>(nullable: false),
                    ExitType = table.Column<int>(nullable: false),
                    PriceEntryid = table.Column<int>(nullable: true),
                    SignalId = table.Column<int>(nullable: false),
                    SignalStepType = table.Column<int>(nullable: false),
                    Sl = table.Column<decimal>(nullable: true),
                    SlType = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalSteps", x => x.id);
                    table.ForeignKey(
                        name: "FK_SignalSteps_PriceEntries_PriceEntryid",
                        column: x => x.PriceEntryid,
                        principalTable: "PriceEntries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignalSteps_Signals_SignalId",
                        column: x => x.SignalId,
                        principalTable: "Signals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignalSteps_PriceEntryid",
                table: "SignalSteps",
                column: "PriceEntryid");

            migrationBuilder.CreateIndex(
                name: "IX_SignalSteps_SignalId",
                table: "SignalSteps",
                column: "SignalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExitRules");

            migrationBuilder.DropTable(
                name: "SignalSteps");

            migrationBuilder.DropTable(
                name: "StopLossRules");

            migrationBuilder.DropTable(
                name: "Signals");
        }
    }
}
