using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers.Authentication;



[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly DataContext dataContext;

    public AuthenticationController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("api/authentication/login")]

    public ActionResult<LoginDto> Login(LoginDto loginDto)
    {
        if (IsInvalid(loginDto))
        {
            return BadRequest();
        }
    }



}
