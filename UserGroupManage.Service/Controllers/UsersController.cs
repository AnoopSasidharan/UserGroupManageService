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
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            this._userGroupRepository = userGroupRepository;
            this._mapper = mapper;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userGroupRepository.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{Id}", Name ="GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int Id)
        {
            var user = await _userGroupRepository.GetUserAsync(Id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.CreatedDate = DateTime.Now;
            if (userDto.UserTypeId < 1)
            {
                var types = await _userGroupRepository.GetUserTypesAsync();
                var regular = types.FirstOrDefault(t => t.Description == "Regular");
                if(regular!=null)
                {
                    user.UserTypeId = regular.TypeId;
                }
            }
            _userGroupRepository.AddUser(user);
            await _userGroupRepository.SaveRepositoryAsync();
            return CreatedAtRoute("GetUserById", new { user.Id }, user);
        }
        //[HttpPatch("{Id}")]
        //public async Task<ActionResult> PatchUser([FromBody] CreateUserDto userDto)
        //{
        //    var user = _mapper.Map<User>(userDto);
        //    user.CreatedDate = DateTime.Now;
        //    _userGroupRepository.AddUser(user);
        //    await _userGroupRepository.SaveRepositoryAsync();
        //    return CreatedAtRoute("GetUserById", new { user.Id }, user);
        //}
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var user = await _userGroupRepository.GetUserAsync(Id);
            if(user==null)
            {
                return NotFound();
            }

            _userGroupRepository.RemoveUser(user);
            await _userGroupRepository.SaveRepositoryAsync();
            return NoContent();
        }
    }
}
