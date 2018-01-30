using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class addPrimaryKeySechueldDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Entrees_Orders",
                table: "Entrees_Orders");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Entrees_Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Entrees_Orders",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entrees_Orders",
                table: "Entrees_Orders",
                columns: new[] { "Id", "OrderId", "EntreeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Entrees_Orders_OrderId",
                table: "Entrees_Orders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Entrees_Orders",
                table: "Entrees_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Entrees_Orders_OrderId",
                table: "Entrees_Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Entrees_Orders");

            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Entrees_Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entrees_Orders",
                table: "Entrees_Orders",
                columns: new[] { "OrderId", "EntreeId" });
        }
    }
}
