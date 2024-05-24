using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Exceptions
{
    public class CredentialsNotValidException(string message) : UnauthorizedException(message)
    {
        public override string Title => "Provided Credentials are not valid";
    }
}
