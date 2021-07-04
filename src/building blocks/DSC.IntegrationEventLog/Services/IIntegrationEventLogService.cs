using DSC.MessageBus;
using System;
using System.Threading.Tasks;

namespace DSC.IntegrationEventLog.Services
{
    public interface IIntegrationEventLogService
    {
        Task<Guid> SaveEventAsync(Event @event);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsNoPublishedAsync(Guid eventId);
    }
}
