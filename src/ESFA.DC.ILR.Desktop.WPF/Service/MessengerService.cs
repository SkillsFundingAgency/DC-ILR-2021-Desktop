using System;
using ESFA.DC.ILR.Desktop.Messaging;
using ESFA.DC.ILR.Desktop.Service.Interface;
using GalaSoft.MvvmLight.Messaging;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class MessengerService : Messenger, IMessengerService
    {
        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            base.Register(recipient, action);
        }
    }
}