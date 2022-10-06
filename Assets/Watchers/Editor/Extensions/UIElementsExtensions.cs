using System;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace Gbros.Watchers
{
    public static class UIElementsExtensions
    {
        public static bool TryQ<T>(this VisualElement element, out T result, string name = null, params string[] classes) where T : VisualElement
        {
            result = element.Q<T>(name, classes);
            return result is not null;
        }
        public static bool TryQ<T>(this VisualElement element, out T result, string name = null, string className = null) where T : VisualElement
        {
            result = element.Q<T>(name, className);
            return result is not null;
        }

        public static VisualElement QUnityTextInput(this VisualElement element) => element.Q("unity-text-input");
        public static VisualElement QTitle(this Node node) => node.Q<VisualElement>("title");
        public static Label QTitleLabel(this Node node) => node.QTitle().Q<Label>("title-label");
        public static T Configure<T>(this T element, Action<T> callback)
        {
            callback?.Invoke(element);
            return element;
        }

        public static TSource Add<TSource, T>(this TSource source, string name, T element, Action<T> callback = null)
        where TSource : VisualElement
        where T : VisualElement
        {
            if (source.Q<T>(name) is null)
            {
                element.name = name;
                source.Add(element);
            }

            callback?.Invoke(element);

            return source;
        }

        public static TSource AddSelectorButton<TSource>(this TSource source, string name, UnityEngine.Object @object, Action<Button> callback = null) where TSource : VisualElement
        {
            return source.AddActionButton(name, () => Selection.activeObject = @object, callback);
        }

        public static TSource AddActionButton<TSource>(this TSource source, string name, System.Action action, Action<Button> callback = null) where TSource : VisualElement
        {
            return source.Add(name, new Button(action) { text = name, }, callback);
        }
    }
}
#endif