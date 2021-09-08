using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserGroupManage.Service.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController:ControllerBase
    {
        public ActionResult<string> Get()
        {
            return Ok("test");
        }
    }
}
