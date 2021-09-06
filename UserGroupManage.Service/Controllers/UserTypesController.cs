using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Services;

namespace UserGroupManage.Service.Controllers
{
    [ApiController]
    [Route("api/usertypes")]
    public class UserTypesController: ControllerBase
    {
        private readonly IUserGroupRepository _userGroupRepository;
        public UserTypesController(IUserGroupRepository userGroupRepository)
        {
            this._userGroupRepository = userGroupRepository;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            return Ok(await _userGroupRepository.GetUserTypesAsync());
        }
        [HttpGet("{Id}", Name ="CreateUserTypeById")]
        public async Task<ActionResult<UserType>> GetUserTypes(int Id)
        {
            var usertype = await _userGroupRepository.GetUserTypesByIdAsync(Id);
            if (usertype == null)
                return NotFound();
            return Ok(usertype);
        }
        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Helpdesk")]
        public async Task<ActionResult> CreateUserType([FromBody] UserType userType)
        {
            _userGroupRepository.AddUserType(userType);
            await _userGroupRepository.SaveRepositoryAsync();
            return CreatedAtRoute("CreateUserTypeById", new { Id = userType.TypeId }, userType);
        }
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Helpdesk")]
        public async Task<ActionResult> DeleteUserTypes(int Id)
        {
            var type = await _userGroupRepository.GetUserTypesByIdAsync(Id);
            if(type==null)
            {
                return NotFound();
            }
            _userGroupRepository.RemoveUserType(type);
            await _userGroupRepository.SaveRepositoryAsync();
            return NoContent();
        }
    }
}
