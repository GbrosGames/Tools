#if UNITY_EDITOR

using System;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UnityEditor;

namespace Gbros.Watchers
{
    public static class Watchers
    {
        public static CompositeDisposable Disposables { get; private set; } = new CompositeDisposable();
        public static Dictionary<string, Watcher> All = new Dictionary<string, Watcher>();
        public static List<Watcher> List = new List<Watcher>();
        public static event Action<Watcher> Created;
        public static event Action<Watcher> Deleted;
        public static event Action Cleared;
        public const string Default = nameof(Watcher);

        public const string DefaultEditorPath = "Packages/com.gbros.tools.watchers/Editor/";
        public static string DefaultEditorStylePath = $"{DefaultEditorPath}WatcherEditor.uss";
        public static string DefaultEditorUXMLPath = $"{DefaultEditorPath}WatcherEditor.uxml";
        public static string EditorStylePath = $"{DefaultEditorPath}WatcherEditor.uss";
        public static string EditorUXMLPath = $"{DefaultEditorPath}WatcherEditor.uxml";

        public static Action<string> Logger { get; set; } = null;
        public static Watcher Watcher(string key = Default)
        {
            Watchers.Logger?.Invoke($"Watchers: Trying to get watcher {key}");
            
            if (All.TryGetValue(key, out var item))
            {
                if (!Disposables.Contains(item))
                {
                    Disposables.Add(item);
                }
                return item;
            }

            item = new Watcher(key);
            All.Add(key, item);
            List.Add(item);

            Created?.Invoke(item);

            if (Disposables is null || Disposables.IsDisposed)
                Disposables = new CompositeDisposable();

            Disposables.Add(item);

            Watchers.Logger?.Invoke($"Watchers: watcher {key} - created");
            return item;
        }

        public static void Delete(string key = Default)
        {
            if (!All.TryGetValue(key, out var watcher)) return;
            if (!watcher.IsDisposed)
            {
                watcher.Dispose();
            }
            All.Remove(key);
            List.Remove(watcher);
            Deleted?.Invoke(watcher);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange value)
        {
            if (value != PlayModeStateChange.ExitingPlayMode) return;
            Watchers.Logger?.Invoke($"Watchers: Clearing watchers - count {All.Count}");
            Cleanup();
        }

        public static void Cleanup()
        {
            All.Clear();
            List.Clear();
            Watchers.Logger?.Invoke($"Watchers: Clearing watchers - disposing {Disposables.Count} watchers");
            Disposables.Dispose();
            Cleared?.Invoke();
        }
    }
}
#endif