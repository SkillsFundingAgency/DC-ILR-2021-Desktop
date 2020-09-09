using System;
using PubSub.Core;

namespace ESFA.DC.ILR.Desktop.Messaging
{
    public class CommandLineMessengerService : IMessengerService
    {
        private readonly Hub _hub = new Hub();

        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            _hub.Subscribe(recipient, action);
        }

        public void Send<TMessage>(TMessage message)
        {
            _hub.Publish(message);
        }
    }
}
