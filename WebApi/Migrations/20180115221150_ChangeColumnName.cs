using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EntreeDetailType",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EntreeCatagory",
                newName: "Catagory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Style",
                table: "EntreeDetailType",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Catagory",
                table: "EntreeCatagory",
                newName: "Type");
        }
    }
}
