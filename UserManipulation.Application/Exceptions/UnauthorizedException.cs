using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Exceptions
{
    public class UnauthorizedException(string message) : CustomException(message)
    {
        public override string Title => "Unauthorized";
    }
}
