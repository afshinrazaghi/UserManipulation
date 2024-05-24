using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Features.Users.GetUsers.Common;

namespace UserManipulation.Application.Features.Users.GetUsers.Queries
{
    public class GetFirstUserQuery : IRequest<UserResponse>
    {
    }
}
