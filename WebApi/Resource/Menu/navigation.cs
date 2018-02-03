using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebApi.Resource.Menu
{
    public class Navigation
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool? ShowBadge { get; set; }
        public NavigationBadge Badge { get; set; }


        public ICollection<Navigation> children;

        public Navigation()
        {
            children = new Collection<Navigation>();
        }
    }

    public class NavigationBadge
    {
        public string Variant { get; set; }
        public string Text { get; set; }
    }
}
