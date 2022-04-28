#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UniRx;
using UnityEditor;

namespace Gbros.Watchers
{
    public class Watcher : IDisposable
    {
        public bool IsDisposed { get; private set; }
        public CompositeDisposable Disposables { get; } = new CompositeDisposable();
        public Watcher(string key = Watchers.Default)
        {
            Key = key;
        }

        public Dictionary<string, WatcherBoard> Boards { get; private set; } = new Dictionary<string, WatcherBoard>();
        public Dictionary<object, SerializedObject> SerializedObjects { get; private set; } = new Dictionary<object, SerializedObject>();

        public string Key { get; }

        public WatcherBoard Board(string key, Action<WatcherBoard> callback = null)
        {
            if (Boards.TryGetValue(key, out var item)) return item;

            item = new WatcherBoard(this, key);
            Boards.Add(key, item);

            callback?.Invoke(item);

            Watchers.Logger?.Invoke($"Watchers: {key} added to {Key}");
            return item;
        }

        public void Cleanup()
        {
            foreach (var item in Boards.Values)
            {
                item.Clear();
            }

            Boards.Clear();
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            Watchers.Logger?.Invoke($"Watchers: Clearing watcher {Key} - disposing {Disposables.Count} properties");
            Disposables.Dispose();
            Disposables.Clear();
            Cleanup();
            Watchers.Delete(Key);
        }
    }
}
#endif