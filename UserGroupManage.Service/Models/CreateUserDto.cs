using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserGroupManage.Service.Models
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }

    }
}
