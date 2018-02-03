using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class removeBadgeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BadgeText",
                table: "ApplicationMenu");

            migrationBuilder.DropColumn(
                name: "BadgeVariant",
                table: "ApplicationMenu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BadgeText",
                table: "ApplicationMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BadgeVariant",
                table: "ApplicationMenu",
                nullable: true);
        }
    }
}
