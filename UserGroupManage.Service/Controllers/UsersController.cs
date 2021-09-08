using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(IUserGroupRepository userGroupRepository, 
                               IMapper mapper,
                               UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager)
        {
            this._userGroupRepository = userGroupRepository;
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userGroupRepository.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
        [HttpGet("{Id}", Name ="GetUserById")]
        public async Task<ActionResult<UserDto>> GetUserById(string Id)
        {
           // var user = await _userGroupRepository.GetUserAsync(Id);
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }
        [HttpPost()]
        [Authorize(Roles = "Admin,Helpdesk")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            user.UserName = user.Email;

            var createResult = await this._userManager.CreateAsync(user, userDto.Password);
            if(!createResult.Succeeded)
            {
                return BadRequest();
            }

            var rolesResult= await _userManager.AddClaimsAsync(user, new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(JwtClaimTypes.GivenName,user.FirstName),
                new Claim(JwtClaimTypes.FamilyName,user.LastName),
                new Claim(JwtClaimTypes.Role,string.IsNullOrWhiteSpace( userDto.UserRole)? "Regular" : userDto.UserRole)
            });

            if(!rolesResult.Succeeded)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetUserById", new { user.Id }, _mapper.Map<UserDto>(user));
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
        [Authorize(Roles = "Admin,Helpdesk")]
        public async Task<ActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user==null)
            {
                return NotFound();
            }

            var deleteresult= await _userManager.DeleteAsync(user);
            if (!deleteresult.Succeeded)
                return BadRequest(deleteresult.Errors);
            return NoContent();
        }
    }
}
