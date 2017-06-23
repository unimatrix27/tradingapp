using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddBrokerTimeInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrokerTimeIntervals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerId = table.Column<int>(nullable: false),
                    BrokerName = table.Column<string>(maxLength: 20, nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    TimeIntervalId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    dataPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerTimeIntervals", x => x.id);
                    table.ForeignKey(
                        name: "FK_BrokerTimeIntervals_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerTimeIntervals_TimeIntervals_TimeIntervalId",
                        column: x => x.TimeIntervalId,
                        principalTable: "TimeIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTimeIntervals_BrokerId",
                table: "BrokerTimeIntervals",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTimeIntervals_TimeIntervalId",
                table: "BrokerTimeIntervals",
                column: "TimeIntervalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrokerTimeIntervals");
        }
    }
}
