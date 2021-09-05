using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;
using UserGroupManage.Service.Services;

namespace UserGroupManage.Service.Controllers
{
    public class GroupsController:ControllerBase
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public GroupsController(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            this._userGroupRepository = userGroupRepository;
            this._mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            var groups = await _userGroupRepository.GetAllGroupsAsync();
            return Ok(groups);
        }
        [HttpGet("{Id}", Name = "GetGroupById")]
        public async Task<ActionResult<Group>> GetGroupById(int Id)
        {
            var group = await _userGroupRepository.GetGroupAsync(Id);
            if (group == null)
                return NotFound();
            return Ok(group);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);
            group.CreatedDate = DateTime.Now;
            _userGroupRepository.AddGroup(group);
            await _userGroupRepository.SaveRepositoryAsync();
            return CreatedAtRoute("GetGroupById", new { group.Id }, group);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteGroup(int Id)
        {
            var group = await _userGroupRepository.GetGroupAsync(Id);
            if (group == null)
            {
                return NotFound();
            }

            _userGroupRepository.RemoveGroup(group);
            await _userGroupRepository.SaveRepositoryAsync();
            return NoContent();
        }
    }
}
