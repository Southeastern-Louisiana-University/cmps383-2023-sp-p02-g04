using Microsoft.AspNetCore.Identity;

namespace SP23.P02.Web.Features.Users
{
    public class User : IdentityUser<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Role { get; set; }
    }
}