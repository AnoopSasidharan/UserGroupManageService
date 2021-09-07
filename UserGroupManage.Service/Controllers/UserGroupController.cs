using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        public UserGroupController(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
        }

        [HttpPost("{GroupId}/{UserId}")]
        [Authorize(Roles = "Admin,Helpdesk")]
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
                var newUser = _mapper.Map<User>(userDto);
                _userGroupRepository.AddUser(newUser);//automapper
                await _userGroupRepository.SaveRepositoryAsync();
                user = newUser;
            }

            group.Users.Add(user);
            await _userGroupRepository.SaveRepositoryAsync();

            return Ok();
        }
    }
}
