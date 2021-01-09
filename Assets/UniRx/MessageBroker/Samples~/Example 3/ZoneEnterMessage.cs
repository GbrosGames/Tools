using Gbros.UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example3
{
    [CreateAssetMenu(fileName = nameof(ZoneEnterMessage), menuName = Constants.ComponentPath + nameof(ZoneEnterMessage))]
    public class ZoneEnterMessage : Message
    {
        [SerializeField] ZoneType zoneType = default;
        public ZoneType ZoneType => zoneType;
        public static ZoneEnterMessage Create(ZoneType zoneType)
        {
            var message = CreateInstance<ZoneEnterMessage>();
            message.zoneType = zoneType;
            return message;
        }
    }


}
