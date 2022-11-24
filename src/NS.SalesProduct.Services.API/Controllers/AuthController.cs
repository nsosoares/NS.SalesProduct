using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Helpers.Messages;
using NS.SalesProduct.Services.API.Extensions;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            INotificationHandler notificationHandler
            , SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager
            , IOptions<JwtSettings> jwtSettings) : base(notificationHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApiViewModel>> Post([FromBody] RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState, registerUserViewModel);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(JwtGenerator.GenerateJwt(_jwtSettings));
            }

            foreach (var error in result.Errors)
            {
                Notify($"{error.Code} - {error.Description}");
            }

            return CustomResponse(registerUserViewModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseApiViewModel>> Login([FromBody] LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState, loginUserViewModel);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

            if (result.Succeeded) return Ok(JwtGenerator.GenerateJwt(_jwtSettings));

            if (result.IsLockedOut)
            {
                Notify(TriggerMessage.TriggerMessageIsLockedOut());
                return CustomResponse(loginUserViewModel);
            }
            Notify(TriggerMessage.TriggerMessageloginFailure());
            return CustomResponse(loginUserViewModel);
        }
    }
}
