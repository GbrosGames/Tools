using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorGenerator))]
    public class ColorGenerator : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var message = ColorSwitchMessage.Create(Random.ColorHSV());
                MessageBroker.Default.Publish(message);
            }
        }
    }
}