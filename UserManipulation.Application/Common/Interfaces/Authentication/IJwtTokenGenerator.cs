using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Models;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user);
    }
}
