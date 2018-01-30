using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApi.Migrations
{
    public partial class recreatemappingentreeorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrees_Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    EntreeId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ScheduledDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrees_Orders", x => new { x.OrderId, x.EntreeId });
                    table.ForeignKey(
                        name: "FK_Entrees_Orders_Entree_EntreeId",
                        column: x => x.EntreeId,
                        principalTable: "Entree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrees_Orders_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrees_Orders_EntreeId",
                table: "Entrees_Orders",
                column: "EntreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
