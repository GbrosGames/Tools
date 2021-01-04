using UniRx;
using UnityEngine;
using UnityEngine.U2D;

namespace Gbros.UniRx.MessageBrokerSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorSwitcher))]
    [RequireComponent(typeof(SpriteShapeRenderer))]
    public class ColorSwitcher : MonoBehaviour
    {
        private SpriteShapeRenderer spriteShapeRenderer;

        private void Awake()
        {
            spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();

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