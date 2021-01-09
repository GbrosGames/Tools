using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorSwitcher))]
    public class ColorSwitcher : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            MessageBroker.Default
                .Receive<ColorSwitchMessage>()
                .Subscribe(SwitchColor)
                .AddTo(this);
        }

        private void SwitchColor(ColorSwitchMessage request)
        {
            spriteRenderer.color = request.Color;
        }
    }
}