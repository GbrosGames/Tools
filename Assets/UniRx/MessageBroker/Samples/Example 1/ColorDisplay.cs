using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gbros.UniRx.MessageBrokerSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorDisplay))]
    [RequireComponent(typeof(Text))]
    public class ColorDisplay : MonoBehaviour
    {
        private Text text;
        private void Awake()
        {
            text = GetComponent<Text>();

            MessageBroker
                .Default
                .Receive<ColorSwitchMessage>()
                .Subscribe(Display)
                .AddTo(this);
        }

        private void Display(ColorSwitchMessage message)
        {
            text.text = $"{message.Color}";
        }
    }
}
