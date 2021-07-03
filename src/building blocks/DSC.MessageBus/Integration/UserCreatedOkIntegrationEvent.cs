using System;

namespace DSC.MessageBus.Integration
{
    public class UserCreatedOkIntegrationEvent : Event
    {
        public UserCreatedOkIntegrationEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}