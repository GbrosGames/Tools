#if UNITY_EDITOR

using UnityEngine.UIElements;

public class WatcherSplitView : TwoPaneSplitView
{
    public new class UxmlFactory : UxmlFactory<WatcherSplitView, UxmlTraits> { }
}

#endif