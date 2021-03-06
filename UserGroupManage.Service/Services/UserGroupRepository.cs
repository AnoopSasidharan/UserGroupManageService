
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;

namespace UserGroupManage.Service.Services
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly UserGroupManagementDbContext _dbContext;

        public UserGroupRepository(UserGroupManagementDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Group>> GetAllGroupsAsync(GroupSearchDto groupSearchDto)
        {
            var groups = _dbContext.Groups as IQueryable<Group>;
            if(!string.IsNullOrWhiteSpace(groupSearchDto.Name))
            {
                groups = groups.Where(g => g.Name.Contains(groupSearchDto.Name));
            }
            if(groupSearchDto.IncludeUsers)
            {
                groups = groups.Include(g => g.Users).ThenInclude(u => u.UserType);
            }
            return (await groups.ToListAsync());
        }
        public async Task<Group> GetGroupAsync(int GroupId)
        {
            return await _dbContext.Groups.Include(g => g.Users).ThenInclude(u => u.UserType)
                .FirstOrDefaultAsync(g => g.Id == GroupId);
        }
        public void AddGroup(Group group)
        {
            _dbContext.Groups.Add(group);
        }
        public void RemoveGroup(Group group)
        {
            _dbContext.Remove(group);
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _dbContext.ApplicationUsers.Include(u=> u.UserType).ToListAsync();
        }
        public async Task<ApplicationUser> GetUserAsync(string UserId)
        {
            //return null;
            return await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u=> u.Id== UserId);
        }
        public void AddUser(User user)
        {
            //_dbContext.Users.Add(user);
        }
        public void RemoveUser(User user)
        {
            _dbContext.Remove(user);
        }
        public async Task<IEnumerable<UserType>> GetUserTypesAsync()
        {
            return await _dbContext.UserTypes.ToListAsync();
        }
        public async Task<UserType> GetUserTypesByIdAsync(int Id)
        {
            return await _dbContext.UserTypes.FindAsync(Id);
        }
        public void AddUserType(UserType userType)
        {
            _dbContext.UserTypes.Add(userType);
        }
        public void RemoveUserType(UserType userType)
        {
            _dbContext.UserTypes.Remove(userType);
        }
        public async Task<bool> SaveRepositoryAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
