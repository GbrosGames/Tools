using UnityEditor;
using UnityEngine;

namespace Gbros.UniRx
{
    [CustomEditor(typeof(Message), editorForChildClasses: true)]
    public class MessageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!Application.isPlaying) return;

            if (!(target is IMessage message)) return;

            if (GUILayout.Button(nameof(IMessage.Publish)))
            {
                message.Publish();
            }
        }
    }
}

