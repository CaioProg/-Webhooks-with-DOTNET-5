using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineWeb.Dtos;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace AirlineWeb.MessagBus
{
    public class MessageBusClient : IMessageBusClient
    {
        public void SendMessage(NotificationMessageDto notificationMessageDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5001};
            using (var Connection = factory.CreateConnection())
            using (var channel = Connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                var message = JsonSerializer.Serialize(notificationMessageDto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine($"--> Message Published on Message Bus");
            }
        }
    }
}