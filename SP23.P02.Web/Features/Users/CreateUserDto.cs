namespace SP23.P02.Web.Features.Users
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
