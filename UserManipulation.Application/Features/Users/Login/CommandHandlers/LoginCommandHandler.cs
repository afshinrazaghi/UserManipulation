using AutoMapper;
using MediatR;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Common.Interfaces.Authentication;
using UserManipulation.Application.Exceptions;
using UserManipulation.Application.Features.Users.Login.Commands;
using UserManipulation.Application.Features.Users.Login.Commands.Validators;
using UserManipulation.Application.Features.Users.Login.Common;
using UserManipulation.Application.Persistence.Contracts;

namespace UserManipulation.Application.Features.Users.Login.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, 
            IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new CustomException(string.Join(",", validationResult.Errors));
            }

            var user = await _userRepository.GetUserWithUserName(request.UserName);
            if (user is not null)
            {
                if (_passwordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Success)
                {
                    var generateTokenResult = _jwtTokenGenerator.Generate(user);
                    return _mapper.Map<LoginResponse>(generateTokenResult);
                }
                else
                {
                    throw new CredentialsNotValidException("User Name Or Password is incorrect");
                }
            }
            else
            {
                throw new CredentialsNotValidException("User Name Or Password is incorrect");
            }
        }
    }
}
