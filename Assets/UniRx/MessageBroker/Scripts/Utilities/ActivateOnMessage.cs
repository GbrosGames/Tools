using System;
using UniRx;
using UnityEngine;

namespace Gbros.UniRx.Utilities
{
    /// <summary>
    /// Activates game object when receive Message 
    /// </summary>
    public abstract class ActivateOnMessage<T> : MonoBehaviour where T : Message
    {
        [SerializeField] protected T message;
        protected virtual Func<T, bool> predicate => incomingMessage => incomingMessage.GetType() == message?.GetType();

        public virtual void Awake()
        {
            this.gameObject.SetActive(false);

            MessageBroker.Default.ReceiveMessage<T>()
                .Where(predicate)
                .Subscribe(x => gameObject.SetActive(true))
                .AddTo(this);
        }
    }

    public class ActivateOnMessage : ActivateOnMessage<Message> { }
}
