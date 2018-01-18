namespace WebApi.Persistent.Shared
{
    public static class EntreeDetailTypeEnum
    {
        public const string Vegetable = "蔬菜";
        public const string Meat = "肉类";
        public const string Seafood = "海鲜";
        public const string Ingredient = "配料";
        public const string Sauce = "酱汁";

        public static string TranslateEntreeDetailType(string entreeType)
        {
            switch (entreeType.ToLower())
            {
                case "meat":
                    return EntreeDetailTypeEnum.Meat;
                case "vegetable":
                    return EntreeDetailTypeEnum.Vegetable;
                case "seafood":
                    return EntreeDetailTypeEnum.Seafood;
                case "ingredient":
                    return EntreeDetailTypeEnum.Ingredient;
                case "sauce":
                    return EntreeDetailTypeEnum.Sauce;
                default:
                    return string.Empty;
            }
        }
    }
}
