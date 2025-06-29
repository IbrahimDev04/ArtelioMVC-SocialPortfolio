using System;
using Artelio.MVC.Contexts;
using Artelio.MVC.Entities;
using Artelio.MVC.Exceptions.Common;
using Artelio.MVC.HubService;
using Artelio.MVC.Services.Implements;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Artelio.MVC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesRegistration(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProfileSettingService, ProfileSettingService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();



            return services;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;


            }).AddEntityFrameworkStores<ArtelioContext>().AddDefaultTokenProviders();
            return services;
        }
        public static IServiceCollection AddMicrosoftSql(this IServiceCollection services, string connStr)
        {
            services.AddSqlServer<ArtelioContext>(connStr);
            return services;
        }


        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(opt => opt.AddDefaultPolicy(policy => policy
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(x => true)));

            return services;
        }



    }
}
