#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
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
        public const string WatcherFoldoutActiveClassName = "watcher-foldout-active";
        public const string WatcherCardTitleClassName = "watcher-card-container-title";

        public Watcher CurrentWatcher { get; private set; }
        public WatcherBoard CurrentBoard { get; private set; }
        public VisualElement BoardContainer { get; private set; }

        public VisualElement LeftPanel { get; private set; }
        public VisualElement RightPanel { get; private set; }

        public ScrollView Topbar { get; private set; }
        public ScrollView Sidebar { get; private set; }


        [MenuItem("Window/Watchers")]
        public static void OpenWindow()
        {
            Create();
        }

        public static WatcherEditor Create()
        {
            var window = GetWindow<WatcherEditor>();
            window.titleContent = new GUIContent(nameof(Watchers));
            return window;
        }

        [MenuItem("Assets/Create/Watchers Stylesheet")]
        public static void CreateStylesheet()
        {
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

            Initialize();
        }

        private void Initialize()
        {
            RightPanel = rootVisualElement.Q<VisualElement>("right-panel");
            LeftPanel = rootVisualElement.Q<VisualElement>("left-panel");

            Topbar = RightPanel.Q<ScrollView>();
            Sidebar = LeftPanel.Q<ScrollView>();

            BoardContainer = RightPanel.Q<VisualElement>("board-container");

            Watchers.Deleted -= OnWatcherDeleted;
            Watchers.Created -= OnWatcherCreated;
            Watchers.Cleared -= OnWatchersCleared;

            Watchers.Deleted += OnWatcherDeleted;
            Watchers.Created += OnWatcherCreated;
            Watchers.Cleared += OnWatchersCleared;

            InitializeWatchers();

            Watchers.EditorCallbacks?.Invoke(this);
        }

        private void InitializeWatchers()
        {
            Cleanup();

            foreach (var watcher in Watchers.All.OrderBy(x => x.Path))
            {
                OnWatcherCreated(watcher);
            }

            ChangeCurrentWatcher(Watchers.All.OrderBy(x => x.Path).FirstOrDefault());
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

        public void ChangeCurrentWatcher(Watcher watcher)
        {
            if (CurrentWatcher is not null)
            {
                CurrentWatcher.Selector.RemoveFromClassList(WatcherSidebarButtonActiveClassName);
            }

            CurrentWatcher = watcher;
            OnCurrentWatcherChanged();
        }

        private void OnCurrentWatcherChanged()
        {
            Topbar.Clear();

            if (CurrentWatcher is null) return;

            CurrentWatcher.Selector.AddToClassList(WatcherSidebarButtonActiveClassName);

            Sidebar.Query<Foldout>().ForEach(x => x.RemoveFromClassList(WatcherFoldoutActiveClassName));
            
            if (CurrentWatcher.GroupKey is not null)
            {
                var keys = CurrentWatcher.GroupKey.Split('\\');

                var currentElement = Sidebar as VisualElement;

                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];

                    if (currentElement.TryQ<Foldout>(out var foldout, key))
                    {
                        currentElement = foldout;
                        currentElement.AddToClassList(WatcherFoldoutActiveClassName);
                    }
                }
            }

            Topbar.Clear();

            foreach (var board in CurrentWatcher.Boards)
            {
                var button = new Button(() => { ChangeCurrentBoard(board); }) { name = board.name, viewDataKey = board.viewDataKey, text = board.name };
                button.AddToClassList(WatcherTopbarButtonClassName);
                Topbar.Add(button);
            }

            Topbar.Sort((x, y) => string.Compare(x.viewDataKey, y.viewDataKey));

            ChangeCurrentBoard(CurrentWatcher.Boards.OrderBy(x => x.viewDataKey).FirstOrDefault());
        }

        public void ChangeCurrentBoard(WatcherBoard board)
        {
            if (CurrentWatcher != board?.Watcher)
            {
                ChangeCurrentWatcher(board?.Watcher);
            }

            if (CurrentBoard is not null && Topbar.TryQ<Button>(out var button, CurrentBoard.viewDataKey))
            {
                button.RemoveFromClassList(WatcherTopbarButtonActiveClassName);
            }

            CurrentBoard = board;
            OnCurrentBoardChanged();
        }

        private void OnCurrentBoardChanged()
        {
            if (CurrentBoard is not null && Topbar.TryQ<Button>(out var button, CurrentBoard.viewDataKey))
            {
                button.AddToClassList(WatcherTopbarButtonActiveClassName);
            }

            BoardContainer.Clear();
            BoardContainer.Add(CurrentBoard);

            if (CurrentBoard is null) return;

            CurrentBoard.StretchToParentSize();
            CurrentBoard.contentContainer.StretchToParentSize();
            CurrentBoard.contentViewContainer.StretchToParentSize();
            CurrentBoard.MarkDirtyRepaint();

            var layer = RightPanel.Q<Layer>();
            layer.StretchToParentSize();

            layer.style.position = Position.Relative;
            layer.style.alignItems = Align.FlexStart;
            layer.style.flexShrink = 0;
            layer.style.flexWrap = Wrap.Wrap;

            foreach (var item in CurrentBoard.graphElements)
            {
                item.style.position = Position.Relative;
                item.UpdatePresenterPosition();
            }

        }

        private void Cleanup()
        {
            BoardContainer?.Clear();
            Topbar?.Clear();
            Sidebar?.Clear();
        }

        private void OnWatcherCreated(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");

            watcher.Selector.clicked -= () => ChangeCurrentWatcher(watcher);
            watcher.Selector.clicked += () => ChangeCurrentWatcher(watcher);

            if (watcher.GroupKey is not null)
            {
                var keys = watcher.GroupKey.Split('\\');

                var currentElement = Sidebar as VisualElement;

                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];

                    if (!currentElement.TryQ<Foldout>(out var foldout, key))
                    {
                        foldout = new Foldout
                        {
                            name = key,
                            text = key
                        };

                        currentElement.Add(foldout);
                    }

                    currentElement = foldout;

                }

                currentElement.Add(watcher.Selector);
            }
            else
            {
                Sidebar.Add(watcher.Selector);
            }
        }

        private void OnWatcherDeleted(Watcher watcher)
        {
            Watchers.Logger?.Invoke($"Watchers: Editor - adding {watcher.Key} to left panel");

            InitializeWatchers();

            if (CurrentWatcher == watcher)
            {
                CurrentWatcher = null;
                ChangeCurrentWatcher(Watchers.All.FirstOrDefault());
            }
        }
    }
}
#endif