using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            IConnection connection = connectionFactory.CreateConnection();
            IModel model = connection.CreateModel();
            model.QueueDeclare("BasicTest", false, false, false, null);
            string message = "Getting started with RabbitMQ .Net core";
            byte[] body = Encoding.UTF8.GetBytes(message);
            model.BasicPublish("", "BasicTest", null, body);

            Console.WriteLine($"Sent message {0} ...", message);

            Console.ReadLine();
        }
    }
}
