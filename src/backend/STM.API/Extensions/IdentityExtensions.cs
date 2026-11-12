using Microsoft.AspNetCore.Identity;
using STM.Domain.Entities;
using STM.Infrastructure.Persistence;

namespace STM.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(15);
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<STMDBContext>()
            .AddSignInManager();

            return services;
        }
    }
}
