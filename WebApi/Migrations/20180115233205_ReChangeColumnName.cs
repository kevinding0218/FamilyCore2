using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class ReChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EntreeStyle",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "EntreeDetailType",
                newName: "DetailType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Style",
                table: "EntreeStyle",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DetailType",
                table: "EntreeDetailType",
                newName: "Style");
        }
    }
}
