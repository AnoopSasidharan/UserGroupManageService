using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;
using UserGroupManage.Service.Services;

namespace UserGroupManage.Service.Controllers
{
    [ApiController]
    [Route("api/usergroups")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public UserGroupController(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        [HttpPost("{GroupId}/{UserId}")]
        public async Task<ActionResult> AddUserToGroup(int GroupId,int UserId, [FromBody] CreateUserDto userDto)
        {
            var group = await _userGroupRepository.GetGroupAsync(GroupId);
            if(group==null)
            {
                return NotFound($"Group with id = {GroupId} is not found");
            }

            var user = await _userGroupRepository.GetUserAsync(UserId);

            if(user==null)
            {
                _userGroupRepository.AddUser(user);//automapper
                await _userGroupRepository.SaveRepositoryAsync();
            }

            group.Users.Add(user);
            await _userGroupRepository.SaveRepositoryAsync();

            return Ok();
        }
    }
}
