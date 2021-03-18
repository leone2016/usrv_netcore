using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMq
{
    /*
     * These classes basically are the wrapper classes of rabbitMQ client, encapsulate with the connections and the MAIN
     * class is the IConnectionFactory 
     */
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMqConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                //this exception also give you information from rabbitMQ documentation
                // So if the connection is broken or not able to connect, it can triggers to this exception.
                Thread.Sleep(2); //wait 2 sec an try to connect again
                _connection = _connectionFactory.CreateConnection();
            }

            return IsConnected;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMq Connection");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                _connection.Dispose();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}