﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using zaloclone_test.Models;
using zaloclone_test.Services;

namespace zaloclone_test.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with connection string from the configuration
            services.AddDbContext<Zalo_CloneContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyDB")));

            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }
    }
}
