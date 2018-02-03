using DomainLibrary.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resource.Menu;

namespace WebApi.Persistent.Menu
{
    public interface IMenuRepository
    {
        Task<IEnumerable<ApplicationMenu>> GetMenuNavigations();

        Task<NavigationBadge> GetNavigationBadge(string NavName);
    }
}
