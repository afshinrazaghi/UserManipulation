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
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetUserList());
        }
    }
}
