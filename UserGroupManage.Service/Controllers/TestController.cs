using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Services;

namespace UserGroupManage.Service.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController:ControllerBase
    {
        private readonly UserGroupRepository _userGroupRepository;

        public TestController(UserGroupRepository userGroupRepository)
        {
            this._userGroupRepository = userGroupRepository;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var group = await _userGroupRepository.GetGroupAsync(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                
            }
            return Ok("test ok");
        }
    }
}
