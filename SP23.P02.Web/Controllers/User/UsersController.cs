using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers.User
{
    /*
    private readonly DbSet<TrainStation> stations;
    private readonly DataContext dataContext;
   */

    [ApiController]
    [Route("/api/User")]
    public class UsersController : ControllerBase
    {
        private DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<UserDto[]> Get()
        {
            var result = _context.Users;
            return Ok(result.Select(x => new UserDto
            {
                Id = x.Id,
            })); ; ;
        }



        [HttpGet("{Id}")]
        public ActionResult<UserDto> Details([FromRoute] int Id)
        {
            var Userid = _context.Users.FirstOrDefault(x => x.Id == Id);

            if (Userid == null)
            {
                return NotFound($"Unable to find Id {Id}");
            }

            return Ok(new UserDto
            {
                Id = Userid.Id,
                UserName = Userid.UserName,
                //Roles = Userid.Roles
            });
        }

        [HttpPost("{CreateUser}")]
        public ActionResult<UserDto> Create(CreateUserDto user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Name must be provided");
            }
            if (user.UserName.Length > 120)
            {
                return BadRequest("Name cannot be longer than 120 characters");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Must have an address");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Name must be provided");
            }
            if (user.Password.Length > 120)
            {
                return BadRequest("Password too long!");
            }
            /*
            if (!string.IsNullOrWhiteSpace(user.Roles))
            {
                return BadRequest("Name must be provided");
            }
            if (user.Roles.Length > 120)
            {
            return BadRequest("Password too long!");
            }
            */

                var returnCreatedUser = new CreateUserDto
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Roles = user.Roles,
                };

                _context.SaveChanges();

                user.UserName = returnCreatedUser.UserName;
                user.Password = returnCreatedUser.Password;
                user.Roles = returnCreatedUser.Roles;

                //_context.Users.Add(returnCreatedUser);

                return CreatedAtAction(nameof(Details), new { Id = returnCreatedUser.UserName }, returnCreatedUser.UserName);
            }
            

        
    }
}
