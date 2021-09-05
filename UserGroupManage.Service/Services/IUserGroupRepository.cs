using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;

namespace UserGroupManage.Service.Services
{
    public interface IUserGroupRepository
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<Group> GetGroupAsync(int GroupId);
        void AddGroup(Group group);
        void RemoveGroup(Group group);

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int UserId);
        void AddUser(User user);
        void RemoveUser(User user);

        Task<IEnumerable<UserType>> GetUserTypesAsync();
        Task<UserType> GetUserTypesByIdAsync(int Id);
        void AddUserType(UserType userType);
        void RemoveUserType(UserType userType);
        Task<bool> SaveRepositoryAsync();

        
    }
}