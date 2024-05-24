using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Application.Persistence.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserWithUserName(string userName);

        Task<IEnumerable<User>> GetUserList();

        Task<User?> GetFirstUser();
    }
}
