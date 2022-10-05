using System;
using Gbros.Watchers.Samples.Example2;
using UnityEngine;

namespace Gbros.Watchers.Samples.Example1
{
    [Serializable]
    public class SomeClass
    {
        public Color SomeColor;
        public string SomeText;
    }

    public class SomeNotUnityObjectClass
    {
        public int SomeValue { get; set; }
    }


    [AddComponentMenu(Constants.ComponentPath + nameof(SomeComponent))]
    public class SomeComponent : MonoBehaviour
    {

        public int SomeValue = 2;
        public Vector2 SomeVector2 = Vector2.up;
        public Vector3 SomeVector3 = Vector3.up;
        public SomeClass SomeClass;

        private void Awake()
        {
            var someNotUnityObjectClass = new SomeNotUnityObjectClass
            {
                SomeValue = 123
            };

#if UNITY_EDITOR
            Watchers.Watcher("Watcher A")
                           .Board("Board A")
                           .Card("Card A")
                           .Container("Container A")
                           .AddSelectorButton($"Select {gameObject.name}", gameObject)
                           .AddSerializedProperty(this, x => x.SomeValue)
                           .AddSerializedProperty(this, "SomeVector2")
                           .AddSerializedProperty(this, x => x.SomeVector3)
                           .AddSerializedProperty(this, x => x.SomeClass)
                           .AddProperty("SomeValue in someNonSerializableClass", someNotUnityObjectClass, x => x.SomeValue, x => someNotUnityObjectClass.SomeValue = x)
                           .AddActionButton("Some Action", () => { Debug.Log($"Value from nonUnityObjectClass - {someNotUnityObjectClass.SomeValue}"); });

            Watchers.Watcher("Watcher B", "Some Group")
                          .Board("Board B")
                          .Card("Card B")
                          .Container("Container B")
                          .AddSelectorButton($"Select {gameObject.name}", gameObject)
                          .AddSerializedProperty(this, x => x.SomeValue)
                          .AddSerializedProperty(this, "SomeVector2")
                          .AddSerializedProperty(this, x => x.SomeVector3)
                          .AddSerializedProperty(this, x => x.SomeClass)
                          .AddProperty("SomeValue in someNonSerializableClass", someNotUnityObjectClass, x => x.SomeValue, x => someNotUnityObjectClass.SomeValue = x)
                          .AddActionButton("Some Action", () => { Debug.Log($"Value from nonUnityObjectClass - {someNotUnityObjectClass.SomeValue}"); });

            Watchers.Watcher("Watcher B2", "Some Group")
                        .Board("Board B2")
                        .Card("Card B2")
                        .Container("Container B")
                        .AddSelectorButton($"Select {gameObject.name}", gameObject)
                        .AddProperty("SomeValue in someNonSerializableClass", someNotUnityObjectClass, x => x.SomeValue, x => someNotUnityObjectClass.SomeValue = x)
                        .AddActionButton("Some Action", () => { Debug.Log($"Value from nonUnityObjectClass - {someNotUnityObjectClass.SomeValue}"); });
#endif
        }
    }

    public class WatcherInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void Initialize()
    {
        // Watchers.EditorStylePath = "Assets/MyStyles.uss";
    }
}
}
