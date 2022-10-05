using UniRx;
using System;

namespace Gbros.Watchers
{
    public static class WatcherExtensions
    {
        public static void AddTo(this IDisposable disposable, Watcher watcher) => disposable.AddTo(watcher.Disposables);
        public static void AddTo(this IDisposable disposable, IWatcherElement watcherElement) => disposable.AddTo(watcherElement.Watcher);
    }
}
