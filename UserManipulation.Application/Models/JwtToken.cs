using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Models
{
    public record JwtToken(string Token, DateTime ExpireDate);
}
