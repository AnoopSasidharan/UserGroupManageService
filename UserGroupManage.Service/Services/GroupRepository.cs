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
    public class GroupRepository : IGroupRepository
    {
        private readonly UserGroupManagementDbContext _dbContext;

        public GroupRepository(UserGroupManagementDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Group>> GetAllGroupsAsync(GroupSearchDto groupSearchDto)
        {
            var groups = _dbContext.Groups as IQueryable<Group>;
            if (!string.IsNullOrWhiteSpace(groupSearchDto.Name))
            {
                groups = groups.Where(g => g.Name.Contains(groupSearchDto.Name));
            }
            if (groupSearchDto.IncludeUsers)
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
        public async Task<bool> SaveRepositoryAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
