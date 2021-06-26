using DSC.Auth.API.Application.Messages.Commands.UserCommand;
using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using DSC.Auth.Infrastructure.Data;
using DSC.Auth.Infrastructure.Data.Repositories;
using DSC.Core.Extensions;
using DSC.Core.Mediator;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DSC.Auth.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            // Usando com banco de dados em memória
            services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("DigitalSchoolServices"));
            // Usando com SqlServer
            // services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServerCs")));

            services.AddIdentity<UserModel, IdentityRole>(options =>
                {
                    // Configurações de senha
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 2;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddErrorDescriber<IdentityPortugues>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddMediatR(typeof(AddUserCommand));
        }

    }
}