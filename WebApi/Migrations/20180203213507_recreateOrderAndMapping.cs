using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApi.Migrations
{
    public partial class recreateOrderAndMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "Entrees_Orders");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
