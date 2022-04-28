#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;

namespace Gbros.Watchers
{
    public class WatcherCardContainer : VisualElement, IWatcherElement
    {
        public WatcherCard Card { get; }
        public WatcherBoard Board => Card.Board;
        public Watcher Watcher => Board.Watcher;
        public Label Title { get; }

        public WatcherCardContainer(WatcherCard card, string name)
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.EditorStylePath);
            styleSheets.Add(styleSheet);

            this.name = name;
            viewDataKey = name;
            Card = card;
            Title = new Label(name);
            Title.viewDataKey = name;
            Title.AddToClassList(WatcherEditor.WatcherCardTitleClassName);

            Add(Title);
        }
    }
}
#endif