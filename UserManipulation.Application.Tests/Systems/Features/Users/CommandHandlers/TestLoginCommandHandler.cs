using AutoMapper;
using FluentAssertions;
using Microsoft.AspNet.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Common.Interfaces.Authentication;
using UserManipulation.Application.Exceptions;
using UserManipulation.Application.Features.Users.Login.CommandHandlers;
using UserManipulation.Application.Features.Users.Login.Common;
using UserManipulation.Application.Mapping;
using UserManipulation.Application.Persistence.Contracts;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Application.Tests.Systems.Features.Users.CommandHandlers
{
    public class TestLoginCommandHandler
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("09195512635", "")]
        [InlineData("", "123456")]
        public async void Handle_OnInvalidValidation_ReturnsCustomException(string userName, string password)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher>();
            var mockIJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var sut = new LoginCommandHandler(mockUserRepository.Object, mockPasswordHasher.Object, mockIJwtTokenGenerator.Object, mapper);

            // Act

            var method = async () => await sut.Handle(new Application.Features.Users.Login.Commands.LoginCommand { UserName = userName, Password = password }, default);

            // Assert

            var exception = await method.Should().ThrowAsync<CustomException>();

            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                exception.WithMessage("User Name is mandatory,Password is mandatory");
            }
            else if (string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                exception.WithMessage("User Name is mandatory");
            }
            else if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                exception.WithMessage("Password is mandatory");
            }
        }


        [Theory]
        [InlineData("09196666666")]
        [InlineData("09125566555")]
        public async void Handle_OnUserNameNotExists_ReturnCredentialsNotValidException(string userName)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher>();
            var mockIJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            mockUserRepository.Setup(ur => ur.GetUserWithUserName(It.Is<string>(x => x != "09195512635")))
                .ReturnsAsync((User?)null);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var sut = new LoginCommandHandler(mockUserRepository.Object, mockPasswordHasher.Object, mockIJwtTokenGenerator.Object, mapper);

            // Act

            var method = async () => await sut.Handle(new Application.Features.Users.Login.Commands.LoginCommand { UserName = userName, Password = "112233" }, default);

            // Assert
            await method.Should().ThrowAsync<CredentialsNotValidException>().WithMessage("User Name Or Password is incorrect");
        }

        [Theory]
        [InlineData("09195512635", "123123")]
        [InlineData("09195512635", "444555")]
        public async void Handle_OnPasswordNotCorrect_ReturnCredentialsNotValidException(string userName, string password)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var passwordHasher = new PasswordHasher();
            var mockIJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            mockUserRepository.Setup(ur => ur.GetUserWithUserName(It.Is<string>(x => x == "09195512635")))
                .ReturnsAsync(new User { UserName = userName, Password = passwordHasher.HashPassword("123456") });

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var sut = new LoginCommandHandler(mockUserRepository.Object, passwordHasher, mockIJwtTokenGenerator.Object, mapper);

            // Act

            var method = async () => await sut.Handle(new Application.Features.Users.Login.Commands.LoginCommand { UserName = userName, Password = password }, default);

            // Assert
            await method.Should().ThrowAsync<CredentialsNotValidException>().WithMessage("User Name Or Password is incorrect");
        }

        [Fact]
        public async void Handle_OnValidCredentials_ReturnToken()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var passwordHasher = new PasswordHasher();
            var mockIJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            string fakeToken = "Token Value";
            var expireDate = DateTime.UtcNow.AddMinutes(30);
            mockUserRepository.Setup(ur => ur.GetUserWithUserName(It.Is<string>(x => x == "09195512635")))
                .ReturnsAsync(new User { UserName = "09195512635", Password = passwordHasher.HashPassword("123456") });

            mockIJwtTokenGenerator.Setup(x => x.Generate(It.IsAny<User>())).Returns(new Models.JwtToken(fakeToken, expireDate));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var sut = new LoginCommandHandler(mockUserRepository.Object, passwordHasher, mockIJwtTokenGenerator.Object, mapper);

            // Act

            var result = await sut.Handle(new Application.Features.Users.Login.Commands.LoginCommand { UserName = "09195512635", Password = "123456" }, default);

            //Assert

            result.Should().BeOfType<LoginResponse>();
            result.Token.Should().NotBeNull();
            result.Token.Should().Be(fakeToken);
            result.ExpireDate.Should().Be(expireDate);


        }
    }
}
