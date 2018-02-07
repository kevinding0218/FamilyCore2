using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class SeedSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("INSERT INTO Users(Email, FirstName, LastName, IsFCUser, LastLogIn, AddedById, AddedOn) VALUES ('kevinding0218@gmail.com', 'Ran', 'Ding', 1, GETDATE(), 1, GETDATE())");

            ////Seed into EntreeDetailType
            //SeedEntreeDetailType(migrationBuilder);

            ////Seed Vegetable into EntreeDetail
            //SeedEntreeDetailVegetable(migrationBuilder);

            ////Seed Meat into EntreeDetail
            //SeedEntreeDetailMeat(migrationBuilder);

            ////Seed into StapleFood
            //SeedStapleFood(migrationBuilder);

            ////Seed into Entree Category
            //SeedEntreeCategory(migrationBuilder);

            ////Seed into Entree Style
            //SeedEntreeStyle(migrationBuilder);

            ////Seed into Entree
            //SeedEntree(migrationBuilder);

            //SeedEntree_Details(migrationBuilder);
        }

        private static void SeedEntree_Details(MigrationBuilder migrationBuilder)
        {
            //Seed into Entrees_Details
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿炒鸡蛋'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '鸡蛋'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
                "(SELECT ID FROM Entree WHERE NAME = '西红柿鸡蛋打卤面'), " +
                "(SELECT ID FROM EntreeDetail WHERE NAME = '鸡蛋'), " +
                "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒牛柳面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '牛里脊'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒炒牛柳'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '牛里脊'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿牛肉汤面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '牛腱'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = 'XO酱蛋炒饭'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '鸡蛋'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = 'XO酱蛋炒饭'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '台湾香肠'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '锅塌豆腐'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '滑豆腐'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿炒花菜'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '西红柿'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿炒花菜'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '中式有机花菜'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿炒鸡蛋'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '西红柿'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿鸡蛋打卤面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '西红柿'), " +
              "1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '年糕汤'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '小白菜')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒牛柳面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '青椒')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒牛柳面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '黄洋葱')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒炒牛柳'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '青椒')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '黑椒炒牛柳'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '黄洋葱')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿牛肉汤面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '西红柿')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO Entrees_Details(EntreeId, EntreeDetailId, Quantity, AddedById, AddedOn) VALUES (" +
              "(SELECT ID FROM Entree WHERE NAME = '西红柿牛肉汤面'), " +
              "(SELECT ID FROM EntreeDetail WHERE NAME = '大青菜')," +
              " 1, (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
        }

        private static void SeedEntree(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('锅塌豆腐', (SELECT ID FROM StapleFood WHERE NAME = '米饭'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 10, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式淮扬'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('西红柿炒花菜', (SELECT ID FROM StapleFood WHERE NAME = '米饭'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 9, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式传统'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('西红柿炒鸡蛋', (SELECT ID FROM StapleFood WHERE NAME = '米饭'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 8, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式传统'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('炸馒头', (SELECT ID FROM StapleFood WHERE NAME = '大馒头'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 6, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式传统'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('西红柿鸡蛋打卤面', (SELECT ID FROM StapleFood WHERE NAME = '面条'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 9, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式淮扬'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('年糕汤', (SELECT ID FROM StapleFood WHERE NAME = '年糕片'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 10, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式淮扬'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('黑椒牛柳面', (SELECT ID FROM StapleFood WHERE NAME = '面条'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 8, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式传统'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('黑椒炒牛柳', (SELECT ID FROM StapleFood WHERE NAME = '米饭'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 8, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式粤菜'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('西红柿牛肉汤面', (SELECT ID FROM StapleFood WHERE NAME = '面条'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 10, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式传统'))");
            migrationBuilder.Sql("INSERT INTO Entree(Name, StapleFoodId, AddedById, AddedOn, CurrentRank, EntreeCatagoryId, EntreeStyleId) VALUES ('XO酱蛋炒饭', (SELECT ID FROM StapleFood   WHERE NAME = '米饭'), (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), 10, (SELECT ID FROM EntreeCatagory WHERE Type = '热菜'), (SELECT ID FROM EntreeStyle WHERE Type = '中式粤菜'))");
        }

        private static void SeedEntreeStyle(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式传统', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式淮扬', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式四川', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式粤菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式台湾', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('日本', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('韩国', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('西餐', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('地中海', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式东北', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeStyle(Type, AddedById, AddedOn) VALUES ('中式北方', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
        }

        private static void SeedEntreeCategory(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('前菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('热菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('汤类', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('点心', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('甜点', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('饮料', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeCatagory(Type, AddedById, AddedOn) VALUES ('快餐', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
        }

        private static void SeedStapleFood(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('米饭', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('面条', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('大馒头', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('巧克力馒头', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('袖珍馒头', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('年糕片', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO StapleFood(Name, AddedById, AddedOn) VALUES ('年糕条', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
        }

        private static void SeedEntreeDetailMeat(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('牛腱', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('牛里脊', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('牛尾骨', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('肥牛片', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('日式高级和牛片', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('日式中级和牛片', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('火锅牛里脊片', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('鸡蛋', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('台湾香肠', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '肉类'))");
        }

        private static void SeedEntreeDetailVegetable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('小青菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('大青菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('小白菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('大白菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('芹菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('中式有机花菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('西红柿', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('茄子', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('青椒', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('黄洋葱', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('嫩豆腐', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('滑豆腐', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('老豆腐', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
            migrationBuilder.Sql("INSERT INTO EntreeDetail(Name, AddedById, AddedOn, EntreeDetailTypeId) VALUES ('西兰花', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE(), (SELECT ID FROM EntreeDetailType WHERE Type = '蔬菜'))");
        }

        private static void SeedEntreeDetailType(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO EntreeDetailType(Type, AddedById, AddedOn) VALUES ('蔬菜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeDetailType(Type, AddedById, AddedOn) VALUES ('肉类', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeDetailType(Type, AddedById, AddedOn) VALUES ('海鲜', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeDetailType(Type, AddedById, AddedOn) VALUES ('配料', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
            migrationBuilder.Sql("INSERT INTO EntreeDetailType(Type, AddedById, AddedOn) VALUES ('酱汁', (SELECT USERID FROM Users WHERE Email = 'kevinding0218@gmail.com'), GETDATE())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Email = 'kevinding0218@gmail.com'");

            migrationBuilder.Sql("DELETE FROM EntreeDetailType");
            migrationBuilder.Sql("DELETE FROM EntreeDetail");
            migrationBuilder.Sql("DELETE FROM StapleFood");
            migrationBuilder.Sql("DELETE FROM EntreeCategory");
            migrationBuilder.Sql("DELETE FROM EntreeStyle");
            migrationBuilder.Sql("DELETE FROM Entree");
            migrationBuilder.Sql("DELETE FROM Entrees_Details");
        }
    }
}
