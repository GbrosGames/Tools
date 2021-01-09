using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example3
{
    [AddComponentMenu(Constants.ComponentPath + nameof(Zone))]
    public class Zone : MonoBehaviour
    {
        [SerializeField] ZoneType zoneType = default;
        public ZoneType ZoneType => zoneType;
    }
}
