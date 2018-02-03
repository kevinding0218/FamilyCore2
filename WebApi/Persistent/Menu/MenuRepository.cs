using DomainLibrary.Menu;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resource.Menu;

namespace WebApi.Persistent.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly FcDbContext _context;

        public MenuRepository(FcDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<ApplicationMenu>> GetMenuNavigations()
        {
            var mainMenu = await this._context.Menus
                                    .Include(nav => nav.Children)
                                    .Where(nav => nav.ApplicationMenuId == null).OrderBy(nav => nav.MenuPosition).ToListAsync();

            return mainMenu;
        }

        public async Task<NavigationBadge> GetNavigationBadge(string NavName)
        {
            var badge = new NavigationBadge();
            await _context.LoadStoredProc("dbo.GetNavigationBadge")
                .WithSqlParam("NavName", NavName)
                .ExecuteStoredProcAsync((handler) =>
                {
                    badge = handler.ReadToList<NavigationBadge>().ToList().FirstOrDefault();
                    // do something with your results.
                });
            return badge;
        }
    }
}
