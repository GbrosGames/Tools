using System;
using UniRx;

namespace Gbros.UniRx
{
    public static class MessageBrokerExtensions
    {
        public static IObservable<T> ReceiveMessage<T>(this IMessageBroker messageBroker)
        => messageBroker.Receive<IMessage>().OfType<IMessage, T>();

        public static void PublishMessage<T>(this IMessageBroker messageBroker, T message) where T : IMessage
        => messageBroker.Publish<IMessage>(message);
    }
}
