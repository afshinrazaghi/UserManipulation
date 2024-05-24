// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "users", durable: true, exclusive: false, autoDelete: false, arguments: null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // Move the message to the destination queue with TTL 10
    var props = channel.CreateBasicProperties();
    props.Headers = new Dictionary<string, object>() { { "x-message-ttl", 10000 } };
    channel.BasicPublish("", "another-user-queue", props, body);
    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
    Console.WriteLine(message);
};

channel.BasicConsume(queue: "users", autoAck: false, consumer: consumer);
Console.WriteLine("Consumer started, waiting for messages...");
Console.ReadKey();
