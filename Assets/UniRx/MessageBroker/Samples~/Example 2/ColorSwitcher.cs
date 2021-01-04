using UniRx;
using UnityEngine;
using UnityEngine.U2D;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [RequireComponent(typeof(SpriteShapeRenderer))]
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorSwitcher))]
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

        private void SwitchColor(ColorSwitchMessage request)
        {
            spriteShapeRenderer.color = request.Color;
        }
    }
}