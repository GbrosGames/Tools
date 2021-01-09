using Gbros.UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example4
{
    [CreateAssetMenu(fileName = nameof(GameEventMessage), menuName = Constants.ComponentPath + nameof(GameEventMessage))]
    public class GameEventMessage : Message
    {
        [SerializeField] string name = default;
        public string Name => name;

        public static GameEventMessage Create(string name)
        {
            var instance = CreateInstance<GameEventMessage>();
            instance.name = name;
            return instance;
        }
    }

}
