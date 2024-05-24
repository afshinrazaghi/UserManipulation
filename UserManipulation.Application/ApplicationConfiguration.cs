using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationConfiguration).Assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(ApplicationConfiguration).Assembly));
            return services;
        }

    }
}
