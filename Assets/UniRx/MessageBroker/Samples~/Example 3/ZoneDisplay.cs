using Gbros.UniRx;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gbros.UniRx.MessageBrokerSamples.Example3
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu(Constants.ComponentPath + nameof(ZoneDisplay))]
    public class ZoneDisplay : MonoBehaviour
    {
        [SerializeField] float notificationDuration = 2f;

        private IObservable<ZoneEnterMessage> whenPlayerEntersNewZone;
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();

            whenPlayerEntersNewZone = MessageBrokerExtensions.Receive<ZoneEnterMessage>(MessageBroker.Default
);

            whenPlayerEntersNewZone
                .Subscribe(Display)
                .AddTo(this);
        }

        private void Display(ZoneEnterMessage request)
        {
            text.text = $"Entering {request.ZoneType} zone";

            Observable
                .Timer(TimeSpan.FromSeconds(notificationDuration))
                .TakeUntil(whenPlayerEntersNewZone)
                .Subscribe(_ =>
                {
                    text.text = string.Empty;
                })
                .AddTo(this);
        }
    }
}
