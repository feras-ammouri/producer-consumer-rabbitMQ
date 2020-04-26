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
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            IConnection connection = connectionFactory.CreateConnection();
            IModel model = connection.CreateModel();
            model.QueueDeclare("BasicTest", false, false, false, null);
            EventingBasicConsumer eventingBasicConsumer = new EventingBasicConsumer(model);
            eventingBasicConsumer.Received += eventingBasicConsumer_Received;
            model.BasicConsume("BasicTest", true, eventingBasicConsumer);
            Console.ReadLine();
        }

        private static void eventingBasicConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            byte[] body = e.Body;
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        }
    }
}
