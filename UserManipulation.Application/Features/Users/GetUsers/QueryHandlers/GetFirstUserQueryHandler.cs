using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Features.Users.GetUsers.Common;
using UserManipulation.Application.Features.Users.GetUsers.Queries;
using UserManipulation.Application.Persistence.Contracts;

namespace UserManipulation.Application.Features.Users.GetUsers.QueryHandlers
{
    public class GetFirstUserQueryHandler : IRequestHandler<GetFirstUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetFirstUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetFirstUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetFirstUser();
            return _mapper.Map<UserResponse>(user);
        }
    }
}
