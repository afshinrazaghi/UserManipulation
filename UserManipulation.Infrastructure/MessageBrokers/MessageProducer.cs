using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using UserManipulation.Application.Common.Interfaces.MessageBrokers;

namespace UserManipulation.Infrastructure.MessageBrokers
{
    public class MessageProducer : IMessageProducer, IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public MessageProducer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage<T>(T message)
        {
            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>() { { "x-message-ttl", 10000 } }; // Set TTL to 10 seconds in milliseconds
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish("", "users", basicProperties, body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
