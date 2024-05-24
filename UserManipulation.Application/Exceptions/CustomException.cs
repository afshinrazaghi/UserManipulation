using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Exceptions
{
    public class CustomException(string message) : Exception(message)
    {
        public virtual string Title => "Exception";
    }
}
