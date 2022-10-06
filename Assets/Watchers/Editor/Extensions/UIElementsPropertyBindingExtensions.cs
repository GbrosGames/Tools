#if UNITY_EDITOR
using UniRx;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Gbros.Watchers
{
    public static class UIElementsPropertyBindingExtensions
    {
        public static IDisposable BindTo<T>(this TextValueField<T> field, IObservable<T> observable) => observable.SubscribeWithState(field, (x, state) => state.value = x);
        public static IDisposable BindTo(this TextField field, IObservable<string> observable) => observable.SubscribeWithState(field, (x, state) => state.value = x);
        public static IDisposable BindTo(this Vector3Field field, IObservable<Vector3> observable) => observable.SubscribeWithState(field, (x, state) => state.value = x);
        public static IDisposable BindTo(this Vector2Field field, IObservable<Vector2> observable) => observable.SubscribeWithState(field, (x, state) => state.value = x);
        public static IDisposable BindTo(this Toggle field, IObservable<bool> observable) => observable.SubscribeWithState(field, (x, state) => state.value = x);
        public static IDisposable BindTo<TEnum>(this EnumField field, IObservable<TEnum> observable) where TEnum: Enum => observable.SubscribeWithState(field, (x, state) => state.value = x);
    }
}
#endif