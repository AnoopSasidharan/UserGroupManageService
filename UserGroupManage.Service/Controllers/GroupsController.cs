using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;
using UserGroupManage.Service.Services;

namespace UserGroupManage.Service.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupsController(IGroupRepository groupRepository,
                                IMapper mapper)
        {
            this._groupRepository = groupRepository;
            this._mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups([FromQuery] GroupSearchDto groupSearchDto)
        {
            //return Ok(new List<Group>() { new Group() });
            //var groups = await _userGroupRepository.GetAllGroupsAsync(groupSearchDto);
            var groups = await _groupRepository.GetAllGroupsAsync(groupSearchDto);
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }
        [HttpGet("{Id}", Name = "GetGroupById")]
        public async Task<ActionResult<GroupDto>> GetGroupById(int Id)
        {
            var group = await _groupRepository.GetGroupAsync(Id);
            if (group == null)
                return NotFound();
            return Ok(_mapper.Map<GroupDto>(group));
        }
        [HttpPost()]
        [Authorize(Roles = "Admin,Helpdesk")]
        public async Task<ActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);
            group.CreatedDate = DateTime.Now;
            _groupRepository.AddGroup(group);
            await _groupRepository.SaveRepositoryAsync();
            return CreatedAtRoute("GetGroupById", new { group.Id }, _mapper.Map<GroupDto>(group));
        }
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin,Helpdesk")]
        public async Task<ActionResult> DeleteGroup(int Id)
        {
            var group = await _groupRepository.GetGroupAsync(Id);
            if (group == null)
            {
                return NotFound();
            }

            _groupRepository.RemoveGroup(group);
            await _groupRepository.SaveRepositoryAsync();
            return NoContent();
        }
    }
}
