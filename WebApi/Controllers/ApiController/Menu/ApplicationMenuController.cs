using AutoMapper;
using DomainLibrary.Menu;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Persistent.Menu;
using WebApi.Resource.Menu;

namespace WebApi.Controllers.ApiController.Menu
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/applicationMenu")]
    public class ApplicationMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepo;

        public ApplicationMenuController(
            IMapper mapper,
            IMenuRepository menuRepo
        )
        {
            this._mapper = mapper;
            this._menuRepo = menuRepo;
        }

        #region  READ LIST OBJECT
        //api/applicationMenu
        [HttpGet]
        public async Task<IActionResult> GetNavigations(int userId)
        {
            var applicationMenus = await _menuRepo.GetMenuNavigations();
            var navigations = this._mapper.Map<IEnumerable<ApplicationMenu>, IEnumerable<Navigation>>(applicationMenus);
            foreach (var nav in navigations)
            {
                if (nav.ShowBadge.GetValueOrDefault())
                {
                    nav.Badge = await _menuRepo.GetNavigationBadge(nav.Name);
                }

                if (nav.children != null && nav.children.Count > 0)
                {
                    foreach (var childNav in nav.children)
                    {
                        if (childNav.ShowBadge.GetValueOrDefault())
                        {
                            childNav.Badge = await _menuRepo.GetNavigationBadge(childNav.Name);
                        }

                        if (childNav.children == null || childNav.children.Count == 0)
                        {
                            childNav.children = null;
                        }
                    }
                }
                else
                {
                    nav.children = null;
                }
            }

            // Return view Model
            return Ok(navigations);
        }
        #endregion
    }
}