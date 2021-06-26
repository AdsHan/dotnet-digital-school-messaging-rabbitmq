using DSC.Core.Mediator;
using DSC.Student.API.Application.Messages.Commands.StudentCommand;
using DSC.Student.Domain.Repositories;
using DSC.Student.Infrastructure.Data;
using DSC.Student.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DSC.Student.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            
            // Usando com banco de dados em memória
            services.AddDbContext<StudentDbContext>(options => options.UseInMemoryDatabase("DigitalSchoolServices"));
            // Usando com SqlServer
            // services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServerCs")));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // services.AddScoped<IRequestHandler<AddStudentCommand, ValidationResult>, StudentCommandHandler>();
            // services.AddScoped<IRequestHandler<AddStudentCommand, ValidationResult>, StudentCommandHandler>();
            services.AddMediatR(typeof(AddStudentCommand));
        }
    }
}