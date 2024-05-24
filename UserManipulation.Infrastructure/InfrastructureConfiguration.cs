using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Application.Common.Interfaces.Authentication;
using UserManipulation.Application.Common.Interfaces.MessageBrokers;
using UserManipulation.Application.Persistence.Contracts;
using UserManipulation.Infrastructure.Authentication;
using UserManipulation.Infrastructure.MessageBrokers;
using UserManipulation.Infrastructure.Persistence;
using UserManipulation.Infrastructure.Settings;

namespace UserManipulation.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddRepositories();
            var jwtSettings = new JwtSettings();
            config.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });
            return services;
        }



        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
