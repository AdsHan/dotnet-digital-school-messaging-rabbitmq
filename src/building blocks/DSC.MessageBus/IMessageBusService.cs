using RabbitMQ.Client.Events;
using System;

namespace DSC.MessageBus
{
    public interface IMessageBusService
    {
        public void Publish<T>(string queue, T message) where T : Event;
        public void Subscribe(string queue, Action<BasicDeliverEventArgs> callback);
    }
}
