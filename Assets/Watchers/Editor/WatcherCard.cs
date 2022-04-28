#if UNITY_EDITOR

using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System;

namespace Gbros.Watchers
{
    public class WatcherCard : Node, IWatcherElement
    {
        public WatcherBoard Board { get; }
        public Watcher Watcher => Board.Watcher;
        public VisualElement TitleContainer { get; }
        public VisualElement Title { get; }

        public WatcherCard(WatcherBoard watcher,
            string name,
            bool hideInputContainer = true,
            bool hideOutputContainer = true,
            bool hideTitleButtonContainer = true)
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.EditorStylePath);
            styleSheets.Add(styleSheet);

            this.name = name;
            viewDataKey = name;
            title = name;
            Board = watcher;

            if (hideInputContainer)
                inputContainer.style.display = DisplayStyle.None;

            if (hideOutputContainer)
                inputContainer.style.display = DisplayStyle.None;

            if (hideTitleButtonContainer)
                titleButtonContainer.style.display = DisplayStyle.None;

            RefreshExpandedState();

            TitleContainer = this.QTitle();
            Title = this.QTitleLabel();
        }

        public WatcherCardContainer Container(string name, Action<WatcherCardContainer> callback = null)
        {
            Watchers.Logger?.Invoke($"Watchers: Trying to get {nameof(WatcherCardContainer)} {name} from {nameof(WatcherCard)} {name}");
            var item = this.Q<WatcherCardContainer>(name);
            if (item is not null) return item;

            item = new WatcherCardContainer(this, name);
            Add(item);
            callback?.Invoke(item);
            Watchers.Logger?.Invoke($"Watchers: {nameof(WatcherCardContainer)} {name} added to {nameof(WatcherCard)} {name}");
            return item;
        }
    }
}
#endif
