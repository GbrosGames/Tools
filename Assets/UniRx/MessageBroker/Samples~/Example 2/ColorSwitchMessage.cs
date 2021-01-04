using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [CreateAssetMenu(fileName = nameof(ColorSwitchMessage), menuName = Constants.ComponentPath + nameof(ColorSwitchMessage))]
    public class ColorSwitchMessage : ScriptableObject
    {
        [SerializeField] private Color color = default;
        public Color Color => color;

        public static ColorSwitchMessage Create(Color color)
        {
            var message = CreateInstance<ColorSwitchMessage>();
            message.color = color;
            return message;
        }

        public void Publish()
        {
            MessageBroker.Default.Publish(this);
        }
    }
}