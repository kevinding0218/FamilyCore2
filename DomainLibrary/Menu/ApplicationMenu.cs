using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLibrary.Menu
{
    [Table("ApplicationMenu")]
    public class ApplicationMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool? ShowBadge { get; set; }
        public int MenuPosition { get; set; }
        //public string BadgeVariant { get; set; }
        //public string BadgeText { get; set; }

        public int? ApplicationMenuId { get; set; }
        public ICollection<ApplicationMenu> Children { get; set; }

        public ApplicationMenu()
        {
            Children = new Collection<ApplicationMenu>();
        }
    }
}
