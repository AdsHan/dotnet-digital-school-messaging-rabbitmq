using DSC.IntegrationEventLog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.IntegrationEventLog.Data.Mapping
{
    public class IntegrationEventLogConfigurations : IEntityTypeConfiguration<IntegrationEventLogModel>
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLogModel> builder)
        {

            builder.HasKey(e => e.Id);

            builder.ToTable("IntegrationEventLogs");
        }
    }
}
