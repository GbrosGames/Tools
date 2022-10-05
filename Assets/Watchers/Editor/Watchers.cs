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
        public static List<Watcher> All = new List<Watcher>();
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

        public static Watcher Watcher(string key = Default, string groupKey = null, Action<Watcher> callback = null)
        {
            Logger?.Invoke($"Watchers: Trying to get watcher {key}");

            var item = All.Find(x => x.Path == groupKey + key);
            if (item is not null)
            {
                if (!Disposables.Contains(item))
                {
                    Disposables.Add(item);
                }

                callback?.Invoke(item);

                return item;
            }

            item = new Watcher(key, groupKey);

            All.Add(item);

            Created?.Invoke(item);

            if (Disposables is null || Disposables.IsDisposed)
                Disposables = new CompositeDisposable();

            Disposables.Add(item);

            Logger?.Invoke($"Watchers: watcher {key} - created");

            callback?.Invoke(item);
            return item;
        }

        internal static Action<WatcherEditor> EditorCallbacks;

        public static void RegisterEditorCallback(Action<WatcherEditor> callback)
        {
            if (callback is null) return;
            EditorCallbacks += callback;
        }

        public static void UnregisterEditorCallback(Action<WatcherEditor> callback)
        {
            if (callback is null) return;
            EditorCallbacks -= callback;
        }

        public static void Delete(string key = Default)
        {
            var watcher = All.Find(x => x.Key == key);
            if (watcher is null) return;

            if (!watcher.IsDisposed)
            {
                watcher.Dispose();
            }

            All.Remove(watcher);
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
            Logger?.Invoke($"Watchers: Clearing watchers - count {All.Count}");
            Cleanup();
        }

        public static void Cleanup()
        {
            All.Clear();
            Logger?.Invoke($"Watchers: Clearing watchers - disposing {Disposables.Count} watchers");
            Disposables.Dispose();
            Cleared?.Invoke();
            EditorCallbacks = null;
        }
    }
}
#endif