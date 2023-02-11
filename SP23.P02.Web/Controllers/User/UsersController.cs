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
        public ActionResult<TrainStationDto> Details([FromRoute] int Id)
        {
            var Userid = _context.Users.FirstOrDefault(x => x.Id == Id);

            if (Userid == null)
            {
                return NotFound($"Unable to find Id {Id}");
            }

            return Ok(new UserDto
            {
                Id = Userid.Id,
               Name = Userid.UserName,
                //Roles = Userid.Roles,
            });
        }

        [HttpPost]
        public ActionResult<UserDto> Create(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Name must be provided");
            }
            if (user.Name.Length > 120)
            {
                return BadRequest("Name cannot be longer than 120 characters");
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Must have an address");
            }

            var returnCreatedStation = new TrainStation
            {
                Name = user.Name,
                //   Address = trainStation.Roles,
            };
            // _context.Users.Add(returnCreatedStation);
            _context.SaveChanges();

            user.Id = returnCreatedStation.Id;

            return CreatedAtAction(nameof(Details), new { Id = returnCreatedStation.Id }, returnCreatedStation);

        }
    }
}
       