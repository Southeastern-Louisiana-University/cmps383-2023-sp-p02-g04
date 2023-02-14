﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SP23.P02.Web.Features.Users
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            
        }
    }
}