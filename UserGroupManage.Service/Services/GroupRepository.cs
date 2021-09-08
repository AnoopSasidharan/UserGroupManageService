using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Services
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UserGroupManagementDbContext _dbContext;

        public GroupRepository(UserGroupManagementDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Group> GetGroupAsync(int GroupId)
        {
            return await _dbContext.Groups.FindAsync(GroupId);
        }
    }
}
