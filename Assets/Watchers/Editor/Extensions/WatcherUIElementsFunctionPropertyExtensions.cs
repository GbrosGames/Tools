using UniRx;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Gbros.Watchers
{
    public static class WatcherUIElementsFunctionPropertyExtensions
    {
        public static TSource AddProperty<TSource, TTarget>(
         this TSource source,
         string key,
         TTarget target,
         Func<TTarget, string> getter,
         Action<string> setter = null,
         Action<TextField> callback = null)
       where TSource : VisualElement, IWatcherElement
       where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }
        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string key,
            TTarget target,
            Func<TTarget, int> getter,
            Action<int> setter = null,
            Action<IntegerField> callback = null)
            where TSource : VisualElement, IWatcherElement
            where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string key,
            TTarget target,
            Func<TTarget, float> getter,
            Action<float> setter = null,
            Action<FloatField> callback = null)
            where TSource : VisualElement, IWatcherElement
            where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string key,
            TTarget target,
            Func<TTarget, Vector2> getter,
            Action<Vector2> setter = null,
            Action<Vector2Field> callback = null)
            where TSource : VisualElement, IWatcherElement
            where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string key,
            TTarget target,
            Func<TTarget, Vector3> getter,
            Action<Vector3> setter = null,
            Action<Vector3Field> callback = null)
            where TSource : VisualElement, IWatcherElement
            where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
           this TSource source,
           string key,
           TTarget target,
           Func<TTarget, bool> getter,
           Action<bool> setter = null,
           Action<Toggle> callback = null)
           where TSource : VisualElement, IWatcherElement
           where TTarget : class
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }


        public static TSource AddProperty<TSource, TTarget, TEnum>(
           this TSource source,
           string key,
           TTarget target,
           Func<TTarget, TEnum> getter,
           Action<TEnum> setter = null,
           Action<EnumField> callback = null)
           where TSource : VisualElement, IWatcherElement
           where TTarget : class
           where TEnum : Enum
        {
            return source.AddProperty(key, target.ObserveEveryValueChanged(getter), setter, callback);
        }
    }
}
