using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class adjustEntreeMappingObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntreeDetail_Entree_EntreeId",
                table: "EntreeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EntreeDetail_EntreeId",
                table: "EntreeDetail");

            migrationBuilder.DropColumn(
                name: "EntreeId",
                table: "EntreeDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntreeId",
                table: "EntreeDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntreeDetail_EntreeId",
                table: "EntreeDetail",
                column: "EntreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntreeDetail_Entree_EntreeId",
                table: "EntreeDetail",
                column: "EntreeId",
                principalTable: "Entree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
