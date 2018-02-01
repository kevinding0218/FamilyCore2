using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Meal
{
    [Table("EntreePhoto")]
    public class EntreePhoto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public byte[] FileBlob { get; set; }

        public int EntreeId { get; set; }
    }
}
