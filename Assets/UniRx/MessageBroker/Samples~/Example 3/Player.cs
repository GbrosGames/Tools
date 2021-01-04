using Gbros.UniRx;
using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example3
{
    [AddComponentMenu(Constants.ComponentPath + nameof(Player))]
    public class Player : PlayerBase
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var areaTrigger = collision.GetComponent<Zone>();
            if (areaTrigger == null) return;

            var message = ZoneEnterMessage.Create(areaTrigger.ZoneType);
            message.Publish();
        }
    }
}
