using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Features.Users.GetUsers.Common;
using UserManipulation.Domain.Entities;

namespace UserManipulation.API.Tests.Fixtures
{
    public static class UsersFixture
    {
        public static List<UserResponse> GetTestUsers() => new()
        {
            new UserResponse
            {
                Id = 1,
                FirstName = "Ali",
                LastName ="Afshar",
                UserName ="09102222222"

            },
            new UserResponse
            {
                Id = 2,
                FirstName = "Afshin",
                LastName ="Razaghi",
                UserName ="09195512635"
            },
            new UserResponse
            {
                Id = 3,
                FirstName = "Mehdi",
                LastName ="Sharbati",
                UserName ="09204455888"
            }
        };
    }
}
