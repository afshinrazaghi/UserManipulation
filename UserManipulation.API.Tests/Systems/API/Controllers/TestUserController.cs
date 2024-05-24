using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.API.Controllers;
using UserManipulation.API.Tests.Fixtures;
using UserManipulation.Application.Common.Interfaces.MessageBrokers;
using UserManipulation.Application.Features.Users.GetUsers.Common;
using UserManipulation.Application.Features.Users.GetUsers.Queries;

namespace UserManipulation.API.Tests.Systems.API.Controllers
{
    public class TestUserController
    {
        #region GetUserList
        [Fact]
        public async void GetUserList_OnSuccess_Returns200()
        {
            // Arrange
            var mockISender = new Mock<ISender>();
            var mockIMessageProducer = new Mock<IMessageProducer>();

            var sut = new UserController(mockISender.Object, mockIMessageProducer.Object);

            // Act
            var result = (OkObjectResult)await sut.GetUserList();

            // Assert
            result.StatusCode.Should().Be(200);

        }

        [Fact]
        public async void GetUserList_OnSuccess_ReturnUsersList()
        {
            // Arrange
            var mockISender = new Mock<ISender>();
            var userList = UsersFixture.GetTestUsers();
            mockISender.Setup(x => x.Send(It.IsAny<GetUserListQuery>(), default)).ReturnsAsync(userList);
            var mockIMessageProducer = new Mock<IMessageProducer>();

            var sut = new UserController(mockISender.Object, mockIMessageProducer.Object);

            // Act
            var result = (OkObjectResult)await sut.GetUserList();

            // Assert
            result.Value.Should().BeOfType<List<UserResponse>>();
            ((List<UserResponse>)result.Value!).Should().HaveCount(userList.Count());
        }
        #endregion


        #region SendUser
        [Fact]
        public async void SendUser_OnSuccess_ReturnStatusCode200()
        {
            // Arrange
            var mockISender = new Mock<ISender>();
            var mockIMessageProducer = new Mock<IMessageProducer>();
            var sut = new UserController(mockISender.Object, mockIMessageProducer.Object);

            // Act

            var result = (OkObjectResult)await sut.SendUser();

            // Assert
            result.StatusCode.Should().Be(200);
        } 
        #endregion
    }
}
