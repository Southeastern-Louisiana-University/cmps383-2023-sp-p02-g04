﻿namespace SP23.P02.Web.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string[] Role { get; set; } = Array.Empty<string>();
    }
}

