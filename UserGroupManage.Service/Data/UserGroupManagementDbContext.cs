using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Data
{
    public class UserGroupManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserGroupManagementDbContext(DbContextOptions<UserGroupManagementDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}
