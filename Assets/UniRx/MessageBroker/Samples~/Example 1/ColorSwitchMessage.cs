using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorSwitchMessage))]
    public class ColorSwitchMessage
    {
        public Color Color { get; }

        public ColorSwitchMessage(Color color)
        {
            Color = color;
        }
    }
}
