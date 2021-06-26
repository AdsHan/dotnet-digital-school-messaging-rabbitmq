using RabbitMQ.Client.Events;

namespace DSC.MessageBus
{
    public interface IConsumer
    {
        void RegisterConsumer(BasicDeliverEventArgs message);
    }
}
