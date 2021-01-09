using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorDisplay))]
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
