using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationFitness.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Identity;
using ApplicationFitness.Domain.Models.Auth;
using WebApi.Services;
using WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IProgramScheduleService _programScheduleService;
        private readonly AuthOptions _authenticationOptions;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager,
            IUserService userService, SignInManager<User> signInManager, IProgramScheduleService programScheduleService,
            IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _signInManager = signInManager;
            _programScheduleService = programScheduleService;
            _authenticationOptions = authOptions.Value;
           
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var resultRole = _roleManager.RoleExistsAsync("user").Result;
                    if (!resultRole)
                    {
                        var role = new Role("user");
                        var roleResult = _roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest("Unautorized user");
                        }
                    }
                    await _userManager.AddToRoleAsync(user, "user");
                    _userService.AddNewUser(model);
                    await _signInManager.SignInAsync(user, false);
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return BadRequest(error.Description);
                    }
                }
                
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> CreateProfilePage([FromBody] UserProfileDto dto, string name)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(name);
                if (user == null)
                {
                    return BadRequest("Invalid User Name");
                }
                _userService.UpdateUserProfile(dto, user);
                var schedule = _programScheduleService.FindProgramForUser(user);
                if (schedule == null)
                {
                    return BadRequest("No such program schedule");
                }
                else
                {
                    return Ok(schedule);
                }
            }
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto model)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (checkingPasswordResult.Succeeded)
            {
                var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authenticationOptions.Issuer,
                     audience: _authenticationOptions.Audience,
                     claims: new List<Claim>(),
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

    }
}
