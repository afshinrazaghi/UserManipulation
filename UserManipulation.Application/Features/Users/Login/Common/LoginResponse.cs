using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Features.Users.Login.Common
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
