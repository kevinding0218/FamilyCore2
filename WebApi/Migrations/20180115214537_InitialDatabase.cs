using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApi.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntreeCatagory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntreeCatagory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntreeDetailType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntreeDetailType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntreeStyle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntreeStyle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StapleFood",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StapleFood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    IsFCUser = table.Column<bool>(nullable: true),
                    LastLogIn = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Supermarket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    AddressRefId = table.Column<int>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supermarket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supermarket_ContactAddress_AddressRefId",
                        column: x => x.AddressRefId,
                        principalTable: "ContactAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entree",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    CurrentRank = table.Column<int>(nullable: true),
                    EntreeCatagoryId = table.Column<int>(nullable: false),
                    EntreeStyleId = table.Column<int>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StapleFoodId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entree_EntreeCatagory_EntreeCatagoryId",
                        column: x => x.EntreeCatagoryId,
                        principalTable: "EntreeCatagory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entree_EntreeStyle_EntreeStyleId",
                        column: x => x.EntreeStyleId,
                        principalTable: "EntreeStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entree_StapleFood_StapleFoodId",
                        column: x => x.StapleFoodId,
                        principalTable: "StapleFood",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Supermarket_StapleFood",
                columns: table => new
                {
                    SuperMarketId = table.Column<int>(nullable: false),
                    StapleFoodId = table.Column<int>(nullable: false),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supermarket_StapleFood", x => new { x.SuperMarketId, x.StapleFoodId });
                    table.ForeignKey(
                        name: "FK_Supermarket_StapleFood_StapleFood_StapleFoodId",
                        column: x => x.StapleFoodId,
                        principalTable: "StapleFood",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supermarket_StapleFood_Supermarket_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "Supermarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntreeDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    EntreeDetailTypeId = table.Column<int>(nullable: false),
                    EntreeId = table.Column<int>(nullable: true),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntreeDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntreeDetail_EntreeDetailType_EntreeDetailTypeId",
                        column: x => x.EntreeDetailTypeId,
                        principalTable: "EntreeDetailType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntreeDetail_Entree_EntreeId",
                        column: x => x.EntreeId,
                        principalTable: "Entree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entrees_Details",
                columns: table => new
                {
                    EntreeId = table.Column<int>(nullable: false),
                    EntreeDetailId = table.Column<int>(nullable: false),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrees_Details", x => new { x.EntreeId, x.EntreeDetailId });
                    table.ForeignKey(
                        name: "FK_Entrees_Details_EntreeDetail_EntreeDetailId",
                        column: x => x.EntreeDetailId,
                        principalTable: "EntreeDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrees_Details_Entree_EntreeId",
                        column: x => x.EntreeId,
                        principalTable: "Entree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supermarket_EntreeDetail",
                columns: table => new
                {
                    SupermarketId = table.Column<int>(nullable: false),
                    EntreeDetailId = table.Column<int>(nullable: false),
                    AddedById = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<int>(nullable: true),
                    LastUpdatedByOn = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supermarket_EntreeDetail", x => new { x.SupermarketId, x.EntreeDetailId });
                    table.ForeignKey(
                        name: "FK_Supermarket_EntreeDetail_EntreeDetail_EntreeDetailId",
                        column: x => x.EntreeDetailId,
                        principalTable: "EntreeDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supermarket_EntreeDetail_Supermarket_SupermarketId",
                        column: x => x.SupermarketId,
                        principalTable: "Supermarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entree_EntreeCatagoryId",
                table: "Entree",
                column: "EntreeCatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entree_EntreeStyleId",
                table: "Entree",
                column: "EntreeStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Entree_StapleFoodId",
                table: "Entree",
                column: "StapleFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_EntreeDetail_EntreeDetailTypeId",
                table: "EntreeDetail",
                column: "EntreeDetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntreeDetail_EntreeId",
                table: "EntreeDetail",
                column: "EntreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrees_Details_EntreeDetailId",
                table: "Entrees_Details",
                column: "EntreeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Supermarket_AddressRefId",
                table: "Supermarket",
                column: "AddressRefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supermarket_EntreeDetail_EntreeDetailId",
                table: "Supermarket_EntreeDetail",
                column: "EntreeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Supermarket_StapleFood_StapleFoodId",
                table: "Supermarket_StapleFood",
                column: "StapleFoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrees_Details");

            migrationBuilder.DropTable(
                name: "Supermarket_EntreeDetail");

            migrationBuilder.DropTable(
                name: "Supermarket_StapleFood");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EntreeDetail");

            migrationBuilder.DropTable(
                name: "Supermarket");

            migrationBuilder.DropTable(
                name: "EntreeDetailType");

            migrationBuilder.DropTable(
                name: "Entree");

            migrationBuilder.DropTable(
                name: "ContactAddress");

            migrationBuilder.DropTable(
                name: "EntreeCatagory");

            migrationBuilder.DropTable(
                name: "EntreeStyle");

            migrationBuilder.DropTable(
                name: "StapleFood");
        }
    }
}
