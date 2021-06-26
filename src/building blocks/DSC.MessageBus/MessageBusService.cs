using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace DSC.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly string _connectionString;

        private ConnectionFactory _factory;
        private IModel _channel;

        public MessageBusService(string connectionString)
        {
            _connectionString = connectionString;
            CreateBus();
        }

        public void Publish<T>(string queue, T message) where T : Event
        {
            _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Cria mensagem e envia para fila 
            var contentJson = JsonSerializer.Serialize<object>(message);
            var contentBytes = Encoding.UTF8.GetBytes(contentJson);

            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: contentBytes);
        }

        public void Subscribe(string queue, Action<BasicDeliverEventArgs> callback)
        {
            _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                callback(eventArgs);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(queue, false, consumer);
        }

        private void CreateBus()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri(_connectionString);
            _channel = _factory.CreateConnection().CreateModel();
        }

    }
}
