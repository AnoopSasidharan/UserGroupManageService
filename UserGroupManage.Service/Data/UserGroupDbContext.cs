using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Data
{
    public class UserGroupDbContext : DbContext
    {
        public UserGroupDbContext(DbContextOptions<UserGroupDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}
