using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Common.Interfaces.Authentication;
using UserManipulation.Application.Models;
using UserManipulation.Domain.Entities;
using UserManipulation.Infrastructure.Settings;

namespace UserManipulation.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public JwtToken Generate(User user)
        {

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256
            );

            string fullName = (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)) ? user.FirstName + " " + user.LastName : "";

            var claims = new Claim[]
            {
               new Claim( JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.GivenName, fullName),
               new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var tokenExpireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                 audience: _jwtSettings.Audience,
                 claims: claims,
                 expires: tokenExpireDate,
                 signingCredentials: signingCredentials
           );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return new JwtToken(token, tokenExpireDate);
        }
    }
}
