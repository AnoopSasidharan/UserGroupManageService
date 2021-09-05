using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Services
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly UserGroupDbContext _dbContext;

        public UserGroupRepository(UserGroupDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _dbContext.Groups.ToListAsync();
        }
        public async Task<Group> GetGroupAsync(int GroupId)
        {
            return await _dbContext.Groups.FindAsync(GroupId);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<User> GetUserAsync(int UserId)
        {
            return await _dbContext.Users.FindAsync(UserId);
        }
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
        }
        public void RemoveUser(User user)
        {
            _dbContext.Remove(user);
        }
        public async Task<bool> SaveRepositoryAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
