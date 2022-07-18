#if UNITY_EDITOR

using Gbros.Watchers;
using UnityEngine.UIElements;

public class WatcherSelector : Button, IWatcherElement
{
    public Label Title { get; }
    public Watcher Watcher { get; private set; }

    public WatcherSelector(Watcher watcher)
    {
        Watcher = watcher;

        Title = new Label
        {
            text = watcher.Key
        };

        this.Add("title", Title);

        name = watcher.Key;
        viewDataKey = watcher.Key;

        Watcher = watcher;
    }
}

public class BoardSelector : Button, IWatcherElement
{
    public Label Title { get; }
    public Watcher Watcher { get; private set; }
    public WatcherBoard Board { get; private set; }
    public BoardSelector(WatcherBoard board)
    {
        Watcher = board.Watcher;

        Title = new Label();
        this.Add("title", Title);

        name = board.viewDataKey;
        viewDataKey = board.viewDataKey;
        Title.text = board.name;
    }
}

#endif