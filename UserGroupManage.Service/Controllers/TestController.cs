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
        private readonly IGroupRepository _groupRepository;

        public TestController(IGroupRepository groupRepository)
        {
            this._groupRepository = groupRepository;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var group = await _groupRepository.GetGroupAsync(1);
                return Ok(group.Name);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                
            }
            
        }
    }
}
