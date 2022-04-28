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
        public static event Action<Watcher> Created;
        public static event Action<Watcher> Deleted;
        public static event Action Cleared;
        public const string Default = nameof(Watcher);

        public static string DefaultEditorStylePath = "Packages/Gbros - Watchers/Editor/WatcherEditor.uss";
        public static string DefaultEditorUXMLPath = "Packages/Gbros - Watchers/Editor/WatcherEditor.uxml";

        public static Action<string> Logger { get; set; } = null;
        public static string EditorStylePath = DefaultEditorStylePath;
        public static string EditorUXMLPath = DefaultEditorUXMLPath;

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
            Watchers.Logger?.Invoke($"Watchers: Clearing watchers - disposing {Disposables.Count} watchers");
            Disposables.Dispose();
            Cleared?.Invoke();
        }
    }
}
#endif