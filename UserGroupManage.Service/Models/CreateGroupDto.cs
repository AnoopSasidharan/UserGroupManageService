using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserGroupManage.Service.Models
{
    public class CreateGroupDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
    }
}
