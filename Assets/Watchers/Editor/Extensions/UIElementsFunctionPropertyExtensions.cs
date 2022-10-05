using UniRx;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

namespace Gbros.Watchers
{
    public static class UIElementsFunctionPropertyExtensions
    {
        public static TSource AddProperty<TSource, TTarget>(
           this TSource source,
           string name,
           TTarget target,
           Func<TTarget, string> getter,
           ICollection<IDisposable> disposables,
           Action<string> setter = null,
           Action<TextField> callback = null)
         where TSource : VisualElement
         where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }
        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string name,
            TTarget target,
            Func<TTarget, int> getter,
            ICollection<IDisposable> disposables,
            Action<int> setter = null,
            Action<IntegerField> callback = null)
            where TSource : VisualElement
            where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string name,
            TTarget target,
            Func<TTarget, float> getter,
            ICollection<IDisposable> disposables,
            Action<float> setter = null,
            Action<FloatField> callback = null)
            where TSource : VisualElement
            where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string name,
            TTarget target,
            Func<TTarget, Vector2> getter,
            ICollection<IDisposable> disposables,
            Action<Vector2> setter = null,
            Action<Vector2Field> callback = null)
            where TSource : VisualElement
            where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
            this TSource source,
            string name,
            TTarget target,
            Func<TTarget, Vector3> getter,
            ICollection<IDisposable> disposables,
            Action<Vector3> setter = null,
            Action<Vector3Field> callback = null)
            where TSource : VisualElement
            where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget>(
           this TSource source,
           string name,
           TTarget target,
           Func<TTarget, bool> getter,
           ICollection<IDisposable> disposables,
           Action<bool> setter = null,
           Action<Toggle> callback = null)
           where TSource : VisualElement
           where TTarget : class
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }

        public static TSource AddProperty<TSource, TTarget, TEnum>(
          this TSource source,
          string name,
          TTarget target,
          Func<TTarget, TEnum> getter,
          ICollection<IDisposable> disposables,
          Action<TEnum> setter = null,
          Action<EnumField> callback = null)
          where TSource : VisualElement
          where TTarget : class
          where TEnum : Enum
        {
            return source.AddProperty(name, target.ObserveEveryValueChanged(getter), disposables, setter, callback);
        }
    }
}
