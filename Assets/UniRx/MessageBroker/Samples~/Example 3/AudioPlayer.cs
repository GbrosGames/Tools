using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example3
{
    [AddComponentMenu(Constants.ComponentPath + nameof(AudioPlayer))]
    public class AudioPlayer : MonoBehaviour
    {
        void Awake()
        {
            MessageBroker
                .Default
                .Receive<ZoneEnterMessage>()
                .Subscribe(PlaySound)
                .AddTo(this);
        }

        private void PlaySound(ZoneEnterMessage message)
        {
            Debug.Log($"Playing {message.ZoneType} sound");
        }
    }
}
