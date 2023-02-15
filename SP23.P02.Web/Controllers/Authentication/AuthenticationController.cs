using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Users;
using Microsoft.AspNetCore.Authorization;

namespace SP23.P02.Web.Controllers.Authentication;



[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly SignInManager<UserDto> signInManager;
    private readonly UserManager<UserDto> userManager;


    public AuthenticationController
          (
              SignInManager<UserDto> signInManager,
              UserManager<UserDto> userManager
          )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpPost("login")]
    [Route("api/authentication/login")]
    [AllowAnonymous]

    public async Task<ActionResult<LoginDto>> Login(LoginDto dto)
    {
        var user = await userManager.FindByNameAsync(dto.UserName);


        if (IsInvalid(dto)|| user == null)
        {
            return BadRequest();

        }
        var passCheck = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (!passCheck.Succeeded)
        {
            return BadRequest();
        }

        await signInManager.SignInAsync(user, true);

        var dtoUser = await GetUserDto(userManager.Users).SingleAsync(x => x.UserName == user.UserName);
        return Ok(dtoUser);


    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    private static IQueryable<UserDto> GetUserDto(IQueryable<UserDto> users)
    {
        return users.Select(x => new UserDto
        {
            Id = x.Id,
            UserName = x.UserName,
        });
    }

    private static bool IsInvalid(LoginDto dto)
    {

        return string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password);
    }


}
