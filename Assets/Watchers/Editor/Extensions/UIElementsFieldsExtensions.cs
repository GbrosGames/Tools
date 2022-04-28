#if UNITY_EDITOR
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Gbros.Watchers
{
    public static class UIElementsFieldsExtensions
    {
        public static TSource AddTextField<TSource>(this TSource source, string name, Action<TextField> callback = null) where TSource : VisualElement => source.Add(name, new TextField(name), callback);
        public static TSource AddIntField<TSource>(this TSource source, string name, Action<IntegerField> callback = null) where TSource : VisualElement => source.Add(name, new IntegerField(name), callback);
        public static TSource AddFloatField<TSource>(this TSource source, string name, Action<FloatField> callback = null) where TSource : VisualElement => source.Add(name, new FloatField(name), callback);
        public static TSource AddBoolField<TSource>(this TSource source, string name, Action<Toggle> callback = null) where TSource : VisualElement => source.Add(name, new Toggle(name), callback);
        public static TSource AddVector3Field<TSource>(this TSource source, string name, Action<Vector3Field> callback = null) where TSource : VisualElement => source.Add(name, new Vector3Field(name), callback);
        public static TSource AddVector2Field<TSource>(this TSource source, string name, Action<Vector2Field> callback = null) where TSource : VisualElement => source.Add(name, new Vector2Field(name), callback);
    }
}
#endif