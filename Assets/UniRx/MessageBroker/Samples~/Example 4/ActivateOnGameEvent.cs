using Gbros.UniRx;
using UnityEngine;
using System;
using Gbros.UniRx.Utilities;

namespace Gbros.UniRx.MessageBrokerSamples.Example4
{
    [CreateAssetMenu(fileName = nameof(ActivateOnGameEvent), menuName = "_" + nameof(ActivateOnGameEvent))]
    public class ActivateOnGameEvent : ActivateOnMessage<GameEventMessage>
    {
        protected override Func<GameEventMessage, bool> predicate => incomingMessage => incomingMessage.Name == message.Name;
    }

}
