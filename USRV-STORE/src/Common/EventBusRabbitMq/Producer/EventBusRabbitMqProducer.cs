using System;
using System.Text;
using System.Text.Json.Serialization;
using EventBusRabbitMq.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace EventBusRabbitMq.Producer
{
    public class EventBusRabbitMqProducer
    {
        private readonly IRabbitMqConnection _connection;

        public EventBusRabbitMqProducer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        /**
         * Event used for microservices
         * CLASS 59
         */
        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            using (var channel = _connection.CreateModel())
            {
                // Durable : 
                // TRUE: that means you would like to save this queue with physically memory
                // FALSE: store data in memory

                // Exclusive
                // It is giving you permission to use of its other connection
                // false: we don't need to use and this queue  with other connections

                // AutoDelete:
                // Its determined to automate deletion or not

                // Arguments
                // is parameters related with determined to exchange, if you have any additional args with these queue and exchange
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                    arguments: null);
                // before use JsonConvert.SerializeObject install Newtonsoft.Json 
                var message = JsonConvert.SerializeObject(publishModel);
                // converting the byte format our published model basketCheckoutEvent 
                var body = Encoding.UTF8.GetBytes(message);

                //=====================================================================================================
                // for more information about this params, check RabbitMQ documentation
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                //====================================================================================================
                //properties: we give the basic properties, body also define it in here
                // queueName: is mandatory
                
                // BasicPublish(): allow to message to be received and sent to queue
                // It is using the root algorithm 
                
                // exchange: direct - fanout - topic
                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties,
                    body: body);
                channel.WaitForConfirmsOrDie();
                
                //====================================================================================================
                //A C K   definition
                // these operation provide to create a queue inside the rabbitMQ
                // REVIEW MORE IN RABBITMQ DOC
                channel.BasicAcks += (sender, eventArgs) => { Console.WriteLine("SENT_____R A B B I T MQ"); };
                channel.ConfirmSelect();
            }
        }
    }
}