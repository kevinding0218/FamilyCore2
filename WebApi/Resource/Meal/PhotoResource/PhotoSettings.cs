using System.IO;
using System.Linq;

namespace WebApi.Resource.Meal.PhotoResource
{
    public class PhotoSettings
    {
        public long MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
        public string StorageLocation { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
    }
}
