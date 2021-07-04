using DSC.IntegrationEventLog.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DSC.IntegrationEventLog.Data
{
    public class IntegrationEventLogDbContext : DbContext
    {
        public IntegrationEventLogDbContext(DbContextOptions<IntegrationEventLogDbContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogModel> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }

}
