using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace EventBusRabbitMq
{
    /*
     * IDisposable: basically provide to dispose these connection objects, when the object is deconstructed,
     * this sis a good practice to use in connection type classes using disposable 
     */
    public interface IRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}