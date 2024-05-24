using AutoMapper;
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
using UserManipulation.Application.DOTs.Auth;
using UserManipulation.Application.Features.Users.Login.Commands;
using UserManipulation.Application.Features.Users.Login.Common;
using UserManipulation.Application.Mapping;
using UserManipulation.Infrastructure.Persistence;

namespace UserManipulation.API.Tests.Systems.API.Controllers
{
    public class TestAccountController
    {
        [Fact]
        public async void Login_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync(new LoginResponse() { Token = "Token Value" });

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new AccountController(mockSender.Object, mapper);
            // Act

            var result = (OkObjectResult)await sut.Login(new LoginRequest { });

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Login_OnValidCredentials_ReturnToken()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(It.Is<LoginCommand>(x => x.UserName == "09195512635" && x.Password == "123456"), default))
                .ReturnsAsync(new LoginResponse() { Token = "Token Value" });

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new AccountController(mockSender.Object, mapper);

            // Act

            var result = (OkObjectResult)await sut.Login(new LoginRequest { UserName = "09195512635", Password = "123456" });

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<LoginResponse>();
            ((LoginResponse)result.Value!).Token.Should().Be("Token Value");

        }


        [Fact]
        public async void Login_OnInvalidCredentials_ReturnUnAuthorized()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            mockSender.Setup(x => x.Send(It.Is<LoginCommand>(c => 
            c.UserName != "09195512635" || (c.UserName == "09195512635" && c.Password != "123456")), default))
                .ReturnsAsync(new LoginResponse());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var sut = new AccountController(mockSender.Object, mapper);

            // Act

            var result = (UnauthorizedResult)await sut.Login(new LoginRequest { UserName = "09125566356", Password = "558695" });

            // Assert

            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}

