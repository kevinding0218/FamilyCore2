using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class EntreePhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntreePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntreeId = table.Column<int>(nullable: false),
                    FileBlob = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntreePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntreePhoto_Entree_EntreeId",
                        column: x => x.EntreeId,
                        principalTable: "Entree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntreePhoto_EntreeId",
                table: "EntreePhoto",
                column: "EntreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntreePhoto");
        }
    }
}
