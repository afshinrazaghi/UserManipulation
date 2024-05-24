using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Features.Users.GetUsers.Queries;
using UserManipulation.Application.Features.Users.GetUsers.QueryHandlers;
using UserManipulation.Application.Mapping;
using UserManipulation.Application.Persistence.Contracts;
using UserManipulation.Application.Tests.Fixtures;

namespace UserManipulation.Application.Tests.Systems.Features.Users.QueryHandlers
{
    public class TestGetUserListQueryHandler
    {
        [Fact]
        public async void GetUserList_OnCall_ReturnUserList()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var usersList = UsersFixture.GetTestUsers();
            mockUserRepository.Setup(x => x.GetUserList()).ReturnsAsync(usersList);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });

            var mapper = mockMapper.CreateMapper();
            var sut = new GetUserListQueryHandler(mockUserRepository.Object, mapper);

            // Act 

            var result = (await sut.Handle(new GetUserListQuery(), default)).ToList();

            //Assert

            result.Should().HaveCount(usersList.Count());
            for (int i = 0; i < usersList.Count; i++)
            {
                var user = usersList[i];
                var userListItem = result[i];

                user.FirstName.Should().BeEquivalentTo(userListItem.FirstName);
                user.LastName.Should().BeEquivalentTo(userListItem.LastName);
                user.UserName.Should().BeEquivalentTo(userListItem.UserName);
            }
        }
    }
}
