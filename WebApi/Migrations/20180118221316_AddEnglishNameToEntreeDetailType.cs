using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddEnglishNameToEntreeDetailType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailName",
                table: "EntreeDetailType",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE EntreeDetailType SET DetailName = 'Vegetable' WHERE DetailType = '蔬菜'");
            migrationBuilder.Sql("UPDATE EntreeDetailType SET DetailName = 'Meat' WHERE DetailType = '肉类'");
            migrationBuilder.Sql("UPDATE EntreeDetailType SET DetailName = 'Seafood' WHERE DetailType = '海鲜'");
            migrationBuilder.Sql("UPDATE EntreeDetailType SET DetailName = 'Ingredient' WHERE DetailType = '配料'");
            migrationBuilder.Sql("UPDATE EntreeDetailType SET DetailName = 'Sauce' WHERE DetailType = '酱汁'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailName",
                table: "EntreeDetailType");
        }
    }
}
