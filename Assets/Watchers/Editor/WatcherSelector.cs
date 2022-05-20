#if UNITY_EDITOR

using System;
using Gbros.Watchers;
using UnityEngine.UIElements;

public class WatcherSelector : VisualElement, IWatcherElement
{
    public Label Title { get; }
    public Watcher Watcher { get; private set; }

    public WatcherSelector(Watcher watcher)
    {
        Watcher = watcher;
        Bind(watcher);
    }

    public WatcherSelector()
    {
        Title = new Label();
        this.Add("title", Title);
    }

    public void Bind(Watcher watcher)
    {
        Watcher = watcher;
        Title.text = watcher.Key;
    }
}

#endif