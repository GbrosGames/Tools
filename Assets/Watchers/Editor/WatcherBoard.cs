#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace Gbros.Watchers
{
    public class WatcherBoard : GraphView, IWatcherElement
    {
        public Watcher Watcher { get; private set; }
        public WatcherBoard(Watcher watcher, string name)
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.EditorStylePath);
            styleSheets.Add(styleSheet);

            this.name = name;
            viewDataKey = name;
            Watcher = watcher;

            AddLayer(0);

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public WatcherCard Card(string name, Action<WatcherCard> callback = null)
        {
            Watchers.Logger?.Invoke($"Watchers: Trying to get {nameof(WatcherCard)} {name} from {nameof(WatcherBoard)} {name}");
            var item = this.Q<WatcherCard>(name);
            if (item is not null) return item;

            item = new WatcherCard(this, name);
            contentViewContainer.Q<Layer>().Add(item);
            callback?.Invoke(item);

            Watchers.Logger?.Invoke($"Watchers: {nameof(WatcherCard)} {name} added to {nameof(WatcherBoard)} {name}");
            return item;
        }
    }
}
#endif