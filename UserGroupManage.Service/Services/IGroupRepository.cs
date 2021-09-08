using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Services
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupAsync(int GroupId);
    }
}
