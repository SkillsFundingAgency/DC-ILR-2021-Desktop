using System;

namespace ESFA.DC.ILR.Desktop.Messaging
{
    /// <summary>
    /// Wrapper for Mvvm Light Messenger exposing only subset, and preventing leak of Mvvm Light in to Service Layer
    /// </summary>
    public interface IMessengerService
    {
        void Register<TMessage>(object recipient, Action<TMessage> action);

        void Send<TMessage>(TMessage message);
    }
}
