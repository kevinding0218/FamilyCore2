using AutoMapper;
using DomainLibrary.Member;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Persistent.Helpers;
using WebApi.Persistent.Member;
using WebApi.Persistent.Utility;
using WebApi.Resource.Member;

namespace WebApi.Controllers.ApiController.Member
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/member")]
    public class MemberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMemberRepository _memberRepository;


        public MemberController(
            IMapper mapper,
            UserManager<AppUser> userManager,
            IMemberRepository memberRepository,
            IUnitOfWork uow
        )
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<RegistrationViewModel, AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            _memberRepository.AddMember(new DomainLibrary.Member.Member { IdentityId = userIdentity.Id, Location = model.Location });
            await _uow.CompleteAsync();

            return new OkObjectResult("Account created");
        }
    }
}