using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;

namespace UserGroupManage.Service.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
        }
    }
}
