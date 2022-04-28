#if UNITY_EDITOR
using UniRx;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using System.Linq.Expressions;

namespace Gbros.Watchers
{
    public static class UIElementsPropertyExtensions
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

        public static TSource AddProperty<TSource>(
          this TSource source,
          string name,
          IObservable<string> property,
          ICollection<IDisposable> disposables,
          Action<string> setter = null,
          Action<TextField> callback = null)
          where TSource : VisualElement
        {
            return source.Add(name, new TextField(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

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
         string name,
         IObservable<int> property,
         ICollection<IDisposable> disposables,
         Action<int> setter = null,
         Action<IntegerField> callback = null)
         where TSource : VisualElement
        {
            return source.Add(name, new IntegerField(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

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
        string name,
        IObservable<float> property,
        ICollection<IDisposable> disposables,
        Action<float> setter = null,
        Action<FloatField> callback = null)
        where TSource : VisualElement
        {
            return source.Add(name, new FloatField(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

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
        string name,
        IObservable<Vector2> property,
        ICollection<IDisposable> disposables,
        Action<Vector2> setter = null,
        Action<Vector2Field> callback = null)
       where TSource : VisualElement
        {
            return source.Add(name, new Vector2Field(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

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
            string name,
            IObservable<Vector3> property,
            ICollection<IDisposable> disposables,
            Action<Vector3> setter = null,
            Action<Vector3Field> callback = null)
        where TSource : VisualElement
        {
            return source.Add(name, new Vector3Field(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

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
            string name,
            IObservable<bool> property,
            ICollection<IDisposable> disposables,
            Action<bool> setter = null,
            Action<Toggle> callback = null)
        where TSource : VisualElement
        {
            return source.Add(name, new Toggle(name), field =>
            {
                field.BindTo(property).AddTo(disposables);

                if (setter is not null)
                {
                    field.RegisterValueChangedCallback(x => setter(x.newValue));
                }
                callback?.Invoke(field);
            });
        }
    }
}
#endif