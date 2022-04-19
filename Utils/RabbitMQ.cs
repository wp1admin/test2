using producer.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Utils

{
    public class RabbitMQ
    {
        public static void publish(APIresult apiResult)
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("RABBITMQ_PORT"))
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "result",
                            type: ExchangeType.Direct,
                            durable: true,
                            autoDelete: false);


                channel.QueueDeclare(queue: "TaskQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);



                //string message = greeting.Greet;
                var body = JsonSerializer.SerializeToUtf8Bytes(apiResult);

                channel.BasicPublish(exchange: "result",
                                     routingKey: "result",
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}
