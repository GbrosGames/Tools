using UniRx;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example1
{
    [AddComponentMenu(Constants.ComponentPath + nameof(ColorGenerator))]
    public class ColorGenerator : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var message = new ColorSwitchMessage(Random.ColorHSV());
                MessageBroker.Default.Publish(message);
            }
        }
    }
}