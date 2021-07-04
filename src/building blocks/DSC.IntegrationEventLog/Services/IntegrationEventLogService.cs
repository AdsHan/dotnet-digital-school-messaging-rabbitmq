using DSC.Core.Enums;
using DSC.IntegrationEventLog.Data;
using DSC.IntegrationEventLog.Entities;
using DSC.IntegrationEventLog.Enums;
using DSC.MessageBus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DSC.IntegrationEventLog.Services
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {

        private readonly IntegrationEventLogDbContext _dbContext;

        public IntegrationEventLogService(IntegrationEventLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> SaveEventAsync(Event @event)
        {
            var newEvent = new IntegrationEventLogModel(@event);

            _dbContext.IntegrationEventLogs.Add(newEvent);

            await _dbContext.SaveChangesAsync();

            return newEvent.Id;
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventStatusEnum.Published);
        }

        public Task MarkEventAsNoPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventStatusEnum.NoPublished);
        }

        private async Task UpdateEventStatus(Guid eventId, IntegrationEventStatusEnum status)
        {
            if (eventId == null) return;

            var evt = await _dbContext.IntegrationEventLogs
                            .Where(a => a.Status == EntityStatusEnum.Active)
                            .FirstOrDefaultAsync(e => e.Id == eventId);

            if (evt == null) return;

            evt.PublishStatus = status;

            _dbContext.IntegrationEventLogs.Update(evt);

            await _dbContext.SaveChangesAsync();

        }
    }
}

