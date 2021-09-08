using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGroupController(IUserGroupRepository userGroupRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("{GroupId}/{UserId}")]
        [Authorize(Roles = "Admin,Helpdesk")]
        public async Task<ActionResult> AddUserToGroup(int GroupId,string UserId)
        {
            var group = await _userGroupRepository.GetGroupAsync(GroupId);
            if(group==null)
            {
                return NotFound($"Group with id = {GroupId} is not found");
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if(user==null)
            {
               return NotFound($"User with id = {UserId} is not found");
            }

            group.Users.Add(user);
            await _userGroupRepository.SaveRepositoryAsync();

            return Ok();
        }
    }
}
