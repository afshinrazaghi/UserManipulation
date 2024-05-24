
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManipulation.API
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiConfiguration).Assembly);
            

            return services;
        }
    }
}
