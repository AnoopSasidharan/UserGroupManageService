using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;

namespace UserGroupManage.Service.Services
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync(GroupSearchDto groupSearchDto);
        Task<Group> GetGroupAsync(int GroupId);
        void AddGroup(Group group);
        void RemoveGroup(Group group);
        Task<bool> SaveRepositoryAsync();
    }
}
