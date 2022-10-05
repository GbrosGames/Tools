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

        public WatcherSelector Selector { get; }

        public string Path { get; }
        
        internal Watcher(string key = Watchers.Default, string groupKey = null)
        {
            Key = key;
            GroupKey = groupKey;
            Selector = new WatcherSelector(this);
            Path = GroupKey + Path;
        }

        public List<WatcherBoard> Boards { get; private set; } = new List<WatcherBoard>();
        public Dictionary<object, SerializedObject> SerializedObjects { get; private set; } = new Dictionary<object, SerializedObject>();

        public string Key { get; }
        public string GroupKey { get; }


        public WatcherBoard Board(string key, Action<WatcherBoard> callback = null)
        {
            var item = Boards.Find(x => x.viewDataKey == key);

            if (item is not null) return item;

            item = new WatcherBoard(this, key);
            Boards.Add(item);

            callback?.Invoke(item);

            Watchers.Logger?.Invoke($"Watchers: {key} added to {Key}");
            return item;
        }

        public void Cleanup()
        {
            foreach (var item in Boards)
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