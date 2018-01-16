using System.ComponentModel.DataAnnotations;

namespace WebApi.Resource.Shared
{
    public class KeyValuePairResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
