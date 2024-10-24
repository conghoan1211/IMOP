﻿using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;
using zaloclone_test.Services;
using zaloclone_test.Utilities;

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
            services.AddScoped<JwtAuthentication>(); // Register JwtAuthentication
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAsideContactService, AsideContactService>();
            services.AddScoped<IInvitationService, InvitationService>();
        }
    }
}