namespace WebApi.Resource.Meal.EntreeResource
{
    public class EntreeDetailMappingResource
    {
        public int EntreeDetailId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string EntreeDetailTypeName { get; set; }
        public bool DisplayMode { get; set; }

        public EntreeDetailMappingResource(int entreeDetailId, string name, int quantity, string entreeDetailTypeName)
        {
            this.EntreeDetailId = entreeDetailId;
            this.Name = name;
            this.Quantity = quantity;
            this.EntreeDetailTypeName = entreeDetailTypeName;
            this.DisplayMode = true;
        }
    }
}
