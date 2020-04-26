using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            // Instantiate a connection to the RabbitMQ node
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            // Open a connection
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                // Open a channel
                using (IModel model = connection.CreateModel())
                {
                    // Declare a queue
                    model.QueueDeclare("Queue", false, false, false, null);

                    // Create a consumer object
                    EventingBasicConsumer eventingBasicConsumer = new EventingBasicConsumer(model);

                    // Event for dispatching deliveries and other consumer lifecycle
                    eventingBasicConsumer.Received += (channel, ea) =>
                    {
                        byte[] body = ea.Body;
                        string message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message {0}...",message);
                    };

                    // Use the channel to consume the message 
                    model.BasicConsume("Queue", true, eventingBasicConsumer);
                }
            }

            Console.ReadLine();
        }
    }
}
