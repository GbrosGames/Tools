using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example4
{
    [CreateAssetMenu(fileName = nameof(GameStartMessage), menuName = Constants.ComponentPath + nameof(GameStartMessage))]
    public class GameStartMessage : Message
    {
        public static GameStartMessage Create() => CreateInstance<GameStartMessage>();
    }

}
