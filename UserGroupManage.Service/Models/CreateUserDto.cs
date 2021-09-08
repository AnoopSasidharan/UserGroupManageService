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
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int UserTypeId { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserRole { get; set; }

    }
}
