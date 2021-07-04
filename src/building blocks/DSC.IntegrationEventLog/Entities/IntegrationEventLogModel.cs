using DSC.Core.DomainObjects;
using DSC.IntegrationEventLog.Enums;
using DSC.MessageBus;
using System;
using System.Text.Json;

namespace DSC.IntegrationEventLog.Entities
{
    public class IntegrationEventLogModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public IntegrationEventLogModel()
        {

        }

        public IntegrationEventLogModel(Event evt)
        {
            Id = new Guid();
            EventType = evt.GetType().Name;
            EventBody = JsonSerializer.Serialize(evt, evt.GetType());
            PublishStatus = IntegrationEventStatusEnum.Created;
        }

        public IntegrationEventStatusEnum PublishStatus { get; set; }
        public string EventType { get; private set; }
        public string EventBody { get; private set; }

    }
}
