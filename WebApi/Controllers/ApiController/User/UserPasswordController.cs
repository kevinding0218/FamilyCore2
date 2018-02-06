using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.User;

namespace WebApi.Controllers.ApiController.User
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/userpassword")]
    public class UserPasswordController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _passwordRepository;

        public UserPasswordController(
            IMapper mapper,
            IUserRepository userRepository,
            IPasswordRepository passwordRepository,
            IUnitOfWork uow
        )
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._passwordRepository = passwordRepository;
        }

        #region CREATE
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserResource newRegisterUser)
        {
            // Convert from View Model to Domain Model
            var newUser = _mapper.Map<RegisterNewUserResource, DomainLibrary.Member.User>(newRegisterUser);
            // Encrypt Password
            var registeredPassword = newUser.UserPasswords.First().Password;
            newUser.UserPasswords.First().Password = _passwordRepository.Encrypt(registeredPassword);
            // Apply Other fields
            newUser.PasswordExpired = DateTime.Now.AddMonths(1);
            newUser.AddedOn = DateTime.Now;
            newUser.Active = true;
            newUser.Note = string.Empty;
            if (newUser.Email.Contains("familycore.com"))
                newUser.IsFCUser = true;
            else
                newUser.IsFCUser = false;

            // Insert into database by using Domain Model
            _userRepository.RegisterNewUser(newUser);

            await _uow.CompleteAsync();

            newUser = await _userRepository.GetUserById(newUser.UserID);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<DomainLibrary.Member.User, ViewUserResource>(newUser);

            // Return view Model
            return Ok(result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyLogin([FromBody] RegisterNewUserResource newRegisterUser)
        {
            var isAuthenticated = await _userRepository.VerifyLogin(newRegisterUser.Email, newRegisterUser.Password);

            if (isAuthenticated)
            {
                var loggedInUser = await _userRepository.GetUserByEmail(newRegisterUser.Email);

                loggedInUser.LastLogIn = DateTime.Now;

                await _uow.CompleteAsync();

                // Fetch complete object from database
                loggedInUser = await _userRepository.GetUserById(loggedInUser.UserID);
                // Convert from Domain Model to View Model
                var result = _mapper.Map<DomainLibrary.Member.User, ViewUserResource>(loggedInUser);

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
    }
}