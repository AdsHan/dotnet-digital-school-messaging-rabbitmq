using DSC.Core.Mediator;
using DSC.MessageBus;
using DSC.MessageBus.Integration;
using DSC.Student.API.Application.Messages.Commands.StudentCommand;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Student.API.Application.Messages.ConsumersBus
{
    public class UserCreatedOkConsumer : BackgroundService, IUserCreatedOkConsumer
    {

        private readonly IMessageBusService _messageBusService;
        private readonly IServiceProvider _serviceProvider;

        public UserCreatedOkConsumer(IMessageBusService messageBusService, IServiceProvider serviceProvider)
        {
            _messageBusService = messageBusService;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBusService.Subscribe(QueueType.CREATED_OK, RegisterConsumer);
            return Task.CompletedTask;
        }

        public void RegisterConsumer(BasicDeliverEventArgs message)
        {
            var byteArray = message.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(byteArray);
            var user = JsonConvert.DeserializeObject<CreateUserIntegrationEvent>(messageString);

            // Eu crio um scopo pois esta classe foi injetada com AddHo
            using (var scope = _serviceProvider.CreateScope())
            {

                var command = new CheckStudentUsersCreatedCommand(user.Id);

                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                var t = Task.Run(() => mediator.SendCommand(command));
                t.Wait();


            }
        }
    }
}
