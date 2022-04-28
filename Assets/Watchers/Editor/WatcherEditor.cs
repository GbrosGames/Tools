#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Reflection;

namespace Gbros.Watchers
{
    public class WatcherEditor : EditorWindow
    {
        public const string WatcherTopbarButtonClassName = "watcher-topbar-button";
        public const string WatcherTopbarButtonActiveClassName = "watcher-topbar-button-active";
        public const string WatcherSidebarButtonClassName = "watcher-sidebar-button";
        public const string WatcherSidebarButtonActiveClassName = "watcher-sidebar-button-active";
        public const string WatcherCardTitleClassName = "watcher-card-container-title";


        private Watcher currentWatcher;
        private WatcherBoard currentBoard;

        private VisualElement boardContainer;
        private VisualElement rightPanel;

        private Toolbar topbar;
        private VisualElement sidebar;


        [MenuItem("Window/Watchers")]
        public static void OpenWindow()
        {
            Debug.LogWarning("halo");
            var window = GetWindow<WatcherEditor>();
            Debug.LogWarning(window);
            window.titleContent = new GUIContent(nameof(Watchers));
        }

        [MenuItem("Assets/Create/Watchers Stylesheet")]
        public static void CreateStylesheet()
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.DefaultEditorStylePath);

            var getActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            var directory = (string)getActiveFolderPath.Invoke(null, null);

            AssetDatabase.CopyAsset(Watchers.DefaultEditorStylePath, $"{directory}/Watcher Stylesheet.uss");
        }

        public void CreateGUI()
        {
            Cleanup();

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Watchers.EditorUXMLPath);
            Debug.LogWarning(Watchers.EditorUXMLPath);
            Debug.LogWarning(visualTree);
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.EditorStylePath);
            root.styleSheets.Add(styleSheet);

            rightPanel = root.Q<VisualElement>("right-panel");
            sidebar = root.Q<VisualElement>("left-panel");
            boardContainer = rightPanel.Q<VisualElement>("board-container");
            topbar = rightPanel.Q<Toolbar>();

            Watchers.Deleted -= OnWatcherDeleted;
            Watchers.Created -= OnWatcherCreated;
            Watchers.Cleared -= OnWatchersCleared;

            Watchers.Deleted += OnWatcherDeleted;
            Watchers.Created += OnWatcherCreated;
            Watchers.Cleared += OnWatchersCleared;

            foreach (var watcher in Watchers.All.Values)
            {
                OnWatcherCreated(watcher);
            }

            ChangeCurrentWatcher(Watchers.All.Values.OrderBy(x => x.Key).FirstOrDefault());
        }

        private void OnWatchersCleared()
        {
            Cleanup();
        }

        private void ChangeCurrentWatcher(Watcher watcher)
        {
            if (currentWatcher is not null && sidebar.TryQ<Button>(out var button, currentWatcher.Key))
            {
                button.RemoveFromClassList(WatcherSidebarButtonActiveClassName);
            }

            currentWatcher = watcher;
            OnCurrentWatcherChanged();
        }

        private void OnCurrentWatcherChanged()
        {
            topbar.Clear();

            if (currentWatcher is null) return;

            if (sidebar.TryQ<Button>(out var sidebarButton, currentWatcher.Key))
            {
                sidebarButton.AddToClassList(WatcherSidebarButtonActiveClassName);
            }

            topbar.Clear();

            foreach (var board in currentWatcher.Boards)
            {
                var button = new Button(() => { ChangeCurrentBoard(board.Value); }) { name = board.Key, viewDataKey = board.Key, text = board.Key };
                button.AddToClassList(WatcherTopbarButtonClassName);
                topbar.Add(button);
            }

            topbar.Sort((x, y) => string.Compare(x.viewDataKey, y.viewDataKey));

            ChangeCurrentBoard(currentWatcher.Boards.Values.OrderBy(x => x.viewDataKey).FirstOrDefault());
        }

        private void ChangeCurrentBoard(WatcherBoard board)
        {
            if (currentBoard is not null && topbar.TryQ<Button>(out var button, currentBoard.viewDataKey))
            {
                button.RemoveFromClassList(WatcherTopbarButtonActiveClassName);
            }

            currentBoard = board;
            OnCurrentBoardChanged();
        }

        private void OnCurrentBoardChanged()
        {
            if (currentBoard is not null && topbar.TryQ<Button>(out var button, currentBoard.viewDataKey))
            {
                button.AddToClassList(WatcherTopbarButtonActiveClassName);
            }

            boardContainer.Clear();
            boardContainer.Add(currentBoard);

            currentBoard.StretchToParentSize();
            currentBoard.contentContainer.StretchToParentSize();
            currentBoard.contentViewContainer.StretchToParentSize();
            currentBoard.MarkDirtyRepaint();

            var layer = rightPanel.Q<Layer>();
            layer.StretchToParentSize();

            layer.style.position = Position.Relative;
            layer.style.alignItems = Align.FlexStart;
            layer.style.flexShrink = 0;
            layer.style.flexWrap = Wrap.Wrap;

            foreach (var item in currentBoard.graphElements)
            {
                item.style.position = Position.Relative;
                item.UpdatePresenterPosition();
            }

        }

        private void Cleanup()
        {
            boardContainer?.Clear();
            sidebar?.Clear();
            topbar?.Clear();
        }

        private void OnWatcherCreated(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");

            var button = new Button(() => { ChangeCurrentWatcher(watcher); }) { name = watcher.Key, viewDataKey = watcher.Key, text = watcher.Key };
            button.AddToClassList(WatcherSidebarButtonClassName);

            sidebar.Add(button);
            sidebar.Sort((x,y) => string.Compare(x.viewDataKey, y.viewDataKey));
        }

        private void OnWatcherDeleted(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");

            if (!sidebar.TryQ<Button>(out var button, watcher.Key)) return;

            sidebar.Remove(button);

            if (currentWatcher?.Key == watcher.Key)
            {
                ChangeCurrentWatcher(Watchers.All.Values.FirstOrDefault());
            }
        }
    }
}
#endif