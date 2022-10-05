using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using UniRx;
using UnityEditor;

namespace Gbros.Watchers
{
    public static class WatcherUIElementsObservablePropertyExtensions
    {
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

        public static TSource AddProperty<TSource,TEnum>(
           this TSource source,
           string key,
           IObservable<TEnum> property,
           Action<TEnum> setter = null,
           Action<EnumField> callback = null)
       where TSource : VisualElement, IWatcherElement
       where TEnum : Enum
        {
            return source.Add(key, new EnumField(key, (TEnum)default), field =>
            {
                field.BindTo(property).AddTo(source);
               
                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter((TEnum)x.newValue));
                }
                callback?.Invoke(field);
            });
        }

    }
}
