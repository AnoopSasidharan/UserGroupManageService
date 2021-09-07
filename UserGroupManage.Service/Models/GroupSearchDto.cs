using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserGroupManage.Service.Models
{
    public class GroupSearchDto
    {
        public string Name { get; set; }
        public bool IncludeUsers { get; set; } = false;
    }
}
