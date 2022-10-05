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

            Watchers.Watcher("Watcher A")
                        .Board("Board AAA")
                        .Card("Card AAA");

            Watchers.Watcher("Watcher B", "Some Group")
                        .Board("Board C")
                        .Card("Card B2");

            Watchers.Watcher("Watcher B", "Some Group aaa")
                       .Board("Board asdasdasdC")
                       .Card("Card Bsss2");

            Watchers.Watcher("Watcher B", "Some Group\\1")
                    .Board("Board1 asdasdasdC")
                    .Card("Card1 Bsss2");
            
            Watchers.Watcher("Watcher B", "Some Group\\1")
                   .Board("Board2 2222")
                   .Card("Card2 222");

            Watchers.Watcher("Watcher B2", "Some Group\\1")
                   .Board("Board2")
                   .Card("Card2 ");

            Watchers.Watcher("Watcher B222", "Some Group\\1\\2")
                  .Board("Board23")
                  .Card("Card23 ");
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
