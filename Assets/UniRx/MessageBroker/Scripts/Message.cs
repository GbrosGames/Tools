using UniRx;
using UnityEngine;

namespace Gbros.UniRx
{
    /// <summary>
    /// Base class for ScriptableObject messages.
    /// If you want to call MessageBroker from MonoBehaviours, call them directly -> Message.Create()
    /// ScriptableObject can't have constructors, so try to add every Message static Create method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Message : ScriptableObject, IMessage
    {
        public void Publish()
        {
            MessageBroker.Default.PublishMessage(this);
        }
    }
}
