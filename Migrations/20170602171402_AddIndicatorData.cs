using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trading.Migrations
{
    public partial class AddIndicatorData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorEntries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Data = table.Column<decimal>(nullable: false),
                    IsDirty = table.Column<bool>(nullable: false),
                    PriceEntryId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_IndicatorEntries_PriceEntries_PriceEntryId",
                        column: x => x.PriceEntryId,
                        principalTable: "PriceEntries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorEntries_PriceEntryId_Type",
                table: "IndicatorEntries",
                columns: new[] { "PriceEntryId", "Type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndicatorEntries");
        }
    }
}
