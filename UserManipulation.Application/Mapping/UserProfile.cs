using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.DOTs.Auth;
using UserManipulation.Application.Features.Users.GetUsers.Common;
using UserManipulation.Application.Features.Users.Login.Commands;
using UserManipulation.Application.Features.Users.Login.Common;
using UserManipulation.Application.Models;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<JwtToken, LoginResponse>();
            CreateMap<User, UserResponse>();
        }
    }
}
