#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System.Reflection;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections.Generic;
using System;

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

        private ScrollView topbar;
        // private ScrollView sidebar;
        private ListView sidebar;


        [MenuItem("Window/Watchers")]
        public static void OpenWindow()
        {
            var window = GetWindow<WatcherEditor>();
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
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Watchers.EditorStylePath);
            root.styleSheets.Add(styleSheet);

            rightPanel = root.Q<VisualElement>("right-panel");
            // sidebar = root.Q<VisualElement>("left-panel").Q<ScrollView>();
            sidebar = root.Q<VisualElement>("left-panel").Q<ListView>();

            boardContainer = rightPanel.Q<VisualElement>("board-container");
            topbar = rightPanel.Q<ScrollView>();
            // topbar = rightPanel.Q<Toolbar>().Q<ScrollView>();

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

            sidebar.makeItem = () => new WatcherSelector();
            sidebar.bindItem = (e, i) =>
            {
                if (e is not WatcherSelector watcherSelector) return;
                var watcher = Watchers.List[i];
                watcherSelector.Bind(watcher);
                watcherSelector.RemoveButton.clicked += () => 
                { 
                    Watchers.Delete(watcher.Key);
                };
            };
            sidebar.itemsSource = Watchers.List;
            sidebar.selectionType = SelectionType.Single;
            sidebar.onSelectionChange += OnWatcherSelectorSelectionChanged;
        }

        private void OnWatcherSelectorSelectionChanged(IEnumerable<object> obj)
        {
            var currentSelection = obj.FirstOrDefault() as Watcher;
            ChangeCurrentWatcher(currentSelection);
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
            sidebar?.Rebuild();
        }

        private void OnWatcherCreated(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");
            sidebar.Sort((x, y) => string.Compare(x.viewDataKey, y.viewDataKey));
        }

        private void OnWatcherDeleted(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");

            // if (!sidebar.TryQ<Button>(out var button, watcher.Key)) return;
            sidebar.Rebuild();

            if (currentWatcher?.Key == watcher.Key)
            {
                ChangeCurrentWatcher(Watchers.All.Values.FirstOrDefault());
            }
        }
    }
}
#endif