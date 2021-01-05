using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorSwitcher))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSwitcher : MonoBehaviour
    {
        private SpriteRenderer spriteShapeRenderer;

        private void Awake()
        {
            spriteShapeRenderer = GetComponent<SpriteRenderer>();

            MessageBroker.Default
                .Receive<ColorSwitchMessage>()
                .Subscribe(SwitchColor)
                .AddTo(this);
        }

        private void SwitchColor(ColorSwitchMessage message)
        {
            spriteShapeRenderer.color = message.Color;
        }
    }
}