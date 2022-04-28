#if UNITY_EDITOR
using UniRx;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;

namespace Gbros.Watchers
{
    public static class WatcherUIElementsPropertyExtensions
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

        public static TSource AddProperty<TSource>(
          this TSource source,
          string key,
          IObservable<string> property,
          Action<string> setter = null,
          Action<TextField> callback = null)
          where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new TextField(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                else
                {
                    field.isReadOnly = true;
                }
                callback?.Invoke(field);
            });
        }

        public static TSource AddProperty<TSource>(
         this TSource source,
         string key,
         IObservable<int> property,
         Action<int> setter = null,
         Action<IntegerField> callback = null)
         where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new IntegerField(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                else
                {
                    field.isReadOnly = true;
                }
                callback?.Invoke(field);
            });
        }

        public static TSource AddProperty<TSource>(
        this TSource source,
        string key,
        IObservable<float> property,
        Action<float> setter = null,
        Action<FloatField> callback = null)
        where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new FloatField(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                else
                {
                    field.isReadOnly = true;
                }
                callback?.Invoke(field);
            });
        }



        public static TSource AddProperty<TSource>(
        this TSource source,
        string key,
        IObservable<Vector2> property,
        Action<Vector2> setter = null,
        Action<Vector2Field> callback = null)
       where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new Vector2Field(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                else
                {
                    foreach (var floatField in field.Children().OfType<FloatField>())
                    {
                        floatField.isReadOnly = true;
                    }
                }
                callback?.Invoke(field);
            });
        }

        public static TSource AddProperty<TSource>(
            this TSource source,
            string key,
            IObservable<Vector3> property,
            Action<Vector3> setter = null,
            Action<Vector3Field> callback = null)
        where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new Vector3Field(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                else
                {
                    foreach (var floatField in field.Children().OfType<FloatField>())
                    {
                        floatField.isReadOnly = true;
                    }
                }
                callback?.Invoke(field);
            });
        }

        public static TSource AddProperty<TSource>(
            this TSource source,
            string key,
            IObservable<bool> property,
            Action<bool> setter = null,
            Action<Toggle> callback = null)
        where TSource : VisualElement, IWatcherElement
        {
            return source.Add(key, new Toggle(key), field =>
            {
                field.BindTo(property).AddTo(source);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                callback?.Invoke(field);
            });
        }

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