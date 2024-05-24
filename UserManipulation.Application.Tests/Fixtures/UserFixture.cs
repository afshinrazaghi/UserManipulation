using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Application.Tests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Id = 1,
                FirstName = "Ali",
                LastName ="Afshar",
                UserName ="09102222222"

            },
            new User
            {
                Id = 2,
                FirstName = "Afshin",
                LastName ="Razaghi",
                UserName ="09195512635"
            },
            new User
            {
                Id = 3,
                FirstName = "Mehdi",
                LastName ="Sharbati",
                UserName ="09204455888"
            }
        };
    }
}
