using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Persistence.Contracts;
using UserManipulation.Domain.Entities;

namespace UserManipulation.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private const string TableName = "User";
        public UserRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<User?> GetUserWithUserName(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var user = await connection.QueryFirstOrDefaultAsync<User>(
                           "GetUserWithUserName",
                           new { UserName = userName },
                           commandType: CommandType.StoredProcedure
                );

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUserList()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var users = await connection.QueryAsync<User>(
                           "GetUserList",
                           null,
                           commandType: CommandType.StoredProcedure
                );

                return users;
            }
        }


        public async Task<User?> GetFirstUser()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var user = await connection.QueryFirstOrDefaultAsync<User>(
                    "GetFirstUser",
                    null,
                    commandType: CommandType.StoredProcedure);

                return user;
            }
        }
    }
}
