#if UNITY_EDITOR
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq.Expressions;
using UnityEditor;

namespace Gbros.Watchers
{
    public static class WatcherUIElementsSerializedPropertyExtensions
    {
        public static TSource AddSerializedProperty<TSource, TTarget, TProperty>(this TSource source,
         TTarget target,
         Expression<Func<TTarget, TProperty>> propertySelector,
         string name = null)
     where TSource : VisualElement, IWatcherElement
     where TTarget : UnityEngine.Object
        {
            if (!source.Watcher.SerializedObjects.TryGetValue(target, out var serializedObject))
            {
                serializedObject = new SerializedObject(target);
                source.Watcher.SerializedObjects.Add(target, serializedObject);
            }

            var propertyName = propertySelector.GetName();
            source.Add(name ?? propertyName, new PropertyField(serializedObject.FindProperty(propertyName), name ?? propertyName), field => field.Bind(serializedObject));
            return source;
        }

        public static TSource AddSerializedProperty<TSource, TTarget>(this TSource source,
         TTarget target,
         string propertyName,
         string name = null)
     where TSource : VisualElement, IWatcherElement
     where TTarget : UnityEngine.Object
        {
            if (!source.Watcher.SerializedObjects.TryGetValue(target, out var serializedObject))
            {
                serializedObject = new SerializedObject(target);
                source.Watcher.SerializedObjects.Add(target, serializedObject);
            }

            source.Add(name ?? propertyName, new PropertyField(serializedObject.FindProperty(propertyName), name ?? propertyName), field => field.Bind(serializedObject));
            return source;
        }
    }
}
#endif