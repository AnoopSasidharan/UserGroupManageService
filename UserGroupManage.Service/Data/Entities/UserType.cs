using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserGroupManage.Service.Data.Entities
{
    public class UserType
    {
        [Key]
        public int TypeId { get; set; }
        public string Description { get; set; }
    }
}
