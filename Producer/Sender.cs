using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
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

                    // Create a message 
                    string message = "RabbitMQ .Net core";
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    // Publish the message 
                    model.BasicPublish("", "Queue", null, body);

                    Console.WriteLine("Sent message {0} ...", message);
                }
            }

            Console.WriteLine("Press [enter] to exit the sender App...");
            Console.ReadLine();
        }
    }
}
