using UnityEditor;
using UnityEngine;

namespace Gbros.UniRx.MessageBrokerSamples.Example2
{
    [CustomEditor(typeof(ColorSwitchMessage))]
    public class ColorSwitchMessageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!Application.isPlaying) return;

            if (GUILayout.Button(nameof(ColorSwitchMessage.Publish)))
            {
                var message = (ColorSwitchMessage)target;
                message.Publish();
            }
        }
    }
}